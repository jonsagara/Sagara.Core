using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Sagara.Core.Google.Chat;

namespace Sagara.Core.Google.Tests.Chat;

public class GoogleChatServiceEnqueueTests
{
    private const string RelaySpaceAlias = "alerts";
    private const string ApiToken = "test-relay-token";
    private const string AcceptedResponse = """{"space":"alerts","id":42,"queuePosition":3,"duplicate":false}""";

    [Fact]
    public async Task EnqueueMessageAsync_PostsToRelaySendWithBearerToken()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal(new Uri("https://relay.example.com/send"), handler.RequestUri);
        Assert.NotNull(handler.Authorization);
        Assert.Equal("Bearer", handler.Authorization.Scheme);
        Assert.Equal(ApiToken, handler.Authorization.Parameter);
    }

    [Theory]
    [InlineData("https://relay.example.com/prefix", "https://relay.example.com/prefix/send")]
    [InlineData("https://relay.example.com/prefix/", "https://relay.example.com/prefix/send")]
    public async Task EnqueueMessageAsync_BaseAddressWithPath_KeepsPath(string baseAddress, string expectedUri)
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler, options: CreateOptions(baseAddress: baseAddress));

        await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(new Uri(expectedUri), handler.RequestUri);
    }

    [Fact]
    public async Task EnqueueMessageAsync_BodyOnly_WrapsGoogleChatMessageInRelayEnvelope()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        await service.EnqueueMessageAsync(RelaySpaceAlias, "The **prod** deploy failed.", cancellationToken: TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();

        Assert.Equal(RelaySpaceAlias, json.GetProperty("space").GetString());
        // Message building is shared with SendMessageAsync, so the classic-markup conversion still applies.
        Assert.Equal("The *prod* deploy failed.", json.GetProperty("message").GetProperty("text").GetString());
        Assert.False(json.GetProperty("message").TryGetProperty("cardsV2", out _));
        Assert.False(json.TryGetProperty("threadKey", out _));
        Assert.False(json.TryGetProperty("idempotencyKey", out _));
    }

    [Fact]
    public async Task EnqueueMessageAsync_EnqueueOptions_SendsThreadKeyAndIdempotencyKey()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        await service.EnqueueMessageAsync(
            RelaySpaceAlias,
            "hello",
            new GoogleChatEnqueueOptions { ThreadKey = "disk-alerts", IdempotencyKey = "alert-8421" },
            TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();

        Assert.Equal("disk-alerts", json.GetProperty("threadKey").GetString());
        Assert.Equal("alert-8421", json.GetProperty("idempotencyKey").GetString());
    }

    [Fact]
    public async Task EnqueueMessageAsync_MentionAllUsers_AppendsAllMention()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", mentionAllUsers: true, cancellationToken: TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();

        Assert.Equal("hello\n\n<users/all>", json.GetProperty("message").GetProperty("text").GetString());
    }

    [Fact]
    public async Task EnqueueMessageAsync_MentionUsers_AppendsUserMentions()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        await service.EnqueueMessageAsync(
            RelaySpaceAlias,
            "hello",
            mentionUsers: [new GoogleWorkspaceUser("123", "a@example.com")],
            cancellationToken: TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();

        Assert.Equal("hello\n\n<users/123>", json.GetProperty("message").GetProperty("text").GetString());
    }

    [Fact]
    public async Task EnqueueMessageAsync_Cards_IncludesCardsInMessage()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        await service.EnqueueMessageAsync(
            RelaySpaceAlias,
            bodyMarkdown: null,
            cards: [new GoogleChatCardV2(Title: "Deploy failed")],
            cancellationToken: TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();

        var card = json.GetProperty("message").GetProperty("cardsV2").EnumerateArray().Single().GetProperty("card");
        Assert.Equal("Deploy failed", card.GetProperty("header").GetProperty("title").GetString());
    }

    [Fact]
    public async Task EnqueueMessageAsync_Accepted_ReturnsSucceededResultWithQueueFields()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        var result = await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken);

        Assert.True(result.Succeeded);
        Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
        Assert.Equal(42, result.Id);
        Assert.Equal(3, result.QueuePosition);
        Assert.False(result.Duplicate);
        Assert.Null(result.Error);
    }

    [Fact]
    public async Task EnqueueMessageAsync_AcceptedDuplicate_ReturnsDuplicate()
    {
        var handler = new CapturingHttpMessageHandler(
            HttpStatusCode.Accepted,
            """{"space":"alerts","id":7,"queuePosition":0,"duplicate":true}""");
        var service = CreateService(handler);

        var result = await service.EnqueueMessageAsync(
            RelaySpaceAlias,
            "hello",
            new GoogleChatEnqueueOptions { IdempotencyKey = "k" },
            TestContext.Current.CancellationToken);

        Assert.True(result.Succeeded);
        Assert.True(result.Duplicate);
        Assert.Equal(0, result.QueuePosition);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest, """{"error":"\"space\" must be a non-empty string"}""", "\"space\" must be a non-empty string")]
    [InlineData(HttpStatusCode.Unauthorized, """{"error":"Unauthorized"}""", "Unauthorized")]
    [InlineData(HttpStatusCode.NotFound, """{"error":"Unknown space \"alerts\""}""", "Unknown space \"alerts\"")]
    [InlineData(HttpStatusCode.RequestEntityTooLarge, """{"error":"message exceeds Google Chat's 32000-byte limit"}""", "message exceeds Google Chat's 32000-byte limit")]
    public async Task EnqueueMessageAsync_ErrorResponse_ReturnsFailedResultWithRelayErrorAndLogs(HttpStatusCode statusCode, string responseBody, string expectedError)
    {
        var handler = new CapturingHttpMessageHandler(statusCode, responseBody);
        var logger = new RecordingLogger<GoogleChatService>();
        var service = CreateService(handler, logger);

        var result = await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.Equal(statusCode, result.StatusCode);
        Assert.Equal(expectedError, result.Error);
        Assert.Null(result.Id);
        Assert.Null(result.QueuePosition);

        var logEntry = Assert.Single(logger.Entries);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Contains("Request to Google Chat throttling relay failed", logEntry.Message, StringComparison.Ordinal);
        Assert.DoesNotContain(ApiToken, logEntry.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task EnqueueMessageAsync_NonJsonErrorResponse_FallsBackToReasonPhrase()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.BadGateway, "<html>Bad gateway</html>");
        var service = CreateService(handler);

        var result = await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.Equal(HttpStatusCode.BadGateway, result.StatusCode);
        Assert.Equal("Bad Gateway", result.Error);
    }

    [Fact]
    public async Task EnqueueMessageAsync_SuccessWithoutQueueFields_ReturnsFailedResult()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK, "{}");
        var service = CreateService(handler);

        var result = await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task EnqueueMessageAsync_HandlerThrows_ReturnsFailedResultWithNullStatusCodeAndLogs()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK, exceptionToThrow: new HttpRequestException("connection refused"));
        var logger = new RecordingLogger<GoogleChatService>();
        var service = CreateService(handler, logger);

        var result = await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.Null(result.StatusCode);
        Assert.Equal("connection refused", result.Error);

        var logEntry = Assert.Single(logger.Entries);
        Assert.Contains("Unhandled exception occurred while trying to enqueue", logEntry.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task EnqueueMessageAsync_TimeoutWithoutCallerCancellation_ReturnsFailedResult()
    {
        // HttpClient reports its own timeout as a TaskCanceledException without the caller's token canceled.
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK, exceptionToThrow: new TaskCanceledException("timed out"));
        var service = CreateService(handler);

        var result = await service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.Null(result.StatusCode);
    }

    [Fact]
    public async Task EnqueueMessageAsync_CallerCanceled_ThrowsOperationCanceledException()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: cts.Token));
    }

    [Fact]
    public async Task EnqueueMessageAsync_RelayNotConfigured_ThrowsInvalidOperationException()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler, options: new GoogleChatServiceOptions());

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.EnqueueMessageAsync(RelaySpaceAlias, "hello", cancellationToken: TestContext.Current.CancellationToken));

        Assert.Null(handler.RequestUri);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task EnqueueMessageAsync_NullOrWhiteSpaceRelaySpaceAlias_Throws(string? relaySpaceAlias)
    {
        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse));

        await Assert.ThrowsAnyAsync<ArgumentException>(
            () => service.EnqueueMessageAsync(relaySpaceAlias!, "hello", cancellationToken: TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task EnqueueMessageAsync_NullOrWhiteSpaceBody_Throws(string? body)
    {
        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse));

        await Assert.ThrowsAnyAsync<ArgumentException>(
            () => service.EnqueueMessageAsync(RelaySpaceAlias, body!, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task EnqueueMessageAsync_Cards_NoBodyAndNoCards_ThrowsArgumentException()
    {
        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse));

        await Assert.ThrowsAsync<ArgumentException>(
            () => service.EnqueueMessageAsync(
                RelaySpaceAlias,
                bodyMarkdown: null,
                cards: [],
                cancellationToken: TestContext.Current.CancellationToken));
    }

    public static TheoryData<GoogleChatEnqueueOptions> InvalidEnqueueOptions => new()
    {
        new GoogleChatEnqueueOptions { ThreadKey = "" },
        new GoogleChatEnqueueOptions { ThreadKey = "   " },
        new GoogleChatEnqueueOptions { ThreadKey = new string('t', GoogleChatEnqueueOptions.MaxKeyLength + 1) },
        new GoogleChatEnqueueOptions { IdempotencyKey = "" },
        new GoogleChatEnqueueOptions { IdempotencyKey = new string('i', GoogleChatEnqueueOptions.MaxKeyLength + 1) },
    };

    [Theory]
    [MemberData(nameof(InvalidEnqueueOptions))]
    public async Task EnqueueMessageAsync_InvalidEnqueueOptions_ThrowsArgumentException(GoogleChatEnqueueOptions enqueueOptions)
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        await Assert.ThrowsAsync<ArgumentException>(
            () => service.EnqueueMessageAsync(RelaySpaceAlias, "hello", enqueueOptions, TestContext.Current.CancellationToken));

        Assert.Null(handler.RequestUri);
    }

    [Fact]
    public async Task EnqueueMessageAsync_KeysAtMaxLength_AreAccepted()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.Accepted, AcceptedResponse);
        var service = CreateService(handler);

        var result = await service.EnqueueMessageAsync(
            RelaySpaceAlias,
            "hello",
            new GoogleChatEnqueueOptions
            {
                ThreadKey = new string('t', GoogleChatEnqueueOptions.MaxKeyLength),
                IdempotencyKey = new string('i', GoogleChatEnqueueOptions.MaxKeyLength),
            },
            TestContext.Current.CancellationToken);

        Assert.True(result.Succeeded);
    }

    private static GoogleChatServiceOptions CreateOptions(string baseAddress = "https://relay.example.com")
        => new()
        {
            Relay = new GoogleChatRelayOptions { BaseAddress = new Uri(baseAddress), ApiToken = ApiToken },
        };

    private static GoogleChatService CreateService(HttpMessageHandler handler, ILogger<GoogleChatService>? logger = null, GoogleChatServiceOptions? options = null)
        => new(new HttpClient(handler), logger ?? NullLogger<GoogleChatService>.Instance, Options.Create(options ?? CreateOptions()));
}
