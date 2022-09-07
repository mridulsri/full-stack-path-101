using Microsoft.EntityFrameworkCore;
using App.Microservices.Products.Persistence;
using MediatR;
using System.Reflection;
using MassTransit;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{
    public static IServiceCollection AddDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        string sqlConnString = Environment.GetEnvironmentVariable("SqlServer") ?? configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrWhiteSpace(sqlConnString))
        {
            string sqliteConnString = "DataSource=products.db";
#if DEBUG
            sqliteConnString = configuration.GetConnectionString("sqlite");
#endif
            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlite(sqliteConnString,
                    b => b.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName)

                ));
        }
        else
        {
            services.AddDbContextPool<ProductDbContext>(options =>
            {
                options.UseSqlServer(sqlConnString, sql =>
                {
                    sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    sql.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName);
                });

            }, poolSize: 2000).AddTransient<ProductDbContext>();
        }
        return services;
    }
    public static IApplicationBuilder UseDataStore(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            try
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<ProductDbContext>())
                {
                    if (context.Database.IsSqlite() || context.Database.IsSqlServer())
                    {
                        Console.WriteLine("Start: migrating database...");
                        context.Database.Migrate();
                        Console.WriteLine("Complete: migrating database...");
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
