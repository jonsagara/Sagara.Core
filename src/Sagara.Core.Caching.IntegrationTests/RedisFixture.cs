using Microsoft.Extensions.Logging.Abstractions;
using Sagara.Core.Caching.IntegrationTests.Infrastructure;
using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests;

/// <summary>
/// Shared xunit collection fixture that owns live <see cref="RedisCache"/> and
/// <see cref="RedisAdminCache"/> connections for the duration of the test run.
/// </summary>
/// <remarks>
/// Configure the Redis connection string via <c>ConnectionStrings:Redis</c> in
/// <c>appsettings.json</c> (local dev) or the <c>ConnectionStrings__Redis</c> environment
/// variable (CI/CD). If the value is absent or the server is unreachable, all tests skip
/// gracefully.
/// </remarks>
public sealed class RedisFixture : IAsyncLifetime
{
    private RedisCache? _resp2Cache;
    private RedisCache? _resp3Cache;
    private RedisAdminCache? _adminCache;

    /// <summary>
    /// <see langword="true"/> when <c>ConnectionStrings:Redis</c> is configured and a PING to
    /// the server succeeded; <see langword="false"/> otherwise.
    /// </summary>
    public bool IsAvailable { get; private set; }

    /// <summary>
    /// Returns the <see cref="RedisCache"/> instance configured for the given protocol.
    /// </summary>
    public RedisCache GetCache(RedisProtocol protocol) => protocol switch
    {
        RedisProtocol.Resp2 => _resp2Cache
            ?? throw new InvalidOperationException("Fixture not initialized."),
        RedisProtocol.Resp3 => _resp3Cache
            ?? throw new InvalidOperationException("Fixture not initialized."),
        _ => throw new ArgumentOutOfRangeException(nameof(protocol), protocol, null),
    };

    /// <summary>
    /// Returns the <see cref="RedisAdminCache"/> instance (RESP2) for admin-level tests.
    /// </summary>
    public RedisAdminCache AdminCache =>
        _adminCache ?? throw new InvalidOperationException("Fixture not initialized.");

    /// <inheritdoc/>
    public async ValueTask InitializeAsync()
    {
        var rawConnStr = TestConfiguration.RedisConnectionString;
        if (string.IsNullOrWhiteSpace(rawConnStr))
        {
            IsAvailable = false;
            return;
        }

        // Ensure abortConnect=false so connection failures are non-fatal.
        var connStr = rawConnStr.Contains("abortConnect", StringComparison.OrdinalIgnoreCase)
            ? rawConnStr
            : $"{rawConnStr},abortConnect=false";

        try
        {
            _resp2Cache = new RedisCache(
                NullLogger<RedisCache>.Instance, connStr, RedisProtocol.Resp2, allowAdmin: false);
            _resp3Cache = new RedisCache(
                NullLogger<RedisCache>.Instance, connStr, RedisProtocol.Resp3, allowAdmin: false);
            _adminCache = new RedisAdminCache(
                NullLogger<RedisAdminCache>.Instance, connStr, RedisProtocol.Resp2);

            // Verify connectivity with a round-trip to the server.
            await _resp2Cache.PingAsync();

            IsAvailable = true;
        }
        catch (Exception)
        {
            IsAvailable = false;
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (_resp2Cache is not null)
        {
            await _resp2Cache.DisposeAsync();
        }

        if (_resp3Cache is not null)
        {
            await _resp3Cache.DisposeAsync();
        }

        if (_adminCache is not null)
        {
            await _adminCache.DisposeAsync();
        }
    }
}
