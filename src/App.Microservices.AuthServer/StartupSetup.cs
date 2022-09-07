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
