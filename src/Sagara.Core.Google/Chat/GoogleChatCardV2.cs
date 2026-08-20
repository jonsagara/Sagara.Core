namespace Sagara.Core.Google.Chat;

public sealed record GoogleChatCardV2(string? Title)
{
    public string? SectionHeader { get; init; }
    public string? Subtitle { get; init; }
    public GoogleChatAlertLevel? AlertLevel { get; init; }
    public IReadOnlyCollection<string>? TextParagraphMarkdowns { get; init; }
    public IReadOnlyCollection<GoogleChatButton>? Buttons { get; init; }
}
