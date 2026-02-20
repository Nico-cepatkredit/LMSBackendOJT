using LMSBackend.Application.Common.Interfaces;
using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Common.Interfaces.IService;
using LMSBackend.Infrastructure.Persistence;
using LMSBackend.Infrastructure.Persistence.Contexts;
using LMSBackend.Infrastructure.Persistence.Diagnostics;
using LMSBackend.Infrastructure.Persistence.Seed;
using LMSBackend.Infrastructure.Repository;
using LMSBackend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMSBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        services.AddDbContext<LMSDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("LMSDBConnection"));

            options.EnableDetailedErrors();

            if (isDevelopment)
            {
                options.EnableSensitiveDataLogging();
            }

            options.AddInterceptors(
                sp.GetRequiredService<EfPerformanceInterceptor>());
        });


        services.AddDbContext<LOSDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("LOSDBConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ILoansTableRepository, LoansTableRepository>();
        services.AddScoped<ICRAMRepository, CRAMRepository>();
        services.AddScoped<EfPerformanceInterceptor>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IUserAccountsRepository, UserAccountsRepository>();
        services.AddScoped<DatabaseSeeder>();

        return services;
    }
}
