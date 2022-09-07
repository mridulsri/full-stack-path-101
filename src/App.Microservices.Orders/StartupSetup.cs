using Microsoft.EntityFrameworkCore;
using App.Microservices.Orders.Persistence;
using MediatR;
using System.Reflection;
using MassTransit;
using App.Microservices.Orders.Consumer;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{
    public static IServiceCollection AddOrderModules(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x => {
            x.AddConsumer<ProductCreatedConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri("rabbitmq://localhost:4001"), h => {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("event-listener", e =>
                {
                    e.ConfigureConsumer<ProductCreatedConsumer>(context);
                });
            });
        });
        return services;
    }
}
