using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace Sagara.Core.Logging.Serilog;

/// <summary>
/// <para>The result of configuring an Azure App Service or dotnet-isolated Azure Function to support logging with both 
/// Serilog and Microsoft.Extensions.Logging (MEL).</para>
/// <para>Initially, the collections is empty. After building the host, the caller MUST call 
/// <see cref="SerilogExtensions.AddNonSerilogMELProvidersToSerilog(IHost, AddSerilogToExistingILoggerFactoryResult)"/>
/// to populate it.</para>
/// </summary>
/// <param name="NonSerilogMELProviders">The concrete <see cref="ILoggerProvider"/> instances retrieved from the 
/// <see cref="IHost"/> instance.</param>
public record AddSerilogToExistingILoggerFactoryResult(
    LoggerProviderCollection NonSerilogMELProviders);
