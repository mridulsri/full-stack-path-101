using Microsoft.AspNetCore.Authentication.JwtBearer;
using App.Microservices.Framework.Jwt;
using Hellang.Middleware.ProblemDetails;
using App.Microservices.Framework.Exceptions;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Reflection;
using FluentValidation;
using App.Application.Interfaces;
using App.Microservices.Framework.Services;
using App.Microservices.Framework.ConfigOptions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        services.AddAuthProcess(configuration);
        #endregion

        #region Swagger api-docs

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerApiDocs(versionId:"v1", title: "Developer API docs");

        #endregion

        var executingAssembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(executingAssembly);
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IApplicationBuilder UseServiceFramework(this IApplicationBuilder app)
    {
        app.UseSwaggerApiDocs();
        return app;

    }

    public static IServiceCollection AddAuthProcess(this IServiceCollection services, IConfiguration configuration)
    {
        AuthenticationOption authenticationOption = new AuthenticationOption();
        configuration.Bind(AuthenticationOption.Authentication, authenticationOption);
        services.AddSingleton(authenticationOption);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(cfg => {
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

    
}
