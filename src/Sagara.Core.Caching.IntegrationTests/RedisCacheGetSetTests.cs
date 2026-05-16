using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests;

/// <summary>Simple model used to verify JSON round-trips through the cache.</summary>
internal sealed record CachedModel(int Id, string Name, DateTimeOffset CreatedAt);

[Collection(RedisCacheTestCollection.Name)]
public sealed class RedisCacheGetSetTests(RedisFixture fixture) : IAsyncLifetime
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
    // GetAsync
    //

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetAsync_KeyDoesNotExist_ReturnsNull(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}missing";
        _keysToCleanup.Add(key);

        var result = await cache.GetAsync<string>(key);

        Assert.Null(result);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetAsync_KeyExists_ReturnsPrimitiveValue(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}primitive:{protocol}";
        _keysToCleanup.Add(key);

        await cache.SetAsync(key, 42);
        var result = await cache.GetAsync<int>(key);

        Assert.Equal(42, result);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetAsync_KeyExists_ReturnsComplexObjectValue(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}complex:{protocol}";
        _keysToCleanup.Add(key);

        var original = new CachedModel(1, "Test", DateTimeOffset.UtcNow);
        await cache.SetAsync(key, original);
        var result = await cache.GetAsync<CachedModel>(key);

        Assert.NotNull(result);
        Assert.Equal(original.Id, result.Id);
        Assert.Equal(original.Name, result.Name);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetAsync_ExpiredKey_ReturnsNull(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}expired:{protocol}";
        _keysToCleanup.Add(key);

        await cache.SetAsync(key, "will-expire", expiry: TimeSpan.FromSeconds(1));
        await Task.Delay(TimeSpan.FromSeconds(2));

        var result = await cache.GetAsync<string>(key);

        Assert.Null(result);
    }


    //
    // SetAsync
    //

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task SetAsync_NoExpiry_ReturnsTrue(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}setasync-noexpiry:{protocol}";
        _keysToCleanup.Add(key);

        var result = await cache.SetAsync(key, "hello");

        Assert.True(result);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task SetAsync_WithExpiry_TtlIsSet(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}setasync-expiry:{protocol}";
        _keysToCleanup.Add(key);

        await cache.SetAsync(key, "hello", expiry: TimeSpan.FromSeconds(60));
        var ttl = await cache.GetTimeToLiveAsync(key);

        Assert.NotNull(ttl);
        Assert.True(ttl.Value > TimeSpan.Zero);
        Assert.True(ttl.Value <= TimeSpan.FromSeconds(60));
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task SetAsync_OverwritesExistingKey(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}setasync-overwrite:{protocol}";
        _keysToCleanup.Add(key);

        await cache.SetAsync(key, "first");
        await cache.SetAsync(key, "second");
        var result = await cache.GetAsync<string>(key);

        Assert.Equal("second", result);
    }


    //
    // Set (sync)
    //

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task Set_Sync_NoExpiry_ReturnsTrue(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}set-noexpiry:{protocol}";
        _keysToCleanup.Add(key);

        var result = cache.Set(key, "hello");

        Assert.True(result);

        await Task.CompletedTask;
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task Set_Sync_WithExpiry_TtlIsSet(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}set-expiry:{protocol}";
        _keysToCleanup.Add(key);

        cache.Set(key, "hello", expiry: TimeSpan.FromSeconds(60));
        var ttl = await cache.GetTimeToLiveAsync(key);

        Assert.NotNull(ttl);
        Assert.True(ttl.Value > TimeSpan.Zero);
        Assert.True(ttl.Value <= TimeSpan.FromSeconds(60));
    }
}
