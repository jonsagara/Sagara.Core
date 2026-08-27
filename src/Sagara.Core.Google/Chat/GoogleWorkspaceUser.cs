namespace Sagara.Core.Google.Chat;

/// <summary>
/// A Google Workspace user to mention in a chat message.
/// </summary>
/// <param name="Id">The Google Workspace user's unique ID.</param>
/// <param name="Email">The Google Workspace user's email address.</param>
public sealed record GoogleWorkspaceUser(string Id, string Email);
