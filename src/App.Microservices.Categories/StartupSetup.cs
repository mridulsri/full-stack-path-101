using App.Microservices.Categories.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{
    public static IServiceCollection AddDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        string sqlConnString = Environment.GetEnvironmentVariable("SqlServer") ?? configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrWhiteSpace(sqlConnString))
        {
            string sqliteConnString = "DataSource=categories.db";
#if DEBUG
            sqliteConnString = configuration.GetConnectionString("sqlite");
#endif
            services.AddDbContext<CategoryDbContext>(options =>
                options.UseSqlite(sqliteConnString,
                    b => b.MigrationsAssembly(typeof(CategoryDbContext).Assembly.FullName)

                ));
        }
        else
        {
            services.AddDbContextPool<CategoryDbContext>(options =>
            {
                options.UseSqlServer(sqlConnString, sql =>
                {
                    sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    sql.MigrationsAssembly(typeof(CategoryDbContext).Assembly.FullName);
                });

            }, poolSize: 2000).AddTransient<CategoryDbContext>();
        }
        return services;
    }

    public static IApplicationBuilder UseDataStore(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            try
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<CategoryDbContext>())
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
}
