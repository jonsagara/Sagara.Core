using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sagara.Core.Google.Chat.Internal;
using Sagara.Core.Google.Chat.Payloads;

namespace Sagara.Core.Google.Chat;

/// <summary>
/// Sends messages to a Google Chat space via an incoming webhook.
/// </summary>
public sealed class GoogleChatService
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly HttpClient _httpClient;

    public GoogleChatService(HttpClient httpClient)
    {
        Check.ThrowIfNull(httpClient);

        _httpClient = httpClient;
    }

    /// <summary>
    /// Sends a message to the Google Chat space backed by <paramref name="webhookUrl"/>.
    /// </summary>
    /// <param name="webhookUrl">The Google Chat incoming webhook URL to POST the message to.</param>
    /// <param name="body">The message body. Standard markdown.</param>
    /// <param name="title">An optional title. Rendered as a bold line above <paramref name="body"/>, and as the
    /// card header title if a card ends up being emitted (see <paramref name="additionalTextWidgetsMarkdown"/>
    /// and <paramref name="buttons"/>).</param>
    /// <param name="alertLevel">An optional alert severity to call out.</param>
    /// <param name="additionalTextWidgetsMarkdown">Additional card text widgets. Each entry is standard markdown,
    /// converted to the restricted HTML subset supported by Google Chat's TextParagraph widget. Providing any
    /// entries causes a card to be emitted.</param>
    /// <param name="buttons">Card buttons. Providing any buttons causes a card to be emitted.</param>
    /// <param name="mentionUsers">Google Workspace users to mention.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="HttpRequestException">The webhook responded with a non-success status code.</exception>
    // Justification: Don't make the caller wrap the webhook URL in a Uri object just to call this method.
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task SendMessageAsync(
        string webhookUrl,
        string body,
        string? title = null,
        GoogleChatAlertLevel? alertLevel = null,
        IReadOnlyList<string>? additionalTextWidgetsMarkdown = null,
        IReadOnlyList<GoogleChatButton>? buttons = null,
        IReadOnlyList<GoogleWorkspaceUser>? mentionUsers = null,
        CancellationToken cancellationToken = default)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(body);

        var payload = BuildPayload(
            body: body,
            title: title,
            alertLevel: alertLevel,
            additionalTextWidgetsMarkdown: additionalTextWidgetsMarkdown,
            buttons: buttons,
            mentionUsers: mentionUsers);

        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }


    //
    // Private methods
    //

    private static ChatMessagePayload BuildPayload(
        string body,
        string? title,
        GoogleChatAlertLevel? alertLevel,
        IReadOnlyList<string>? additionalTextWidgetsMarkdown,
        IReadOnlyList<GoogleChatButton>? buttons,
        IReadOnlyList<GoogleWorkspaceUser>? mentionUsers)
    {
        // A card is only worth emitting when there's card-specific content to show. AlertLevel alone does not
        // trigger a card — see BuildText, which renders the alert accent inline when no card is emitted.
        var hasCard = title is not null || additionalTextWidgetsMarkdown is { Count: > 0 } || buttons is { Count: > 0 };

        return new ChatMessagePayload
        {
            Text = BuildText(body, title, alertLevel, mentionUsers, hasCard),
            CardsV2 = hasCard ? [BuildCard(title, alertLevel, additionalTextWidgetsMarkdown, buttons)] : null,
        };
    }

    private static string BuildText(
        string body,
        string? title,
        GoogleChatAlertLevel? alertLevel,
        IReadOnlyList<GoogleWorkspaceUser>? mentionUsers,
        bool hasCard)
    {
        var text = new StringBuilder();

        // When a card is emitted, the alert level is shown as a widget in the card instead (see BuildCard) so we
        // don't show it twice in the same message.
        if (!hasCard && alertLevel is { } level)
        {
            var info = GoogleChatAlertLevelInfo.For(level);
            text.Append(info.Emoji).Append(" **").Append(info.Label).Append("**\n\n");
        }

        if (title is not null)
        {
            text.Append("**").Append(title).Append("**\n\n");
        }

        text.Append(body);

        if (mentionUsers is { Count: > 0 })
        {
            text.Append("\n\n");
            text.AppendJoin(' ', mentionUsers.Select(user => $"<users/{user.Id}>"));
        }

        return text.ToString();
    }

    private static ChatCardWrapper BuildCard(
        string? title,
        GoogleChatAlertLevel? alertLevel,
        IReadOnlyList<string>? additionalTextWidgetsMarkdown,
        IReadOnlyList<GoogleChatButton>? buttons)
    {
        var widgets = new List<ChatCardWidget>();

        if (alertLevel is { } level)
        {
            var info = GoogleChatAlertLevelInfo.For(level);
            widgets.Add(new ChatCardWidget
            {
                // Fixed system content, not user markdown, so it's built directly rather than run through Markdig.
                TextParagraph = new ChatTextParagraph
                {
                    Text = $"<font color=\"{info.HexColor}\">{info.Emoji} {info.Label}</font>",
                },
            });
        }

        if (additionalTextWidgetsMarkdown is not null)
        {
            foreach (var widgetMarkdown in additionalTextWidgetsMarkdown)
            {
                widgets.Add(new ChatCardWidget
                {
                    TextParagraph = new ChatTextParagraph
                    {
                        Text = ChatCardHtmlRenderer.ToTextParagraphHtml(widgetMarkdown),
                    },
                });
            }
        }

        if (buttons is { Count: > 0 })
        {
            widgets.Add(new ChatCardWidget
            {
                ButtonList = new ChatButtonList
                {
                    Buttons = buttons
                        .Select(button => new ChatButton
                        {
                            Text = button.Text,
                            OnClick = new ChatOnClick
                            {
                                OpenLink = new ChatOpenLink { Url = button.Url },
                            },
                        })
                        .ToList(),
                },
            });
        }

        return new ChatCardWrapper
        {
            CardId = "card",
            Card = new ChatCard
            {
                Header = title is not null ? new ChatCardHeader { Title = title } : null,
                Sections = [new ChatCardSection { Widgets = widgets }],
            },
        };
    }
}
