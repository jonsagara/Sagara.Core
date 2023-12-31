﻿using Microsoft.Extensions.Hosting;
using Serilog;

namespace Sagara.Core.Logging.Serilog;

public static class SerilogExtensions
{
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
