using Application;
using Shared.Constants;
using Shared.Web;
using System.Text.Json.Serialization;

namespace Api;

public static class ConfigureService
{
    public static IServiceCollection AddWebAPIService(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpContextAccessor();

        services.AddTransient<ICurrentUserProdvider, CurrentUserProvider>();

        services.AddAutoMapper(typeof(Program).Assembly);

        services.AddControllers()
            .AddJsonOptions(opt =>
            {
                // Handle reference loops
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opt.JsonSerializerOptions.WriteIndented = true;
            });

        return services;
    }
}
