namespace Microsoft.Extensions.DependencyInjection;

public static class LoggingExtentions
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        return services;
    }
}
