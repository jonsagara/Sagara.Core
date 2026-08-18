namespace Sagara.Core.Google.Chat;

/// <summary>
/// A message to send to a Google Chat space via <see cref="GoogleChatService.SendMessageAsync"/>.
/// </summary>
public sealed record GoogleChatMessage
{
    /// <summary>
    /// The message body. Standard markdown. Required.
    /// </summary>
    public required string Body { get; init; }

    /// <summary>
    /// An optional title. Rendered as a bold line above <see cref="Body"/>, and as the card header title if a
    /// card ends up being emitted (see <see cref="AdditionalTextWidgetsMarkdown"/> and <see cref="Buttons"/>).
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// An optional alert severity to call out.
    /// </summary>
    public GoogleChatAlertLevel? AlertLevel { get; init; }

    /// <summary>
    /// Additional card text widgets. Each entry is standard markdown, converted to the restricted HTML subset
    /// supported by Google Chat's TextParagraph widget. Providing any entries causes a card to be emitted.
    /// </summary>
    public IReadOnlyList<string> AdditionalTextWidgetsMarkdown { get; init; } = [];

    /// <summary>
    /// Card buttons. Providing any buttons causes a card to be emitted.
    /// </summary>
    public IReadOnlyList<GoogleChatButton> Buttons { get; init; } = [];

    /// <summary>
    /// Google Workspace numeric user resource IDs to mention. Incoming webhooks cannot resolve users by email
    /// address, so callers must supply the numeric ID (e.g. from the People API or Admin console).
    /// </summary>
    public IReadOnlyList<string> MentionUserIds { get; init; } = [];
}
