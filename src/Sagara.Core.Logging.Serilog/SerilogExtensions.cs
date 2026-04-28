using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sagara.Core.Logging.Serilog;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace Sagara.Core.Logging.Serilog;

public static class SerilogExtensions
{
    /// <summary>
    /// <para>Configure Serilog and Microsoft.Extensions.Logging (MEL) for Azure App Service and dotnet-isolated Azure Function so that:</para>
    /// <list type="bullet">
    /// <item>
    /// <description>Serilog.ILogger log events are forwarded to all MEL providers EXCEPT SerilogLoggerProvider.</description>
    /// </item>
    /// <item>
    /// <description>MEL log events are forwarded to all of Serilog's sinks.</description>
    /// </item>
    /// <item>
    /// <description>Both Serilog.ILogger and MEL write log events to Azure Monitor (via OTel) without duplicates, and are viewable in Application Insights.</description>
    /// </item>
    /// </list>
    /// <para>See also: <see cref="AddNonSerilogMELProvidersToSerilog(IHost, AddSerilogToExistingILoggerFactoryResult)"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>NOTE: this is intended for Azure App Service and dotnet-isolated Azure Functions.</para>
    /// </remarks>
    /// <param name="builder">The application builder. The type is not enforced so that we don't have to take a dependency
    /// on the Azure Functions SDK, but it should be either a WebApplicationBuilder or a FunctionsApplicationBuilder.</param>
    /// <param name="createSerilogLoggerConfiguration">The caller-provided a function that creates a Serilog LoggerConfiguration.</param>
    /// <param name="additionalWriteToProvidersInclusionPredicate">An optional predicate to add additional conditions for forwarding Serilog LogEvents
    /// to MEL providers via WriteTo.Providers(). By default, only events with the "DirectSerilog" property are forwarded. If provided, this predicate's 
    /// return value will be ANDed together with the check for existence of the "DirectSerilog" property.</param>
    /// <returns>A result containing the empty collection of other MEL providers. The caller must populate it after building the host.</returns>

    public static AddSerilogToExistingILoggerFactoryResult AddSerilogToExistingILoggerFactory(this IHostApplicationBuilder builder, 
        Func<LoggerConfiguration> createSerilogLoggerConfiguration, Func<LogEvent, bool>? additionalWriteToProvidersInclusionPredicate = null)
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
                .Filter.ByIncludingOnly(le =>
                {
                    // ONLY send log events that originated from Serilog.ILogger.
                    var isFromSerilog = le.Properties.ContainsKey(directSerilogPropertyName);

                    // If the caller sent an additional predicate, apply that as well. The prime example of using this is to exclude
                    //   "bad API middleware" Serilog logs from being forwarded to MEL, which probably get us blocked from using
                    //   Application Insights.
                    // If the user didn't send an additional predicate, default to true so we don't automatically discard the event.
                    var additionalPredicateResult = additionalWriteToProvidersInclusionPredicate?.Invoke(le) ?? true;

                    return isFromSerilog && additionalPredicateResult;
                })
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
    /// <para>After the host is built, retrieve all non-<see cref="SerilogLoggerProvider"/> <see cref="ILoggerProvider"/> instances
    /// and add them to Serilog's "write to" providers retuned by <see cref="AddSerilogToExistingILoggerFactory(IHostApplicationBuilder, Func{LoggerConfiguration}, Func{LogEvent, bool}?)"/>. 
    /// Serilog will forward all directly written Serilog.ILogger log entries to the registered MEL providers.</para>
    /// </summary>
    /// <param name="host">The Azure App Service or dotnet-isolated Azure Function's <see cref="IHost"/> instance.</param>
    /// <param name="addSerilogResult">A collection of concrete <see cref="ILoggerProvider"/> instances that were 
    /// registered with the host, excluding <see cref="SerilogLoggerProvider"/>.</param>
    public static void AddNonSerilogMELProvidersToSerilog(this IHost host, AddSerilogToExistingILoggerFactoryResult addSerilogResult)
    {
        Check.ThrowIfNull(host);
        Check.ThrowIfNull(addSerilogResult);

        // After building the host, we can pull out any MEL ILoggerProviders that were registered and add them to the
        //   LoggerProviderCollection (except SerilogLoggerProvider, which causes circular references and crashes the process).
        //   This is what allows Serilog to write to any MEL providers, including to Azure Monitor for viewing in Application Insights.
        var loggerProviders = host.Services
            .GetServices<ILoggerProvider>()
            .Where(lp => lp.GetType() != typeof(SerilogLoggerProvider))
            .ToArray();

        foreach (var loggerProvider in loggerProviders)
        {
            addSerilogResult.NonSerilogMELProviders.AddProvider(loggerProvider);
        }
    }

    /// <summary>
    /// Sets Serilog as the logging provider.
    /// </summary>
    /// <remarks>
    /// <para>This is (hopefully) a temporary replacement for <c>Serilog.Extensions.Hosting.UseSerilog</c>. It's necessary
    /// to support working with the new <c>Host.CreateApplicationBuilder()</c> pattern being rolled out in .NET 7, which 
    /// does not currently support the <c>IHostBuilder</c> interface.</para>
    /// <para>See: https://github.com/dotnet/runtime/discussions/81090#discussioncomment-4784551</para>
    /// </remarks>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to configure.</param>
    /// <param name="configureLogger">The delegate for configuring the Serilog.LoggerConfiguration that will be used 
    /// to construct a Serilog.Core.Logger.</param>
    /// <param name="preserveStaticLogger">Indicates whether to preserve the value of Serilog.Log.Logger.</param>
    /// <param name="writeToProviders">By default, Serilog does not write events to Microsoft.Extensions.Logging.ILoggerProviders 
    /// registered through the Microsoft.Extensions.Logging API. Normally, equivalent Serilog sinks are used in place of providers. 
    /// Specify true to write events to all providers.</param>
    /// <returns>The host application builder.</returns>
    [Obsolete($"Obsolete. Use {nameof(AddSerilogToExistingILoggerFactory)} instead. It's additive instead of overwriting the existing ILoggerFactory.")]
    public static IHostApplicationBuilder UseSerilog(this IHostApplicationBuilder builder, Action<IHostApplicationBuilder, IServiceProvider, LoggerConfiguration> configureLogger, bool preserveStaticLogger = false, bool writeToProviders = false)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configureLogger);

        builder.Services.AddSerilog(
            (serviceProvider, loggerConfiguration) => configureLogger(builder, serviceProvider, loggerConfiguration),
            preserveStaticLogger: preserveStaticLogger,
            writeToProviders: writeToProviders
            );

        return builder;
    }
}
