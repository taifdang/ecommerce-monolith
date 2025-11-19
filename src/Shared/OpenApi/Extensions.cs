using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace Shared.OpenApi;

public static class Extensions
{
    public static IServiceCollection AddAspnetOpenApi(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Ecommerce API",
                Version = "v1",
            });
            // Bearer token scheme
            option.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme 
                { 
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,            
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    Description =
                        "Enter 'Bearer' [space] and your token in the text input below.\n\nExample: 'Bearer mlJhbXoiOiJIUzI1NiIsInR5cCI6IkpXKvJ9...'",
                });
            // Require
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // Api Key scheme
            //option.AddSecurityDefinition(
            //    "ApiKey",
            //    new OpenApiSecurityScheme
            //    {
            //        Name = "X-API-KEY",
            //        Type = SecuritySchemeType.ApiKey,
            //        In = ParameterLocation.Header,
            //        Description =
            //            "Enter your API key in the text input below.\n\nExample: '13123-aSFdef'",
            //    });
        });

        return services;
    }
}
