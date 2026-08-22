namespace Sagara.Core.Google.Chat;

/// <summary>
/// Display metadata for each <see cref="GoogleChatAlertLevel"/>.
/// </summary>
internal readonly record struct GoogleChatAlertLevelInfo(string Emoji, string Label, string HexColor)
{
    public static GoogleChatAlertLevelInfo For(GoogleChatAlertLevel level)
        => level switch
        {
            GoogleChatAlertLevel.Info => new GoogleChatAlertLevelInfo("ℹ️", "INFO", "#1a73e8"),
            GoogleChatAlertLevel.Warning => new GoogleChatAlertLevelInfo("⚠️", "WARNING", "#f9ab00"),
            GoogleChatAlertLevel.Error => new GoogleChatAlertLevelInfo("🔴", "ERROR", "#d93025"),
            GoogleChatAlertLevel.Success => new GoogleChatAlertLevelInfo("✅", "SUCCESS", "#188038"),
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, message: null),
        };
}
