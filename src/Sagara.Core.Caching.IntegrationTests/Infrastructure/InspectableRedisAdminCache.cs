using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests.Infrastructure;

/// <summary>
/// Exposes the protected <see cref="RedisCache.Multiplexer"/> to the test fixture for disposal.
/// </summary>
internal sealed class InspectableRedisAdminCache(
    ILogger<RedisAdminCache> logger,
    string connectionString,
    RedisProtocol protocol)
    : RedisAdminCache(logger, connectionString, protocol)
{
    internal ConnectionMultiplexer GetMultiplexer() => Multiplexer;
}
