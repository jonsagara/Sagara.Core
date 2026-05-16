using Microsoft.Extensions.Configuration;

namespace Sagara.Core.Caching.IntegrationTests.Infrastructure;

/// <summary>
/// Provides configuration values for the integration test project, loaded from
/// <c>appsettings.json</c> (local dev) and environment variables (CI/CD).
/// </summary>
/// <remarks>
/// The environment variable convention uses <c>__</c> as a section separator, so
/// <c>ConnectionStrings__Redis</c> maps to <c>ConnectionStrings:Redis</c>.
/// </remarks>
internal static class TestConfiguration
{
    private static readonly Lazy<IConfiguration> _config = new(BuildConfiguration);

    /// <summary>
    /// The Redis connection string read from <c>ConnectionStrings:Redis</c> in configuration,
    /// or <see langword="null"/> if not set.
    /// </summary>
    public static string? RedisConnectionString => _config.Value.GetConnectionString("Redis");

    /// <summary>
    /// <see langword="true"/> when <c>REDIS_ALLOW_FLUSH</c> is set to <c>true</c> in
    /// <c>appsettings.json</c> (local dev) or the <c>REDIS_ALLOW_FLUSH</c> environment
    /// variable (CI/CD); <see langword="false"/> otherwise.
    /// </summary>
    public static bool RedisAllowFlush =>
        string.Equals(_config.Value["REDIS_ALLOW_FLUSH"], "true", StringComparison.OrdinalIgnoreCase);

    private static IConfiguration BuildConfiguration() =>
        new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
}
