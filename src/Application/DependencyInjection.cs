using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        builder.Services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        return builder;
    }
}
