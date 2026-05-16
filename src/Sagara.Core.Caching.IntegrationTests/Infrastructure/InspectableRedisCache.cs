using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests.Infrastructure;

/// <summary>
/// Exposes the protected <see cref="RedisCache.Multiplexer"/> to the test fixture for
/// connectivity verification and disposal.
/// </summary>
internal sealed class InspectableRedisCache(
    ILogger<RedisCache> logger,
    string connectionString,
    RedisProtocol protocol,
    bool allowAdmin)
    : RedisCache(logger, connectionString, protocol, allowAdmin)
{
    internal ConnectionMultiplexer GetMultiplexer() => Multiplexer;
}
