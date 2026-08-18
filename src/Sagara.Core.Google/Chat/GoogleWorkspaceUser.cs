namespace Sagara.Core.Google.Chat;

/// <summary>
/// A Google Workspace user to mention in a message sent via <see cref="GoogleChatService.SendMessageAsync"/>.
/// Incoming webhooks cannot resolve users by email address, so <paramref name="Id"/> must be the numeric Google
/// Workspace user resource ID (e.g. from the People API or Admin console).
/// </summary>
/// <param name="Id">The Google Workspace numeric user resource ID.</param>
public sealed record GoogleWorkspaceUser(string Id);
