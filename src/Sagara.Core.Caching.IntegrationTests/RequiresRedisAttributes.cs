namespace Sagara.Core.Caching.IntegrationTests;

/// <summary>
/// Skips the test when <c>REDIS_CONNECTION_STRING</c> is not set.
/// </summary>
internal sealed class RequiresRedisFactAttribute : FactAttribute
{
    public RequiresRedisFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING")))
        {
            Skip = "REDIS_CONNECTION_STRING is not set.";
        }
    }
}

/// <summary>
/// Skips the theory when <c>REDIS_CONNECTION_STRING</c> is not set.
/// </summary>
internal sealed class RequiresRedisTheoryAttribute : TheoryAttribute
{
    public RequiresRedisTheoryAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING")))
        {
            Skip = "REDIS_CONNECTION_STRING is not set.";
        }
    }
}

/// <summary>
/// Skips the test when <c>REDIS_CONNECTION_STRING</c> is not set, or when
/// <c>REDIS_ALLOW_FLUSH</c> is not <c>true</c>.
/// </summary>
internal sealed class RequiresRedisFlushFactAttribute : FactAttribute
{
    public RequiresRedisFlushFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING")))
        {
            Skip = "REDIS_CONNECTION_STRING is not set.";
        }
        else if (!string.Equals(
            Environment.GetEnvironmentVariable("REDIS_ALLOW_FLUSH"),
            "true",
            StringComparison.OrdinalIgnoreCase))
        {
            Skip = "REDIS_ALLOW_FLUSH is not set to 'true'. Skipping destructive test.";
        }
    }
}
