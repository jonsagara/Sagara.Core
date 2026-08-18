namespace Sagara.Core.Google.Chat;

/// <summary>
/// A card button that opens <paramref name="Url"/> when clicked. Google Chat incoming webhooks cannot receive
/// interactive callback actions, so this is the only supported button behavior.
/// </summary>
/// <param name="Text">The button's label.</param>
/// <param name="Url">The URL to open when the button is clicked.</param>
public sealed record GoogleChatButton(string Text, string Url);
