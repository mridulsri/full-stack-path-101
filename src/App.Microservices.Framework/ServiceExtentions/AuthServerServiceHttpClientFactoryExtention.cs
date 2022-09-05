using App.Microservices.Framework.ServiceExtentions.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace App.Microservices.Framework.ServiceExtentions;

public static class AuthServerServiceHttpClientFactoryExtention
{

    public static IServiceCollection AddAuthServerService(this IServiceCollection services, ClientServiceConfiguration serviceConfig)
    {
        // Generic client
        // Named client
        services.AddHttpClient("AuthServerService", cfg =>
        {
            cfg.BaseAddress = serviceConfig.BaseAddress;
            cfg.DefaultRequestHeaders.Add("Authorization", serviceConfig.Key);
            cfg.DefaultRequestHeaders.Add("x-api-version", "1");

        }).AddHttpMessageHandler<AuthServiceHttpClientInterceptor>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(request =>
            {
                return request.Method == HttpMethod.Get ? GetRetryPolicy() : GetNoOpPolicy();
            });
            //.ConfigurePrimaryHttpMessageHandler(() => GetCertificateHandler());

        // typed client
        return services;
    }


    private static IAsyncPolicy<HttpResponseMessage> GetNoOpPolicy()
    {
        return Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));

    }

    //private static IAsyncPolicy GetCircuitBreakerPolicy()
    //{
    //    return (IAsyncPolicy)HttpPolicyExtensions
    //        .HandleTransientHttpError()
    //        .CircuitBreakerAsync();
    //}

    private static HttpClientHandler GetCertificateHandler()
    {
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ClientCertificates.Add(default);
        return httpClientHandler;

    }

}


public class ClientServiceConfiguration
{
    public Uri BaseAddress { get; set; }
    public string Key { get; set; }
}