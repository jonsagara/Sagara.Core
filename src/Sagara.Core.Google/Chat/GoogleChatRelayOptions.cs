namespace Sagara.Core.Google.Chat;

/// <summary>
/// Connection settings for the Google Chat throttling relay: a service that queues messages per Google Chat
/// space and forwards them to Google no faster than Google Workspace's limit of one message per second per space.
/// </summary>
/// <remarks>
/// Both properties are required when the relay is configured. They are validated when the options are first
/// resolved (at host startup, for hosted apps), not here, so that they can be bound from configuration.
/// </remarks>
public sealed class GoogleChatRelayOptions
{
    /// <summary>
    /// The relay's base address, e.g. <c>https://chatrelay.example.com</c>. Must be an absolute <c>https</c> URI.
    /// </summary>
    public Uri? BaseAddress { get; set; }

    /// <summary>
    /// The bearer token sent to the relay in the <c>Authorization</c> header. Treat this as a secret: keep it in
    /// user-secrets, environment variables, or a secret store rather than in <c>appsettings.json</c>.
    /// </summary>
    public string? ApiToken { get; set; }
}
