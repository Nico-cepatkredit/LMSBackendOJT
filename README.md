📘 CQRS + Clean Architecture Setup

ASP.NET Core · .NET 8 · Step-by-Step

0️⃣ Prerequisites

.NET SDK 8.x (SDK 10 is fine as long as projects target net8.0)

Windows / macOS / Linux

VS Code or Visual Studio

Verify:

dotnet --list-sdks

1️⃣ Create Solution Structure
Purpose

A solution groups all related projects.

mkdir LMSBackend
cd LMSBackend
dotnet new sln -n LMSBackend
mkdir src tests


📌 Result:

LMSBackend/
├── LMSBackend.slnx
├── src/
└── tests/

2️⃣ Create Projects (NET 8)
Purpose of each project
Project	Purpose
API	HTTP entry point
Application	CQRS logic
Domain	Business rules
Infrastructure	DB & external systems
Tests	Unit & integration tests
Create API
cd src
dotnet new webapi -n LMSBackend.API -f net8.0
cd ..
dotnet sln add src/LMSBackend.API/LMSBackend.API.csproj

Create Class Libraries
cd src
dotnet new classlib -n LMSBackend.Application -f net8.0
dotnet new classlib -n LMSBackend.Domain -f net8.0
dotnet new classlib -n LMSBackend.Infrastructure -f net8.0
cd ..


Add to solution:

dotnet sln add src/LMSBackend.Application/LMSBackend.Application.csproj
dotnet sln add src/LMSBackend.Domain/LMSBackend.Domain.csproj
dotnet sln add src/LMSBackend.Infrastructure/LMSBackend.Infrastructure.csproj

3️⃣ Project References (VERY IMPORTANT)
Purpose

Enforces Clean Architecture dependency rules.

dotnet add src/LMSBackend.API reference src/LMSBackend.Application
dotnet add src/LMSBackend.API reference src/LMSBackend.Infrastructure

dotnet add src/LMSBackend.Application reference src/LMSBackend.Domain

dotnet add src/LMSBackend.Infrastructure reference src/LMSBackend.Application
dotnet add src/LMSBackend.Infrastructure reference src/LMSBackend.Domain


Dependency flow:

API → Application → Domain
API → Infrastructure → Application

4️⃣ Install NuGet Packages (NET 8)
🧠 Application (CQRS core)
dotnet add src/LMSBackend.Application package MediatR
dotnet add src/LMSBackend.Application package FluentValidation
dotnet add src/LMSBackend.Application package FluentValidation.DependencyInjectionExtensions
dotnet add src/LMSBackend.Application package AutoMapper
dotnet add src/LMSBackend.Application package AutoMapper.Extensions.Microsoft.DependencyInjection


Purpose

MediatR → CQRS (Commands / Queries)

FluentValidation → Input validation

AutoMapper → DTO ↔ Entity mapping

🏗 Infrastructure (Database)
dotnet add src/LMSBackend.Infrastructure package Microsoft.EntityFrameworkCore --version 8.0.4
dotnet add src/LMSBackend.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.4
dotnet add src/LMSBackend.Infrastructure package Microsoft.EntityFrameworkCore.Design --version 8.0.4
dotnet add src/LMSBackend.Infrastructure package Microsoft.EntityFrameworkCore.Tools --version 8.0.4
dotnet add src/LMSBackend.Infrastructure package Dapper


Purpose

EF Core → Writes / transactions

Dapper → Fast reads (CQRS Queries)

🌐 API (Web concerns)
dotnet add src/LMSBackend.API package Swashbuckle.AspNetCore
dotnet add src/LMSBackend.API package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add src/LMSBackend.API package Serilog.AspNetCore
dotnet add src/LMSBackend.API package Newtonsoft.Json


Purpose

Swagger → API docs

JWT → Authentication

Serilog → Logging

5️⃣ Dependency Injection Setup
Application DI

📍 LMSBackend.Application/Common/DependencyInjection.cs

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LMSBackend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(cfg =>
            cfg.AddMaps(Assembly.GetExecutingAssembly()));

        return services;
    }
}


Purpose
Registers:

CQRS handlers

Validators

AutoMapper profiles

Infrastructure DI

📍 LMSBackend.Infrastructure/DependencyInjection.cs

using LMSBackend.Application.Common.Interfaces;
using LMSBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMSBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}


Purpose
Registers:

EF Core DbContext

Repositories / Unit of Work

6️⃣ Core Infrastructure Classes
DbContext

📍 Infrastructure/Persistence/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
}

Unit of Work

📍 Application/Common/Interfaces/IUnitOfWork.cs

namespace LMSBackend.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}


📍 Infrastructure/Persistence/UnitOfWork.cs

using LMSBackend.Application.Common.Interfaces;

namespace LMSBackend.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}

7️⃣ Wire Everything in API

📍 LMSBackend.API/Program.cs

using LMSBackend.Application;
using LMSBackend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

8️⃣ Test Projects (NET 8)
cd tests
dotnet new xunit -n LMSBackend.Application.Tests -f net8.0
dotnet new xunit -n LMSBackend.API.Tests -f net8.0
cd ..


Add to solution:

dotnet sln add tests/LMSBackend.Application.Tests
dotnet sln add tests/LMSBackend.API.Tests


Install packages:

dotnet add tests/LMSBackend.Application.Tests package Moq
dotnet add tests/LMSBackend.Application.Tests package FluentAssertions

dotnet add tests/LMSBackend.API.Tests package FluentAssertions
dotnet add tests/LMSBackend.API.Tests package Microsoft.AspNetCore.Mvc.Testing

9️⃣ Final Build Check
dotnet clean
dotnet build
dotnet test

🧠 Architecture Summary
Layer	Responsibility
API	HTTP, Auth, Swagger
Application	CQRS logic
Domain	Business rules
Infrastructure	DB, EF, Dapper
Tests	Verification