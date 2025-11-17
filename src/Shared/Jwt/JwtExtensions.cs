using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using Microsoft.IdentityModel.Tokens;
using Shared.Web;
using System.Text;

namespace Shared.Jwt;

public static class JwtExtensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        var jwtOptions = services.GetOptions<Identity>("Identity");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = jwtOptions.Authority;
            options.Audience = jwtOptions.Audience;
            options.RequireHttpsMetadata = false; // required https

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = [jwtOptions.Authority],
                ValidateAudience = true,
                ValidAudiences = [jwtOptions.Audience],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(2), // Reduce default clock skew

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),

                NameClaimType = "name", // map claim, User.Identity.Name = "name"
                RoleClaimType = "role", // map claim, User.Identity.Role = "role"
            };

            options.MapInboundClaims = false;
        });

        services.AddAuthorization(
            options =>
            {
                // Role-bases
                options.AddPolicy(
                    IdentityConstant.Role.Admin,
                    x =>
                    {
                        x.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                        x.RequireRole(IdentityConstant.Role.Admin); 
                    }
                );
                options.AddPolicy(
                    IdentityConstant.Role.User,
                    x =>
                    {
                        x.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                        x.RequireRole(IdentityConstant.Role.User);
                    }
                );
            });

        return services;
    }
}
