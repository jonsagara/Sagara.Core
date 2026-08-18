namespace Sagara.Core.Google.Chat;

/// <summary>
/// A card button that opens <paramref name="Url"/> when clicked. Google Chat incoming webhooks cannot receive
/// interactive callback actions, so this is the only supported button behavior.
/// </summary>
/// <param name="Text">The button's label.</param>
/// <param name="Url">The URL to open when the button is clicked.</param>
public sealed record GoogleChatButton(
    string Text,
    // Justification: Don't make the caller wrap the URL in a Uri object just to construct this object.
#pragma warning disable CA1054 // URI-like parameters should not be strings
#pragma warning disable CA1056 // URI-like parameters should not be strings
    string Url);
#pragma warning restore CA1056 // URI-like parameters should not be strings
#pragma warning restore CA1054 // URI-like parameters should not be strings
