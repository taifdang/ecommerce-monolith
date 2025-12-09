using Api.Extensions;
using Api.Services;
using Application.Common.Interfaces;
using Infrastructure.Services;
using ServiceDefaults.OpenApi;


namespace Api;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddWebServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddJwt();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDefaultOpenApi();

        builder.Services.AddControllers();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<ICurrentUserProvider, CurrentUserProvider>();
        builder.Services.AddTransient<ICookieService, CookieService>();

        builder.Services.AddHostedService<GracePeriodBackgroundService>();

        return builder;
    }

    public static WebApplication UseWebServices(this WebApplication app)
    {  
        app.UseHttpsRedirection();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDefaultOpenApi();
        }
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapControllers();

        return app;
    }
}
