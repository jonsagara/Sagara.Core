namespace Sagara.Core.Google.Chat;

/// <summary>
/// An optional alert severity to call out in a message sent via <see cref="GoogleChatService.SendMessageAsync"/>.
/// </summary>
public enum GoogleChatAlertLevel
{
    Info,
    Warning,
    Error,
    Success,
}
