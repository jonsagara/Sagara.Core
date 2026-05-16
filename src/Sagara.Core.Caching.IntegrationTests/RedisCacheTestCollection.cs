namespace Sagara.Core.Caching.IntegrationTests;

[CollectionDefinition(Name)]
public sealed class RedisCacheTestCollection : ICollectionFixture<RedisFixture>
{
    public const string Name = nameof(RedisCacheTestCollection);
}
