using Api.Extensions;
using Api.Services;
using Application.Common.Interfaces;
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

        //builder.Services.AddHostedService<GracePeriodBackgroundService>();

        return builder;
    }

    public static WebApplication UseWebServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDefaultOpenApi();
        }
        app.UseRouting();   
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapControllers();
        app.MapDefaultEndpoints();

        return app;
    }
}
