using Application.Common.Interfaces;
using Persistence.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;
using Persistence.Repositories;
using Persistence.Seed;

namespace Persistence;

public static class Extensions
{
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // Interceptors
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventIntercopter>();

        // Database Context
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(builder.Configuration.GetConnectionString("shopdb"));

            options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        // Repositories    
        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<IUnitOfWork, ApplicationDbContext>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        // Seeders    
        builder.Services.AddScoped<IDataSeeder<ApplicationDbContext>, CatalogDataSeeder>();

        return builder;
    }

    public static async Task<WebApplication> MigratePersistenceAsync(this WebApplication app)
    {
        await app.MigrationDbContextAsync<ApplicationDbContext>();
        return app;
    }
}
