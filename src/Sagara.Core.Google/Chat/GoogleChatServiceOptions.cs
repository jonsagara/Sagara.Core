namespace Sagara.Core.Google.Chat;

/// <summary>
/// Configuration options for <see cref="GoogleChatService"/>.
/// </summary>
public sealed class GoogleChatServiceOptions
{
    /// <summary>
    /// How to reference a <see cref="GoogleWorkspaceUser"/> when mentioning them in a chat message.
    /// Defaults to <see cref="GoogleChatMentionStyle.Id"/>.
    /// </summary>
    public GoogleChatMentionStyle MentionStyle { get; set; } = GoogleChatMentionStyle.Id;

    /// <summary>
    /// When <see langword="true"/> (the default), the Markdown message body passed to a
    /// <see cref="GoogleChatService"/> <c>SendMessageAsync</c> overload is converted to Google Chat
    /// "classic" markup before sending, and user mentions are emitted in classic (<c>&lt;users/…&gt;</c>)
    /// form.
    /// <para>
    /// Incoming webhooks do not render Markdown in the main message body, so the body has to be sent as
    /// classic markup. This option only affects the top-level message body; Markdown in card
    /// <c>TextParagraph</c> widgets is always left untouched and sent with <c>MARKDOWN</c> text syntax.
    /// </para>
    /// <para>
    /// Classic markup has no email-mention form, so while this option is enabled users are always
    /// mentioned by ID, even when <see cref="MentionStyle"/> is <see cref="GoogleChatMentionStyle.Email"/>.
    /// </para>
    /// </summary>
    public bool ConvertBodyToClassicMarkup { get; set; } = true;
}
