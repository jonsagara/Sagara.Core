using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests;

[Collection(RedisCacheTestCollection.Name)]
public sealed class RedisCachePingTests(RedisFixture fixture)
{
    public static TheoryData<RedisProtocol> BothProtocols => new()
    {
        RedisProtocol.Resp2,
        RedisProtocol.Resp3,
    };


    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task PingAsync_WhenRedisAvailable_ReturnsNonNegativeTimeSpan(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);

        var latency = await cache.PingAsync();

        Assert.True(latency >= TimeSpan.Zero);
    }
}
