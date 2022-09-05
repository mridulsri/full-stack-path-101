using Microsoft.EntityFrameworkCore;
using App.Microservices.AuthServer.Persistence;
using App.Microservices.AuthServer.Services.Authenticators;
using App.Microservices.AuthServer.Services.RefreshTokenRepositories;
using App.Microservices.AuthServer.Services.TokenGenerators;
using App.Microservices.AuthServer.Services.TokenValidators;
using System.Reflection;
using MediatR;
using App.Application.Interfaces;
using App.Infrastructure.Cryptography;
using App.Microservices.Framework.ConfigOptions;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{
    public static IServiceCollection AddDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        string sqlConnString = Environment.GetEnvironmentVariable("SqlServer") ?? configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrWhiteSpace(sqlConnString))
        {
            string sqliteConnString = configuration.GetConnectionString("sqlite");
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlite(sqliteConnString,
                    b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)

                ));
        }
        else
        {
            services.AddDbContextPool<AuthDbContext>(options =>
            {
                options.UseSqlServer(sqlConnString, sql =>
                {
                    sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    sql.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName);
                });

            }, poolSize: 2000).AddTransient<AuthDbContext>();
        }
        return services;
    }

    public static IApplicationBuilder UseDataStore(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            try
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<AuthDbContext>())
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
    public static IServiceCollection AddAuthModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient<IPasswordHasher, PasswordHasher>();
        AuthenticationOption authenticationOption = new AuthenticationOption();
        configuration.Bind(AuthenticationOption.Authentication, authenticationOption);
        services.AddSingleton(authenticationOption);
        // https://quizdeveloper.com/faq/aspdotnet-core-some-services-are-not-able-to-be-constructed-aid1205
        services.AddSingleton<AccessTokenGenerator>();
        services.AddSingleton<RefreshTokenGenerator>();
        services.AddSingleton<RefreshTokenValidator>();
        services.AddScoped<Authenticator>();
        services.AddSingleton<TokenGenerator>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }

    #region GrpcServices

    public static IApplicationBuilder UseGrpcSerices(this IApplicationBuilder app)
    {

        return app;
    }
    #endregion

}
