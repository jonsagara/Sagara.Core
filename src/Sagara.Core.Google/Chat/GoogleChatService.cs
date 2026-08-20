using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Markdig;
using Markdig.Syntax;
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
        var hasCard = title is not null
            || additionalTextWidgetsMarkdown is { Count: > 0 }
            || buttons is { Count: > 0 };

        return new ChatMessagePayload
        {
            Text = BuildText(
                body: body,
                title: title,
                alertLevel: alertLevel,
                mentionUsers: mentionUsers,
                hasCard: hasCard),
            MarkupSyntax = "MARKUP_SYNTAX_MARKDOWN",
            CardsV2 = hasCard
                ? [BuildCard(title, alertLevel, additionalTextWidgetsMarkdown, buttons)]
                : null,
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
            var alertLevelInfo = GoogleChatAlertLevelInfo.For(level);

            text
                .Append(alertLevelInfo.Emoji)
                .Append(" *")
                .Append(alertLevelInfo.Label)
                .Append("*\n\n");
        }

        if (title is not null)
        {
            text
                .Append('*')
                .Append(title)
                .Append("*\n\n");
        }

        text.Append(body);

        if (mentionUsers is { Count: > 0 })
        {
            text.Append("\n\n");
#warning TODO: Use the chat-user mention syntax
            text.AppendJoin(' ', mentionUsers.Select(user => $"<users/{user.Id}>"));
        }

        return text.ToString();
    }

    /// <summary>
    /// Replaces newlines with &lt;br&gt; tags everywhere in <paramref name="markdown"/> except where doing so would
    /// break Markdown block structure that Google's parser relies on real newlines to recognize. Rather than
    /// guessing at that structure with regexes, the markdown is parsed with Markdig and every newline that falls
    /// inside a code block (plus the newline touching each side of it), or between sibling blocks inside a list
    /// (item-to-item, or a list item's own content to a nested list), is left untouched. Newlines in the middle of
    /// a single list item's wrapped content aren't part of either protected category, so they're still converted.
    /// </summary>
    private static string ReplaceNewlinesOutsideCodeBlocks(string markdown)
    {
        var markdownNormalizedNewlines = markdown.Replace("\r\n", "\n", StringComparison.Ordinal);
        var document = Markdown.Parse(markdownNormalizedNewlines);

        var protectedNewlineIndexes = new HashSet<int>();

        foreach (var codeBlock in document.Descendants<CodeBlock>())
        {
            ProtectSpan(protectedNewlineIndexes, markdownNormalizedNewlines, codeBlock.Span.Start, codeBlock.Span.End);
        }

        foreach (var listBlock in document.Descendants<ListBlock>())
        {
            // A list also needs its outer boundary (the newline into it from whatever precedes it, and out of it
            //   to whatever follows) protected, on top of the gaps between its own items/nested lists below - the
            //   same "marker must start on a real line" requirement that applies to a fence's own boundary.
            ProtectBoundaryNewlines(protectedNewlineIndexes, markdownNormalizedNewlines, listBlock.Span.Start, listBlock.Span.End);
            ProtectChildGaps(protectedNewlineIndexes, markdownNormalizedNewlines, listBlock);
        }

        foreach (var listItemBlock in document.Descendants<ListItemBlock>())
        {
            ProtectChildGaps(protectedNewlineIndexes, markdownNormalizedNewlines, listItemBlock);
        }

        var result = new StringBuilder(markdownNormalizedNewlines.Length);

        for (var i = 0; i < markdownNormalizedNewlines.Length; i++)
        {
            if (markdownNormalizedNewlines[i] == '\n' && !protectedNewlineIndexes.Contains(i))
            {
                result.Append("<br>");
            }
            else
            {
                result.Append(markdownNormalizedNewlines[i]);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Marks every newline from <paramref name="start"/> to <paramref name="end"/> (inclusive) as protected, plus
    /// the single newline immediately touching each side of that range if present. Google's Markdown parser only
    /// recognizes a fenced code block as a block-level element when its ``` markers sit on their own line, so the
    /// boundary newlines have to stay real too, or the parser fails to see the fence as closed.
    /// </summary>
    private static void ProtectSpan(HashSet<int> protectedNewlineIndexes, string text, int start, int end)
    {
        if (start > 0 && text[start - 1] == '\n')
        {
            start--;
        }

        if (end + 1 < text.Length && text[end + 1] == '\n')
        {
            end++;
        }

        for (var i = start; i <= end && i < text.Length; i++)
        {
            if (text[i] == '\n')
            {
                protectedNewlineIndexes.Add(i);
            }
        }
    }

    /// <summary>
    /// Marks just the single newline immediately touching each side of the [<paramref name="start"/>,
    /// <paramref name="end"/>] range as protected (unlike <see cref="ProtectSpan"/>, the interior is left alone).
    /// </summary>
    private static void ProtectBoundaryNewlines(HashSet<int> protectedNewlineIndexes, string text, int start, int end)
    {
        if (start > 0 && text[start - 1] == '\n')
        {
            protectedNewlineIndexes.Add(start - 1);
        }

        if (end + 1 < text.Length && text[end + 1] == '\n')
        {
            protectedNewlineIndexes.Add(end + 1);
        }
    }

    /// <summary>
    /// Protects the newline(s) that fall between each pair of consecutive children of <paramref name="container"/>
    /// (e.g. list item to list item, or a list item's own content to a nested list), so those block boundaries
    /// survive for Google's Markdown parser instead of being collapsed into the preceding line's text.
    /// </summary>
    private static void ProtectChildGaps(HashSet<int> protectedNewlineIndexes, string text, ContainerBlock container)
    {
        for (var i = 0; i < container.Count - 1; i++)
        {
            ProtectSpan(protectedNewlineIndexes, text, container[i].Span.End + 1, container[i + 1].Span.Start - 1);
        }
    }

    private static ChatCardWrapper BuildCard(
        string? title,
        GoogleChatAlertLevel? alertLevel,
        IReadOnlyList<string>? additionalTextWidgetsMarkdown,
        IReadOnlyList<GoogleChatButton>? buttons)
    {
        List<ChatCardWidget> widgets = [];

        if (alertLevel is { } level)
        {
            var alertLevelInfo = GoogleChatAlertLevelInfo.For(level);

            widgets.Add(new ChatCardWidget
            {
                // Fixed system content, not user markdown, so it's built directly rather than run through Markdig.
                TextParagraph = new ChatTextParagraph
                {
                    Text = $"<font color=\"{alertLevelInfo.HexColor}\">{alertLevelInfo.Emoji} {alertLevelInfo.Label}</font>",
                    TextSyntax = "HTML",
                },
            });
        }

        if (additionalTextWidgetsMarkdown is not null)
        {
            foreach (var widgetMarkdown in additionalTextWidgetsMarkdown)
            {
                // Google Chat collapses card widgets with Markdown treats newlines as space/whitespace separators
                //   instead of HTML paragraph breaks, collapsing consecutive line breaks into a single line. To
                //   preserve formatting, we have to replace newlines with <br> tags. Skip this inside fenced code
                //   blocks so multi-line code samples don't get mangled with literal <br> tags.
                var widgetMarkdownWithBRs = ReplaceNewlinesOutsideCodeBlocks(widgetMarkdown);

                widgets.Add(new ChatCardWidget
                {
                    TextParagraph = new ChatTextParagraph
                    {
                        Text = widgetMarkdownWithBRs,//ChatCardHtmlRenderer.ToTextParagraphHtml(widgetMarkdown),
                        TextSyntax = "MARKDOWN",
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
            CardId = Guid.NewGuid().ToString(),
            Card = new ChatCard
            {
                Header = title is not null
                    ? new ChatCardHeader { Title = title }
                    : null,
                Sections = [new ChatCardSection { Widgets = widgets }],
            },
        };
    }
}
