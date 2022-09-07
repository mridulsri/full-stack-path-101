using Microsoft.AspNetCore.Authentication.JwtBearer;
using Hellang.Middleware.ProblemDetails;
using App.Microservices.Framework.Exceptions;
using MediatR;
using System.Reflection;
using FluentValidation;
using App.Application.Interfaces;
using App.Microservices.Framework.Services;
using App.Microservices.Framework.ConfigOptions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{

    public static IServiceCollection AddServiceFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IDateTimeService, DateTimeService>();

        #region ProblemDetails        
        services.AddProblemDetails(setup =>
        {
            // Control when an exception is included
            setup.IncludeExceptionDetails = (ctx, ex) =>
            {
                // Fetch services from HttpContext.RequestServices
                var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                return env.IsDevelopment() || env.IsStaging();
            };

            setup.Map<CustomException>(exception => new CustomProblemDetails
            {
                Title = exception.Title,
                Detail = exception.Detail,
                Status = StatusCodes.Status500InternalServerError,
                Type = exception.Type,
                Instance = exception.Instance,
                AdditionalInfo = exception.AdditionalInfo
            });
        });
        #endregion

        #region Jwt-Authentication
        services.AddAuthConfiguration(configuration);
        #endregion

        #region Swagger api-docs

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerApiDocs(versionId: "v1", title: "Developer API docs");

        #endregion

        var executingAssembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(executingAssembly);
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IApplicationBuilder UseServiceFramework(this IApplicationBuilder app)
    {
        app.UseSwaggerApiDocs();
        // return static content
        /*
        app.Use(async (context, next) =>
        {
            if (!context.Request.Path.Value.Contains("api"))
            {
                var parantDirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
                var indexFile = Path.Combine(parantDirInfo.Parent.FullName, "App.WebHosts", "wwwroot", "index.html");
                await context.Response.SendFileAsync(indexFile);
            }
            else
                await next.Invoke();
        });
        */
        return app;
    }

    public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        AuthenticationOption authenticationOption = new AuthenticationOption();
        configuration.Bind(AuthenticationOption.Authentication, authenticationOption);
        services.AddSingleton(authenticationOption);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(cfg =>
        {
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOption.AccessTokenSecret)),
                ValidIssuer = authenticationOption.Issuer,
                ValidAudience = authenticationOption.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }


    public static IServiceCollection AddDataStore<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
    {
        var provider = configuration.GetValue("Provider", "sqlite");
        services.AddDbContextPool<TDbContext>(options =>
        {
            switch (provider)
            {
                case "SqlServer":
                    {
                        string sqlConnString = Environment.GetEnvironmentVariable("ASPNETCORE_SQLSERVER") ?? configuration.GetConnectionString("SqlServer");
                        options.UseSqlServer(sqlConnString, sql =>
                        {
                            sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                            sql.MigrationsAssembly(typeof(TDbContext).Assembly.FullName);
                        });
                        break;
                    }
                default:
                    {
                        string sqliteConnString = "DataSource=app.db";
#if DEBUG
                        sqliteConnString = configuration.GetConnectionString("sqlite");
#endif
                        options.UseSqlite(sqliteConnString, sql =>
                        {
                            sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                            sql.MigrationsAssembly(typeof(TDbContext).Assembly.FullName);
                        });
                        break;
                    }
            }
        }, poolSize: 2000).AddTransient<TDbContext>();

        return services;
    }

    public static IApplicationBuilder UseDataStoreMigration<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            try
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<TDbContext>())
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
