using Serilog.Extensions.Logging;

namespace Sagara.Core.Logging.Serilog.Azure.Functions;

/// <summary>
/// 
/// </summary>
/// <param name="NonSerilogMELProviders"></param>
public record ConfigureAzureFunctionsLoggingResult(
    LoggerProviderCollection NonSerilogMELProviders);
