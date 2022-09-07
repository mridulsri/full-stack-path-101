using App.Infrastructure.Logging.ConfigOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Logging;


public static class LoggingExtentions
{
    public static IHostBuilder UseAppLogger(this IHostBuilder builder, IConfiguration configuration)
    {
        builder.ConfigureLogging((ctx, logging) =>
        {
            logging.AddSerilog();
            var options = new LoggingOptions();
            configuration.Bind(LoggingOptions.Logging, options);
            
            var env = ctx.HostingEnvironment;
            var logsPath = Environment.GetEnvironmentVariable("ASPNETCORE_LOGPATH") ?? Path.Combine(env.ContentRootPath, "logs");

            if (!Directory.Exists(logsPath))
                Directory.CreateDirectory(logsPath);

            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            var loggerConfiguration = new LoggerConfiguration();

            loggerConfiguration = loggerConfiguration
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.File(Path.Combine(logsPath, "log.txt"),
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] [TraceId: {TraceId}] {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: options.File.MinimumLogEventLevel);

            Log.Logger = loggerConfiguration.CreateLogger();

        });

        return builder;
    }


}
