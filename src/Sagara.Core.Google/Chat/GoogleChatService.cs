using System.Net;
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

    // Justification: Don't make the caller wrap the webhook URL in a Uri object just to call this method.
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task SendMessageAsync(
        string webhookUrl,
        string bodyMarkdown,
        CancellationToken cancellationToken = default)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);

        var payload = BuildPayload(
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: false,
            mentionUsers: null,
            cards: null);

        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }

    // Justification: Don't make the caller wrap the webhook URL in a Uri object just to call this method.
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task SendMessageAsync(
        string webhookUrl, 
        string bodyMarkdown, 
        bool mentionAllUsers, 
        CancellationToken cancellationToken = default)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);

        var payload = BuildPayload(
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: mentionAllUsers,
            mentionUsers: null,
            cards: null);

        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }

    // Justification: Don't make the caller wrap the webhook URL in a Uri object just to call this method.
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task SendMessageAsync(
        string webhookUrl,
        string bodyMarkdown,
        IReadOnlyCollection<GoogleWorkspaceUser> mentionUsers,
        CancellationToken cancellationToken = default)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);
        Check.ThrowIfNull(mentionUsers);

        var payload = BuildPayload(
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: false,
            mentionUsers: mentionUsers,
            cards: null);

        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }

    // Justification: Don't make the caller wrap the webhook URL in a Uri object just to call this method.
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task SendMessageAsync(
        string webhookUrl,
        string bodyMarkdown,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        CancellationToken cancellationToken = default)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);
        Check.ThrowIfNull(cards);

        var payload = BuildPayload(
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: false,
            mentionUsers: null,
            cards: cards);

        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);
    }

    // Justification: Don't make the caller wrap the webhook URL in a Uri object just to call this method.
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task SendMessageAsync(
        string webhookUrl,
        string bodyMarkdown,
        bool mentionAllUsers,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        CancellationToken cancellationToken = default)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);
        Check.ThrowIfNull(cards);

        var payload = BuildPayload(
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: mentionAllUsers,
            mentionUsers: null,
            cards: cards);

        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);
    }

    // Justification: Don't make the caller wrap the webhook URL in a Uri object just to call this method.
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task SendMessageAsync(
        string webhookUrl,
        string bodyMarkdown,
        IReadOnlyCollection<GoogleWorkspaceUser> mentionUsers,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        CancellationToken cancellationToken = default)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);
        Check.ThrowIfNull(mentionUsers);
        Check.ThrowIfNull(cards);

        var payload = BuildPayload(
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: false,
            mentionUsers: mentionUsers,
            cards: cards);

        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);
    }


    //
    // Private methods
    //

    private static ChatMessagePayload BuildPayload(
        string bodyMarkdown,
        bool mentionAllUsers,
        IReadOnlyCollection<GoogleWorkspaceUser>? mentionUsers,
        IReadOnlyCollection<GoogleChatCardV2>? cards = null)
    {
        // A card is only worth emitting when there's card-specific content to show. AlertLevel alone does not
        // trigger a card — see BuildText, which renders the alert accent inline when no card is emitted.
        var hasCard = cards is { Count: > 0 };

        return new ChatMessagePayload
        {
            Text = BuildText(
                bodyMarkdown: bodyMarkdown,
                mentionAllUsers: mentionAllUsers,
                mentionUsers: mentionUsers),
            MarkupSyntax = "MARKUP_SYNTAX_MARKDOWN",
            CardsV2 = hasCard
                ? BuildCards(cards)
                : null,
        };
    }

    private static string BuildText(string bodyMarkdown, bool mentionAllUsers, IReadOnlyCollection<GoogleWorkspaceUser>? mentionUsers)
    {
        var text = new StringBuilder(bodyMarkdown);

        if (mentionAllUsers)
        {
            text.Append("\n\n");
            text.Append("<chat-user data-user=\"users/all\">");
        }
        else if (mentionUsers is { Count: > 0 })
        {
            text.Append("\n\n");
            text.AppendJoin(' ', mentionUsers.Select(user => $"<chat-user data-email=\"{WebUtility.HtmlEncode(user.Email)}\">"));
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

    private static List<ChatCardWrapper> BuildCards(IReadOnlyCollection<GoogleChatCardV2>? cards)
    {
        if (cards is null)
        {
            return [];
        }

        List<ChatCardWrapper> cardWrappers = new(cards.Count);

        foreach (var card in cards)
        {
            var cardWrapper = BuildCard(
                sectionHeader: card.SectionHeader,
                title: card.Title,
                subtitle: card.Subtitle,
                alertLevel: card.AlertLevel,
                textParagraphMarkdowns: card.TextParagraphMarkdowns,
                buttons: card.Buttons);

            cardWrappers.Add(cardWrapper);
        }

        return cardWrappers;
    }

    private static ChatCardWrapper BuildCard(
        string? sectionHeader,
        string? title,
        string? subtitle,
        GoogleChatAlertLevel? alertLevel,
        IReadOnlyCollection<string>? textParagraphMarkdowns,
        IReadOnlyCollection<GoogleChatButton>? buttons)
    {
        List<ChatCardWidget> widgets = [];


        //
        // First widget: Alert Level, if any.
        //

        if (alertLevel is { } level)
        {
            var alertLevelInfo = GoogleChatAlertLevelInfo.For(level);

            widgets.Add(new ChatCardWidget
            {
                // This is our content, not the user's, so we don't need to worry about escaping it.
                TextParagraph = new ChatTextParagraph
                {
                    Text = $"<font color=\"{alertLevelInfo.HexColor}\">{alertLevelInfo.Emoji} {alertLevelInfo.Label}</font>",
                    TextSyntax = "HTML",
                },
            });
        }


        //
        // Next widget(s): Text paragraphs, if any.
        //

        if (textParagraphMarkdowns is not null)
        {
            foreach (var textParagraphMarkdown in textParagraphMarkdowns)
            {
                // Google Chat collapses card widgets with Markdown treats newlines as space/whitespace separators
                //   instead of HTML paragraph breaks, collapsing consecutive line breaks into a single line. To
                //   preserve formatting, we have to replace newlines with <br> tags. Skip this inside fenced code
                //   blocks so multi-line code samples don't get mangled with literal <br> tags.
                var textParagraphMarkdownWithBRs = ReplaceNewlinesOutsideCodeBlocks(textParagraphMarkdown);

                widgets.Add(new ChatCardWidget
                {
                    TextParagraph = new ChatTextParagraph
                    {
                        Text = textParagraphMarkdownWithBRs,
                        TextSyntax = "MARKDOWN",
                    },
                });
            }
        }


        //
        // Next widget(s): Buttons, if any.

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
                Header = title is not null || subtitle is not null
                    ? new ChatCardHeader { Title = title, Subtitle = subtitle }
                    : null,
                Sections = [new ChatCardSection { Header = sectionHeader, Widgets = widgets }],
            },
        };
    }
}
