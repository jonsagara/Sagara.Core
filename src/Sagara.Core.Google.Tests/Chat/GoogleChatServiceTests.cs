//using System.Net;
//using System.Text.Json;
//using Sagara.Core.Google.Chat;

//namespace Sagara.Core.Google.Tests.Chat;

//public class GoogleChatServiceTests
//{
//    private const string WebhookUrl = "https://chat.googleapis.com/v1/spaces/x/messages?key=y&token=z";

//    [Fact]
//    public async Task SendMessageAsync_BodyOnly_SendsTextOnlyPayload()
//    {
//        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK);
//        var service = CreateService(handler);

//        await service.SendMessageAsync(WebhookUrl, "hello", cancellationToken: TestContext.Current.CancellationToken);

//        var json = await handler.GetRequestJsonAsync();

//        Assert.Equal("hello", json.GetProperty("text").GetString());
//        Assert.False(json.TryGetProperty("cardsV2", out _));
//    }

//    [Fact]
//    public async Task SendMessageAsync_TitleAlertButtonsAndWidgets_EmitsCard()
//    {
//        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK);
//        var service = CreateService(handler);

//        await service.SendMessageAsync(
//            WebhookUrl,
//            "hello",
//            cardTitle: "Deploy failed",
//            alertLevel: GoogleChatAlertLevel.Error,
//            additionalTextWidgetsMarkdown: ["more **info**"],
//            buttons: [new GoogleChatButton("View logs", "https://example.com/logs")],
//            cancellationToken: TestContext.Current.CancellationToken);

//        var json = await handler.GetRequestJsonAsync();
//        var text = json.GetProperty("text").GetString();

//        // Title is prepended to the text field too, so it shows in the notification preview.
//        Assert.Contains("Deploy failed", text, StringComparison.Ordinal);
//        // Alert level renders in the card, and is not also duplicated in the text prefix.
//        Assert.DoesNotContain("ERROR", text, StringComparison.Ordinal);

//        var widgets = json.GetProperty("cardsV2").EnumerateArray().First()
//            .GetProperty("card").GetProperty("sections").EnumerateArray().First()
//            .GetProperty("widgets").EnumerateArray().ToList();

//        Assert.Equal(3, widgets.Count); // alert accent widget + additional text widget + button list
//        Assert.Contains("ERROR", widgets[0].GetProperty("textParagraph").GetProperty("text").GetString(), StringComparison.Ordinal);
//        Assert.True(widgets[2].TryGetProperty("buttonList", out _));
//    }

//    [Fact]
//    public async Task SendMessageAsync_MentionUsers_AppendsMentionChipsToText()
//    {
//        var handler = new CapturingHttpMessageHandler(HttpStatusCode.OK);
//        var service = CreateService(handler);

//        await service.SendMessageAsync(
//            WebhookUrl,
//            "hello",
//            mentionUsers: [new GoogleWorkspaceUser("12345"), new GoogleWorkspaceUser("67890")],
//            cancellationToken: TestContext.Current.CancellationToken);

//        var json = await handler.GetRequestJsonAsync();
//        var text = json.GetProperty("text").GetString();

//        Assert.Contains("<users/12345>", text, StringComparison.Ordinal);
//        Assert.Contains("<users/67890>", text, StringComparison.Ordinal);
//    }

//    [Fact]
//    public async Task SendMessageAsync_NonSuccessStatusCode_ThrowsHttpRequestException()
//    {
//        var handler = new CapturingHttpMessageHandler(HttpStatusCode.BadRequest);
//        var service = CreateService(handler);

//        await Assert.ThrowsAsync<HttpRequestException>(
//            () => service.SendMessageAsync(WebhookUrl, "hello", cancellationToken: TestContext.Current.CancellationToken));
//    }

//    [Theory]
//    [InlineData(null)]
//    [InlineData("")]
//    [InlineData("   ")]
//    public async Task SendMessageAsync_NullOrWhiteSpaceBody_Throws(string? body)
//    {
//        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.OK));

//        await Assert.ThrowsAnyAsync<ArgumentException>(
//            () => service.SendMessageAsync(WebhookUrl, body!, cancellationToken: TestContext.Current.CancellationToken));
//    }

//    [Theory]
//    [InlineData(null)]
//    [InlineData("")]
//    [InlineData("   ")]
//    public async Task SendMessageAsync_NullOrWhiteSpaceWebhookUrl_Throws(string? webhookUrl)
//    {
//        var service = CreateService(new CapturingHttpMessageHandler(HttpStatusCode.OK));

//        await Assert.ThrowsAnyAsync<ArgumentException>(
//            () => service.SendMessageAsync(webhookUrl!, "hello", cancellationToken: TestContext.Current.CancellationToken));
//    }

//    private static GoogleChatService CreateService(HttpMessageHandler handler)
//        => new(new HttpClient(handler));

//    private sealed class CapturingHttpMessageHandler(HttpStatusCode statusCode) : HttpMessageHandler
//    {
//        private string? _requestBody;

//        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            _requestBody = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);

//            return new HttpResponseMessage(statusCode);
//        }

//        public Task<JsonElement> GetRequestJsonAsync()
//        {
//            Assert.NotNull(_requestBody);

//            using var document = JsonDocument.Parse(_requestBody);
//            return Task.FromResult(document.RootElement.Clone());
//        }
//    }
//}
