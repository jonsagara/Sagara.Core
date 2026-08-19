using System.Text.Json.Serialization;

namespace Sagara.Core.Google.Chat.Payloads;

internal sealed class ChatMessagePayload
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("markup_syntax")]
    public string? MarkupSyntax { get; set; }

    [JsonPropertyName("cardsV2")]
    public IReadOnlyList<ChatCardWrapper>? CardsV2 { get; set; }
}

internal sealed class ChatCardWrapper
{
    [JsonPropertyName("cardId")]
    public required string CardId { get; set; }

    [JsonPropertyName("card")]
    public required ChatCard Card { get; set; }
}

internal sealed class ChatCard
{
    [JsonPropertyName("header")]
    public ChatCardHeader? Header { get; set; }

    [JsonPropertyName("sections")]
    public required IReadOnlyList<ChatCardSection> Sections { get; set; }
}

internal sealed class ChatCardHeader
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
}

internal sealed class ChatCardSection
{
    [JsonPropertyName("widgets")]
    public required IReadOnlyList<ChatCardWidget> Widgets { get; set; }
}

/// <summary>
/// A single card widget. Modeled as one type with nullable variant members instead of a discriminated union,
/// since there are only two variants in use.
/// </summary>
internal sealed class ChatCardWidget
{
    [JsonPropertyName("textParagraph")]
    public ChatTextParagraph? TextParagraph { get; set; }

    [JsonPropertyName("buttonList")]
    public ChatButtonList? ButtonList { get; set; }
}

internal sealed class ChatTextParagraph
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

internal sealed class ChatButtonList
{
    [JsonPropertyName("buttons")]
    public required IReadOnlyList<ChatButton> Buttons { get; set; }
}

internal sealed class ChatButton
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("onClick")]
    public required ChatOnClick OnClick { get; set; }
}

internal sealed class ChatOnClick
{
    [JsonPropertyName("openLink")]
    public required ChatOpenLink OpenLink { get; set; }
}

internal sealed class ChatOpenLink
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}
