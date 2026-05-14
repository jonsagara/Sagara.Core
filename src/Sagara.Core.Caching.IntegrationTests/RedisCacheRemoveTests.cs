using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests;

[Collection(RedisCacheTestCollection.Name)]
public sealed class RedisCacheRemoveTests(RedisFixture fixture) : IAsyncLifetime
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
    // RemoveAsync(string)
    //

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task RemoveAsync_SingleKey_Exists_ReturnsTrueAndKeyIsGone(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}remove-single-exists:{protocol}";

        await cache.SetAsync(key, "value");
        var removed = await cache.RemoveAsync(key);
        var fetched = await cache.GetAsync<string>(key);

        Assert.True(removed);
        Assert.Null(fetched);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task RemoveAsync_SingleKey_DoesNotExist_ReturnsFalse(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var key = $"{_keyPrefix}remove-single-missing:{protocol}";
        _keysToCleanup.Add(key);

        var removed = await cache.RemoveAsync(key);

        Assert.False(removed);
    }


    //
    // RemoveAsync(IReadOnlyCollection<string>)
    //

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task RemoveAsync_Collection_AllExist_ReturnsCorrectCount(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var keys = Enumerable.Range(1, 3)
            .Select(i => $"{_keyPrefix}remove-all-{i}:{protocol}")
            .ToList();

        foreach (var k in keys)
        {
            await cache.SetAsync(k, "value");
        }

        var count = await cache.RemoveAsync(keys);

        Assert.Equal(3, count);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task RemoveAsync_Collection_MixedExistAndMissing_ReturnsOnlyDeletedCount(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var existingKey = $"{_keyPrefix}remove-mixed-exists:{protocol}";
        var missingKey = $"{_keyPrefix}remove-mixed-missing:{protocol}";
        _keysToCleanup.Add(missingKey);

        await cache.SetAsync(existingKey, "value");
        var count = await cache.RemoveAsync([existingKey, missingKey]);

        Assert.Equal(1, count);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task RemoveAsync_Collection_EmptyList_ReturnsZero(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var count = await cache.RemoveAsync([]);

        Assert.Equal(0, count);
    }
}
