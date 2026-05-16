using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests;

[Collection(RedisCacheTestCollection.Name)]
public sealed class RedisCacheSlidingExpirationTests(RedisFixture fixture) : IAsyncLifetime
{
    private readonly string _keyPrefix = $"test:{Guid.NewGuid():N}:";
    private readonly List<string> _keysToCleanup = [];

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        if (_keysToCleanup.Count > 0)
        {
            await fixture.GetCache(RedisProtocol.Resp2).RemoveAsync(_keysToCleanup);
        }
    }

    public static TheoryData<RedisProtocol> BothProtocols => new()
    {
        RedisProtocol.Resp2,
        RedisProtocol.Resp3,
    };


    //
    // GetWithSlidingExpirationAsync
    //

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetWithSlidingExpirationAsync_KeyDoesNotExist_ReturnsNull(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}sliding-async-missing:{protocol}";
        _keysToCleanup.Add(key);

        var result = await cache.GetWithSlidingExpirationAsync<string>(key, TimeSpan.FromSeconds(30));

        Assert.Null(result);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetWithSlidingExpirationAsync_KeyExists_ReturnsValue(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}sliding-async-exists:{protocol}";
        _keysToCleanup.Add(key);

        await cache.SetAsync(key, "cached-value");
        var result = await cache.GetWithSlidingExpirationAsync<string>(key, TimeSpan.FromSeconds(30));

        Assert.Equal("cached-value", result);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetWithSlidingExpirationAsync_KeyExists_TtlIsReset(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}sliding-async-ttl:{protocol}";
        _keysToCleanup.Add(key);

        // Set with a 10s TTL and wait 3s so there are ~7s remaining.
        await cache.SetAsync(key, "value", expiry: TimeSpan.FromSeconds(10));
        await Task.Delay(TimeSpan.FromSeconds(3));

        // Sliding get with 10s expiry should reset the clock.
        await cache.GetWithSlidingExpirationAsync<string>(key, TimeSpan.FromSeconds(10));
        var ttl = await cache.GetTimeToLiveAsync(key);

        Assert.NotNull(ttl);
        // TTL should now be close to 10s, not the ~7s left before the call.
        Assert.True(ttl.Value > TimeSpan.FromSeconds(8));
    }


    //
    // GetWithSlidingExpiration (sync)
    //

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetWithSlidingExpiration_Sync_KeyDoesNotExist_ReturnsNull(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}sliding-sync-missing:{protocol}";
        _keysToCleanup.Add(key);

        var result = cache.GetWithSlidingExpiration<string>(key, TimeSpan.FromSeconds(30));

        Assert.Null(result);

        await Task.CompletedTask;
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetWithSlidingExpiration_Sync_KeyExists_ReturnsValue(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}sliding-sync-exists:{protocol}";
        _keysToCleanup.Add(key);

        await cache.SetAsync(key, "cached-value");
        var result = cache.GetWithSlidingExpiration<string>(key, TimeSpan.FromSeconds(30));

        Assert.Equal("cached-value", result);
    }
}
