namespace Sagara.Core.Google.Chat;

/// <summary>
/// A Google Workspace user to mention in a chat message.
/// </summary>
/// <param name="Email">The Google Workspace user's email address.</param>
public sealed record GoogleWorkspaceUser(string Email);
