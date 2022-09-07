using Microsoft.EntityFrameworkCore;
using App.Microservices.Orders.Persistence;
using MediatR;
using System.Reflection;
using MassTransit;
using App.Microservices.Orders.Consumer;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{

    public static IServiceCollection AddSqlDataBaseProvider(this IServiceCollection services, IConfiguration configuration)
    {
        string sqlConnString = Environment.GetEnvironmentVariable("SqlServer") ?? configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrWhiteSpace(sqlConnString))
        {
            string sqliteConnString = "DataSource=orders.db";
#if DEBUG
            sqliteConnString = configuration.GetConnectionString("sqlite");
#endif
            services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlite(sqliteConnString,
                    b => b.MigrationsAssembly(typeof(OrderDbContext).Assembly.FullName)

                ));
        }
        else
        {
            services.AddDbContextPool<OrderDbContext>(options =>
            {
                options.UseSqlServer(sqlConnString, sql =>
                {
                    sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    sql.MigrationsAssembly(typeof(OrderDbContext).Assembly.FullName);
                });

            }, poolSize: 2000).AddTransient<OrderDbContext>();
        }
        return services;
    }


    public static IApplicationBuilder UseSqlDataBaseProvider(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            try
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<OrderDbContext>())
                {
                    if (context.Database.IsSqlite() || context.Database.IsSqlServer())
                    {
                        Console.WriteLine("migrating database ...");
                        context.Database.Migrate();
                        Console.WriteLine("migrating database ... DONE");
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        return app;
    }


    public static IServiceCollection AddOrderModules(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

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
