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

    private static IConfiguration BuildConfiguration() =>
        new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
}
