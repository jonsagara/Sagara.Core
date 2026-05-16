using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests;

[Collection(RedisCacheTestCollection.Name)]
public sealed class RedisCacheIncrementTests(RedisFixture fixture) : IAsyncLifetime
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


    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task IncrementAndExpireOnCreateAsync_NewKey_ReturnsOne_TtlIsSet(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}incr-new:{protocol}";
        _keysToCleanup.Add(key);

        var result = await cache.IncrementAndExpireOnCreateAsync(key, TimeSpan.FromSeconds(60));
        var ttl = await cache.GetTimeToLiveAsync(key);

        Assert.Equal(1L, result);
        Assert.NotNull(ttl);
        Assert.True(ttl.Value > TimeSpan.Zero);
        Assert.True(ttl.Value <= TimeSpan.FromSeconds(60));
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task IncrementAndExpireOnCreateAsync_ExistingKey_ReturnsIncrementedValue(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}incr-existing:{protocol}";
        _keysToCleanup.Add(key);

        await cache.IncrementAndExpireOnCreateAsync(key, TimeSpan.FromSeconds(60));
        var second = await cache.IncrementAndExpireOnCreateAsync(key, TimeSpan.FromSeconds(60));

        Assert.Equal(2L, second);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task IncrementAndExpireOnCreateAsync_ExistingKey_TtlIsNotReset(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}incr-ttl-unchanged:{protocol}";
        _keysToCleanup.Add(key);

        // Create with a 10s TTL.
        await cache.IncrementAndExpireOnCreateAsync(key, TimeSpan.FromSeconds(10));

        // Wait 3s so ~7s remain.
        await Task.Delay(TimeSpan.FromSeconds(3));

        // Increment again with the same TTL argument — TTL should NOT be reset.
        await cache.IncrementAndExpireOnCreateAsync(key, TimeSpan.FromSeconds(10));
        var ttl = await cache.GetTimeToLiveAsync(key);

        Assert.NotNull(ttl);
        // TTL must still reflect the original creation time, not a fresh 10s window.
        Assert.True(ttl.Value < TimeSpan.FromSeconds(8));
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task IncrementAndExpireOnCreateAsync_CalledConcurrently_FinalCountMatchesCallCount(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}incr-concurrent:{protocol}";
        _keysToCleanup.Add(key);

        const int callCount = 20;
        var tasks = Enumerable.Range(0, callCount)
            .Select(_ => cache.IncrementAndExpireOnCreateAsync(key, TimeSpan.FromSeconds(60)));

        var results = await Task.WhenAll(tasks);

        // The final value must equal the number of concurrent increments.
        Assert.Equal(callCount, results.Max());
    }
}
