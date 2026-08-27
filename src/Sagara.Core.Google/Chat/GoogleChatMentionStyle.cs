namespace Sagara.Core.Google.Chat;

/// <summary>
/// Specifies how a <see cref="GoogleWorkspaceUser"/> is referenced when mentioned in a chat message.
/// </summary>
public enum GoogleChatMentionStyle
{
    /// <summary>
    /// Mention users by their Google Workspace user ID.
    /// </summary>
    Id,

    /// <summary>
    /// Mention users by their email address.
    /// </summary>
    Email,
}
