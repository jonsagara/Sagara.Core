using Sagara.Core.Caching.IntegrationTests.Infrastructure;

namespace Sagara.Core.Caching.IntegrationTests;

/// <summary>
/// Skips the test when <c>ConnectionStrings:Redis</c> is not configured.
/// </summary>
internal sealed class RequiresRedisFactAttribute : FactAttribute
{
    public RequiresRedisFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(TestConfiguration.RedisConnectionString))
        {
            Skip = "ConnectionStrings:Redis is not configured.";
        }
    }
}

/// <summary>
/// Skips the theory when <c>ConnectionStrings:Redis</c> is not configured.
/// </summary>
internal sealed class RequiresRedisTheoryAttribute : TheoryAttribute
{
    public RequiresRedisTheoryAttribute()
    {
        if (string.IsNullOrWhiteSpace(TestConfiguration.RedisConnectionString))
        {
            Skip = "ConnectionStrings:Redis is not configured.";
        }
    }
}

/// <summary>
/// Skips the test when <c>ConnectionStrings:Redis</c> is not configured, or when
/// <c>REDIS_ALLOW_FLUSH</c> is not <c>true</c>.
/// </summary>
internal sealed class RequiresRedisFlushFactAttribute : FactAttribute
{
    public RequiresRedisFlushFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(TestConfiguration.RedisConnectionString))
        {
            Skip = "ConnectionStrings:Redis is not configured.";
        }
        else if (!TestConfiguration.RedisAllowFlush)
        {
            Skip = "REDIS_ALLOW_FLUSH is not set to 'true'. Skipping destructive test.";
        }
    }
}
