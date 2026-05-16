using StackExchange.Redis;

namespace Sagara.Core.Caching.IntegrationTests;

[Collection(RedisCacheTestCollection.Name)]
public sealed class RedisCachePubSubTests(RedisFixture fixture) : IAsyncLifetime
{
    private readonly string _channelPrefix = $"test-channel:{Guid.NewGuid():N}:";

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    public static TheoryData<RedisProtocol> BothProtocols => new()
    {
        RedisProtocol.Resp2,
        RedisProtocol.Resp3,
    };


    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task GetSubscriber_ReturnsNonNullSubscriber(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var subscriber = cache.GetSubscriber();

        Assert.NotNull(subscriber);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task PublishAsync_NoSubscribers_ReturnsZero(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var channel = RedisChannel.Literal($"{_channelPrefix}no-subs:{protocol}");

        var receiverCount = await cache.PublishAsync(channel, "hello");

        Assert.Equal(0L, receiverCount);
    }

    [RequiresRedisTheory, MemberData(nameof(BothProtocols))]
    public async Task SubscribeAsync_ThenPublishAsync_SubscriberReceivesMessage(RedisProtocol protocol)
    {
        var cache = fixture.GetCache(protocol);
        var channel = RedisChannel.Literal($"{_channelPrefix}subscribe:{protocol}");

        var tcs = new TaskCompletionSource<RedisValue>(TaskCreationOptions.RunContinuationsAsynchronously);

        await cache.SubscribeAsync(channel, (_, message) => tcs.TrySetResult(message));

        // Give the subscription a moment to propagate before publishing.
        await Task.Delay(100);

        await cache.PublishAsync(channel, "hello-pubsub");

        var received = await tcs.Task.WaitAsync(TimeSpan.FromSeconds(5));

        Assert.Equal("hello-pubsub", (string?)received);
    }
}
