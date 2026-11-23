using System.Net;

namespace Sagara.Core.Google.Chat;

/// <summary>
/// The outcome of a <see cref="GoogleChatService"/> <c>EnqueueMessageAsync</c> call.
/// </summary>
/// <remarks>
/// <see cref="Succeeded"/> means the relay <em>accepted</em> the message into its queue, not that Google
/// delivered it. The relay delivers later, at least once; messages that Google permanently rejects are moved to
/// the relay's dead letters.
/// </remarks>
public sealed record GoogleChatEnqueueResult
{
    /// <summary>
    /// <see langword="true"/> if the relay accepted the message; otherwise, <see langword="false"/>, and
    /// <see cref="Error"/> describes why.
    /// </summary>
    public required bool Succeeded { get; init; }

    /// <summary>
    /// The relay's HTTP status code, or <see langword="null"/> if no response was received (e.g., a network
    /// error or timeout).
    /// </summary>
    public HttpStatusCode? StatusCode { get; init; }

    /// <summary>
    /// When <see cref="Succeeded"/> is <see langword="false"/>, the relay's error message or the exception
    /// message. Otherwise, <see langword="null"/>.
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// When <see cref="Succeeded"/> is <see langword="true"/>, the relay's per-space sequence number for the
    /// message. Otherwise, <see langword="null"/>.
    /// </summary>
    public long? Id { get; init; }

    /// <summary>
    /// When <see cref="Succeeded"/> is <see langword="true"/>, the message's position in the space's queue
    /// (<c>1</c> = next to be sent), or <c>0</c> if it was a duplicate that has already been sent. Otherwise,
    /// <see langword="null"/>.
    /// </summary>
    public int? QueuePosition { get; init; }

    /// <summary>
    /// <see langword="true"/> if the message's <see cref="GoogleChatEnqueueOptions.IdempotencyKey"/> matched a
    /// message enqueued in the last 24 hours, so this one was not queued again.
    /// </summary>
    public bool Duplicate { get; init; }
}
