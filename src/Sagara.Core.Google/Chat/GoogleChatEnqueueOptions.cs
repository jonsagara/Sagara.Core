namespace Sagara.Core.Google.Chat;

/// <summary>
/// Optional per-message settings for <see cref="GoogleChatService"/>'s <c>EnqueueMessageAsync</c> overloads.
/// </summary>
public sealed record GoogleChatEnqueueOptions
{
    /// <summary>
    /// The maximum length of <see cref="ThreadKey"/> and <see cref="IdempotencyKey"/>, as enforced by the relay.
    /// </summary>
    public const int MaxKeyLength = 200;

    /// <summary>
    /// When set, the message is posted as a reply in the thread with this key, or starts that thread if it
    /// doesn't exist yet. Must be non-blank and at most <see cref="MaxKeyLength"/> characters.
    /// </summary>
    public string? ThreadKey { get; init; }

    /// <summary>
    /// When set, a repeat enqueue with the same key to the same space within 24 hours is accepted by the relay but
    /// not sent again, so callers can retry safely. Must be non-blank and at most <see cref="MaxKeyLength"/>
    /// characters.
    /// </summary>
    public string? IdempotencyKey { get; init; }
}
