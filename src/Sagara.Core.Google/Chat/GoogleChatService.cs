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
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private readonly HttpClient _httpClient;

    public GoogleChatService(HttpClient httpClient)
    {
        Check.ThrowIfNull(httpClient);

        _httpClient = httpClient;
    }

    /// <summary>
    /// Sends <paramref name="message"/> to the Google Chat space backed by <paramref name="webhookUrl"/>.
    /// </summary>
    /// <param name="webhookUrl">The Google Chat incoming webhook URL to POST the message to.</param>
    /// <param name="message">The message to send.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="HttpRequestException">The webhook responded with a non-success status code.</exception>
    // Justification: Don't make the caller wrap the webhook URL in a Uri object just to call this method.
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task SendMessageAsync(string webhookUrl, GoogleChatMessage message, CancellationToken cancellationToken = default)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNull(message);
        Check.ThrowIfNullOrWhiteSpace(message.Body);

        var payload = BuildPayload(message);

        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, s_jsonOptions, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }

    private static ChatMessagePayload BuildPayload(GoogleChatMessage message)
    {
        // A card is only worth emitting when there's card-specific content to show. AlertLevel alone does not
        // trigger a card — see BuildText, which renders the alert accent inline when no card is emitted.
        var hasCard = message.Title is not null || message.AdditionalTextWidgetsMarkdown.Count > 0 || message.Buttons.Count > 0;

        return new ChatMessagePayload
        {
            Text = BuildText(message, hasCard),
            CardsV2 = hasCard ? [BuildCard(message)] : null,
        };
    }

    private static string BuildText(GoogleChatMessage message, bool hasCard)
    {
        var text = new StringBuilder();

        // When a card is emitted, the alert level is shown as a widget in the card instead (see BuildCard) so we
        // don't show it twice in the same message.
        if (!hasCard && message.AlertLevel is { } alertLevel)
        {
            var info = GoogleChatAlertLevelInfo.For(alertLevel);
            text.Append(info.Emoji).Append(" **").Append(info.Label).Append("**\n\n");
        }

        if (message.Title is not null)
        {
            text.Append("**").Append(message.Title).Append("**\n\n");
        }

        text.Append(message.Body);

        if (message.MentionUserIds.Count > 0)
        {
            text.Append("\n\n");
            text.AppendJoin(' ', message.MentionUserIds.Select(id => $"<users/{id}>"));
        }

        return text.ToString();
    }

    private static ChatCardWrapper BuildCard(GoogleChatMessage message)
    {
        var widgets = new List<ChatCardWidget>();

        if (message.AlertLevel is { } alertLevel)
        {
            var info = GoogleChatAlertLevelInfo.For(alertLevel);
            widgets.Add(new ChatCardWidget
            {
                // Fixed system content, not user markdown, so it's built directly rather than run through Markdig.
                TextParagraph = new ChatTextParagraph
                {
                    Text = $"<font color=\"{info.HexColor}\">{info.Emoji} {info.Label}</font>",
                },
            });
        }

        foreach (var widgetMarkdown in message.AdditionalTextWidgetsMarkdown)
        {
            widgets.Add(new ChatCardWidget
            {
                TextParagraph = new ChatTextParagraph
                {
                    Text = ChatCardHtmlRenderer.ToTextParagraphHtml(widgetMarkdown),
                },
            });
        }

        if (message.Buttons.Count > 0)
        {
            widgets.Add(new ChatCardWidget
            {
                ButtonList = new ChatButtonList
                {
                    Buttons = message.Buttons
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
                Header = message.Title is not null ? new ChatCardHeader { Title = message.Title } : null,
                Sections = [new ChatCardSection { Widgets = widgets }],
            },
        };
    }
}
