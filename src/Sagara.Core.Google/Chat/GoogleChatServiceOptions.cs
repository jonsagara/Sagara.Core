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
}
