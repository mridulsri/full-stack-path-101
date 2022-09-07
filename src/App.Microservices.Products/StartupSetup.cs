using Microsoft.EntityFrameworkCore;
using App.Microservices.Products.Persistence;
using MediatR;
using System.Reflection;
using MassTransit;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{
    public static IServiceCollection AddProductModules(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddMassTransit(options => {
            options.UsingRabbitMq((context, cfg) => {
                cfg.Host(new Uri("rabbitmq://localhost:4001"), host => {
                    host.Username("guest");
                    host.Password("guest");
                });

            });
        });

        return services;
    }
}
