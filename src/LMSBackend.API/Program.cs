using System.Text.Json.Serialization;
using LMSBackend.API.Common.Filters;
using LMSBackend.API.Middlewares;
using LMSBackend.Application;
using LMSBackend.Infrastructure;
using LMSBackend.Infrastructure.Persistence.Contexts;
using LMSBackend.Infrastructure.Persistence.Seed;
using LMSBackend.API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVite",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:5174",
                    "http://localhost:5173"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();

    logging.AddSimpleConsole(options =>
    {
        options.IncludeScopes = true;
        options.SingleLine = false;
        options.TimestampFormat = "HH:mm:ss ";
    });

    // logging.AddFilter("Microsoft", LogLevel.Warning);
    // logging.AddFilter("LMSBackend", LogLevel.Information);
});
// Controllers (IMPORTANT)
builder.Services.AddControllers(options =>
{
    options.Filters.Add<APIResponseFilter>();
    options.Filters.Add<APIExceptionFilter>();
})
.AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddHttpClient();
builder.Services.AddJwtAuthentication(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CQRS + Infrastructure DI
var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, isDevelopment);

var app = builder.Build();

app.UseCors("AllowVite");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LMSDbContext>();
    await db.Database.OpenConnectionAsync();
    await db.Database.CloseConnectionAsync();
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
