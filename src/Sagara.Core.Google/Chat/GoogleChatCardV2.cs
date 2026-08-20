namespace Sagara.Core.Google.Chat;

public sealed record GoogleChatCardV2(
    string? SectionHeader,
    string? Title,
    string? Subtitle,
    GoogleChatAlertLevel? AlertLevel,
    IReadOnlyCollection<string>? TextParagraphMarkdowns,
    IReadOnlyCollection<GoogleChatButton>? Buttons)
{
    public IReadOnlyCollection<string> TextParagraphMarkdowns { get; } = TextParagraphMarkdowns ?? [];
    public IReadOnlyCollection<GoogleChatButton> Buttons { get; } = Buttons ?? [];
}