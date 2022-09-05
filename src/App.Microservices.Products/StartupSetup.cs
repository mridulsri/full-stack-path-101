using Microsoft.EntityFrameworkCore;
using App.Microservices.Products.Persistence;
using MediatR;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{
    public static IServiceCollection AddDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        string sqlConnString = Environment.GetEnvironmentVariable("SqlServer") ?? configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrWhiteSpace(sqlConnString))
        {
            string sqliteConnString = configuration.GetConnectionString("sqlite");
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
        return services;
    }
}
