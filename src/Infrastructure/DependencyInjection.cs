using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using Shared.Web;

namespace Infrastructure;
//ref: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-9.0&tabs=visual-studio
public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        // Get Configuration
        var appSettings = builder.Configuration.GetOptions<AppSettings>();
        builder.Services.AddSingleton(appSettings);

        // DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(p => p.UseSqlServer(appSettings.ConnectionStrings.DefaultConnection));

        // Identity
        builder.Services.AddDbContext<AppIdentityDbContext>(p => p.UseSqlServer(appSettings.ConnectionStrings.DefaultConnection));
        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
        {
            config.Password.RequiredLength = 6;
            config.Password.RequireDigit = false;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;
        })
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();

        // Jwt
        builder.Services.AddScoped<ITokenService, TokenService>();

        // Repositores
        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Custom Servicess      
        builder.Services.AddScoped<IEmailService, EmailService>();
        //if (appSettings.FileStorageSettings.LocalStorage)
        //{
        //    builder.Services.AddSingleton<IFileService, LocalStorageService>();
        //}
        builder.Services.AddScoped<IFileService, LocalStorageService>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddScoped<IUserManagerService, UserManagerService>();

        return builder;
    }
}
