namespace Sagara.Core.Logging.Serilog;

/// <summary>
/// Constants to use when configuring Serilog enrichers.
/// </summary>
public static class LoggerEnrichmentProperties
{
    /// <summary>
    /// Use the assembly name as a Serilog enrichment property.
    /// </summary>
    public const string Assembly = "Assembly";
}
