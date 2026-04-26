using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace Sagara.Core.Logging.Serilog.Azure.Functions;

public static class SerilogAzureFunctionsExtensions
{
    public static ConfigureAzureFunctionsLoggingResult ConfigureAzureFunctionsLogging(this IHostApplicationBuilder builder, Func<LoggerConfiguration> createSerilogLoggerConfiguration)
    {
        Check.ThrowIfNull(builder);
        Check.ThrowIfNull(createSerilogLoggerConfiguration);

        // The "DirectSerilog" property tags events that originate from Serilog directly (DI / static); WriteTo.Providers()
        //   forwards only these to other MEL providers, so MEL log events that get sent to Serilog aren't again forwarded
        //   back to MEL. This eliniates duplicates in Application Insights.
        // NOTE: the random suffix has no meaning; it' s just to ensure the property name is unique and won't collide
        //   with any user-defined properties.
        var directSerilogPropertyName = $"DirectSerilog_{RandomString.GenerateAlphanumeric(length: 16)}";

        // This collection lets Serilog write log events from Serilog.ILogger to through other dynamically-added MEL
        //   ILoggerProviders (but not SerilogLoggerProvider, as that creates circular references and crashes the app).
        // Justification: This lives for the life of the process as a registered singleton, and the process will dispose of
        //   it upon shutdown.
#pragma warning disable CA2000 // Unnecessary assignment of a value
        var nonSerilogMELProviders = new LoggerProviderCollection();
#pragma warning restore CA2000 // Unnecessary assignment of a value

        // Always write Serilog.ILogger log evnets to all other non-Serilog ILoggerProviders (most importantly, OTel).
        //   For logs that originate from MEL (and thus don't have the "DirectSerilog" property), we assume they've
        //   already reached OTel, and we don't forward them again.
        var underlyingLogger = createSerilogLoggerConfiguration()
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(le => le.Properties.ContainsKey(directSerilogPropertyName))
                .WriteTo.Providers(nonSerilogMELProviders)
            )
            .CreateLogger();

        // Create a Serilog logger that has the "DirectSerilog" property, so that the bridge to OTel can identify
        //   events that originate from Serilog directly (DI / static).
        // This is also the Serilog.ILogger instance that register with DI for injecting, and also for using as 
        //   static logging properties via Log.Logger.ForContext<T>(). The "DirectSerilog" property will be included on
        //   all logs from that logger, so they will be forwarded to OTel and thus to Azure Monitor / App Insights.
        //   Logs that don't have the "DirectSerilog" property are assumed to have come through MEL (and thus already
        //   reached OTel via OpenTelemetryLoggerProvider) and are not forwarded again to avoid double-writing in Azure
        //   Monitor / App Insights.
        // "true" has no meaning. We only care whether or not the property is present on the LogEvent.
        Log.Logger = underlyingLogger.ForContext(propertyName: directSerilogPropertyName, value: true);

        // Now that we have the underlying Serilog logger, we can add the SerilogLoggerProvider to MEL, which allows a
        //   MEL ILogger<T> to write to Serilog's non-OTel sinks (i.e., Files).
        // We can't call builder.Services.AddSerilog() or builder.Host.UseSerilog() because that would overwrite the
        //   Functions host's ILoggerFactory and thus break communication between the host and the worker.
        builder.Logging.AddSerilog(underlyingLogger, dispose: true);

        // Register the collection of other MEL providers and the Serilog logger itself for DI. Serilog needs the providers
        //   collection to know what to forward to.
        builder.Services.AddSingleton(nonSerilogMELProviders);

        // Log.Logger is what will be used for static logging via Log.ForContext<T>().
        builder.Services.AddSingleton(Log.Logger);

        // Return the collection of non-Serilog MEL providers so that the caller can populate it after building the host.
        return new(NonSerilogMELProviders: nonSerilogMELProviders);
    }

    /// <summary>
    /// <para>After the host is build, retrieve all non-<see cref="SerilogLoggerProvider"/> <see cref="ILoggerProvider"/> instances
    /// and register them with Serilog. This enables Serilog to forward all Serilog.ILogger log entries to the registered
    /// MEL providers.</para>
    /// </summary>
    /// <param name="host">The dotnet-isolated Azure Function's <see cref="IHost"/> instance.</param>
    /// <param name="configureAzureFuncLoggingResult">The result of configuring Azure Functions logging for Serilog and MEL.</param>
    public static void RegisterNonSerilogMELProvidersWithSerilog(this IHost host, ConfigureAzureFunctionsLoggingResult configureAzureFuncLoggingResult)
    {
        Check.ThrowIfNull(host);
        Check.ThrowIfNull(configureAzureFuncLoggingResult);

        // After building the host, we can pull out any MEL ILoggerProviders that were registered and add them to the
        //   LoggerProviderCollection (except SerilogLoggerProvider, which causes circular references and crashes the process).
        //   This is what allows Serilog to write to any MEL providers, including to Azure Monitor for viewing in Application Insights.
        var loggerProviders = host.Services
            .GetServices<ILoggerProvider>()
            .Where(lp => lp.GetType() != typeof(SerilogLoggerProvider))
            .ToArray();

        foreach (var loggerProvider in loggerProviders)
        {
            configureAzureFuncLoggingResult.NonSerilogMELProviders.AddProvider(loggerProvider);
        }
    }
}
