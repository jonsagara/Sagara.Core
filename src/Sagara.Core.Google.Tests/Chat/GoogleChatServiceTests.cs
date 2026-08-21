using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sagara.Core.Google.Chat;

namespace Sagara.Core.Google.Tests.Chat;

public class GoogleChatServiceTests
{
    private const string WebhookUrl = "https://chat.googleapis.com/v1/spaces/x/messages?key=y&token=z";

    [Fact]
    public async Task SendMessageAsync_BodyOnly_SendsTextOnlyPayload()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK);
        var service = CreateService(handler);

        await service.SendMessageAsync(WebhookUrl, "hello", cancellationToken: TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();

        Assert.Equal("hello", json.GetProperty("text").GetString());
        Assert.False(json.TryGetProperty("cardsV2", out _));
    }

    [Fact]
    public async Task SendMessageAsync_Cards_EmitsAlertTextParagraphAndButtonWidgets()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK);
        var service = CreateService(handler);

        var card = new GoogleChatCardV2(Title: "Deploy failed")
        {
            Subtitle = "prod",
            AlertLevel = GoogleChatAlertLevel.Error,
            TextParagraphMarkdowns = ["more **info**"],
            Buttons = [new GoogleChatButton("View logs", "https://example.com/logs")],
        };

        await service.SendMessageAsync(
            WebhookUrl,
            "hello",
            cards: [card],
            cancellationToken: TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();

        var cardElement = json.GetProperty("cardsV2").EnumerateArray().First().GetProperty("card");
        Assert.Equal("Deploy failed", cardElement.GetProperty("header").GetProperty("title").GetString());
        Assert.Equal("prod", cardElement.GetProperty("header").GetProperty("subtitle").GetString());

        var widgets = cardElement.GetProperty("sections").EnumerateArray().First()
            .GetProperty("widgets").EnumerateArray().ToList();

        Assert.Equal(3, widgets.Count); // alert accent widget + text paragraph widget + button list
        Assert.Contains("ERROR", widgets[0].GetProperty("textParagraph").GetProperty("text").GetString(), StringComparison.Ordinal);
        Assert.Contains("more **info**", widgets[1].GetProperty("textParagraph").GetProperty("text").GetString(), StringComparison.Ordinal);
        Assert.True(widgets[2].TryGetProperty("buttonList", out _));
    }

    [Fact]
    public async Task SendMessageAsync_MentionAllUsers_AppendsMentionAllChipToText()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK);
        var service = CreateService(handler);

        await service.SendMessageAsync(
            WebhookUrl,
            "hello",
            mentionAllUsers: true,
            cancellationToken: TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();
        var text = json.GetProperty("text").GetString();

        Assert.Contains("""<chat-user data-user="users/all">""", text, StringComparison.Ordinal);
    }

    [Fact]
    public async Task SendMessageAsync_MentionUsers_AppendsMentionChipsToText()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK);
        var service = CreateService(handler);

        await service.SendMessageAsync(
            WebhookUrl,
            "hello",
            mentionUsers: [new GoogleWorkspaceUser("jon@example.com"), new GoogleWorkspaceUser("jane@example.com")],
            cancellationToken: TestContext.Current.CancellationToken);

        var json = await handler.GetRequestJsonAsync();
        var text = json.GetProperty("text").GetString();

        Assert.Contains("""<chat-user data-email="jon@example.com">""", text, StringComparison.Ordinal);
        Assert.Contains("""<chat-user data-email="jane@example.com">""", text, StringComparison.Ordinal);
    }

    [Fact]
    public async Task SendMessageAsync_NonSuccessStatusCode_LogsErrorAndDoesNotThrow()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.BadRequest);
        var logger = new RecordingLogger<GoogleChatService>();
        var service = CreateService(handler, logger);

        await service.SendMessageAsync(WebhookUrl, "hello", cancellationToken: TestContext.Current.CancellationToken);

        var logEntry = Assert.Single(logger.Entries);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Contains("Request to Google Chat API failed", logEntry.Message, StringComparison.Ordinal);
        Assert.Contains("BadRequest", logEntry.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task SendMessageAsync_Cards_NonSuccessStatusCode_LogsErrorAndDoesNotThrow()
    {
        var handler = new CapturingHttpMessageHandler(HttpStatusCode.BadRequest);
        var logger = new RecordingLogger<GoogleChatService>();
        var service = CreateService(handler, logger);

        await service.SendMessageAsync(
            WebhookUrl,
            "hello",
            cards: [new GoogleChatCardV2(Title: "Card")],
            cancellationToken: TestContext.Current.CancellationToken);

        var logEntry = Assert.Single(logger.Entries);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Contains("Request to Google Chat API failed", logEntry.Message, StringComparison.Ordinal);
        Assert.Contains("BadRequest", logEntry.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendMessageAsync_NullOrWhiteSpaceBody_Throws(string? body)
    {
        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.OK));

        await Assert.ThrowsAnyAsync<ArgumentException>(
            () => service.SendMessageAsync(WebhookUrl, body!, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendMessageAsync_NullOrWhiteSpaceWebhookUrl_Throws(string? webhookUrl)
    {
        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.OK));

        await Assert.ThrowsAnyAsync<ArgumentException>(
            () => service.SendMessageAsync(webhookUrl!, "hello", cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task SendMessageAsync_Cards_NoBodyAndNoCards_ThrowsArgumentException()
    {
        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.OK));

        await Assert.ThrowsAsync<ArgumentException>(
            () => service.SendMessageAsync(
                WebhookUrl,
                bodyMarkdown: null,
                cards: [],
                cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task SendMessageAsync_Cards_EmptyCard_ThrowsArgumentException()
    {
        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.OK));

        await Assert.ThrowsAsync<ArgumentException>(
            () => service.SendMessageAsync(
                WebhookUrl,
                bodyMarkdown: null,
                cards: [new GoogleChatCardV2(Title: null)],
                cancellationToken: TestContext.Current.CancellationToken));

        
    }

    private static GoogleChatService CreateService(HttpMessageHandler handler, ILogger<GoogleChatService>? logger = null)
        => new(new HttpClient(handler), logger ?? NullLogger<GoogleChatService>.Instance);

    private sealed class RecordingLogger<T> : ILogger<T>
    {
        public List<(LogLevel LogLevel, string Message)> Entries { get; } = [];

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            => Entries.Add((logLevel, formatter(state, exception)));
    }

    private sealed class CapturingHttpMessageHandler(HttpStatusCode statusCode) : HttpMessageHandler
    {
        private string? _requestBody;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _requestBody = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);

            return new HttpResponseMessage(statusCode);
        }

        public Task<JsonElement> GetRequestJsonAsync()
        {
            Assert.NotNull(_requestBody);

            using var document = JsonDocument.Parse(_requestBody);
            return Task.FromResult(document.RootElement.Clone());
        }
    }
}
