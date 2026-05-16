using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests;

[Collection(RedisCacheTestCollection.Name)]
public sealed class RedisAdminCacheTests(RedisFixture fixture) : IAsyncLifetime
{
    private readonly string _keyPrefix = $"test:{Guid.NewGuid():N}:";

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;


    [RequiresRedisFlushFact]
    public async Task FlushAllAsync_ClearsAllKeys()
    {
        var adminCache = fixture.AdminCache;

        // Use the RESP2 cache to seed a few keys we can verify are gone afterward.
        var seedCache = fixture.GetCache(RedisProtocol.Resp2);
        var keys = Enumerable.Range(1, 3)
            .Select(i => $"{_keyPrefix}flush-{i}")
            .ToList();

        foreach (var k in keys)
        {
            await seedCache.SetAsync(k, "value");
        }

        await adminCache.FlushAllAsync();

        // All seeded keys must be gone.
        foreach (var k in keys)
        {
            var value = await seedCache.GetAsync<string>(k);
            Assert.Null(value);
        }
    }
}
