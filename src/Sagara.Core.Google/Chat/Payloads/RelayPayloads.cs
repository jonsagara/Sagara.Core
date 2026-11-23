using System.Text.Json.Serialization;

namespace Sagara.Core.Google.Chat.Payloads;

/// <summary>
/// Body of the relay's <c>POST /send</c> request.
/// </summary>
internal sealed class RelaySendRequestPayload
{
    [JsonPropertyName("space")]
    public required string Space { get; set; }

    [JsonPropertyName("message")]
    public required ChatMessagePayload Message { get; set; }

    [JsonPropertyName("threadKey")]
    public string? ThreadKey { get; set; }

    [JsonPropertyName("idempotencyKey")]
    public string? IdempotencyKey { get; set; }
}

/// <summary>
/// Body of the relay's <c>POST /send</c> response: the queue fields on success (202), or <c>error</c> on failure.
/// </summary>
internal sealed class RelaySendResponsePayload
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("queuePosition")]
    public int? QueuePosition { get; set; }

    [JsonPropertyName("duplicate")]
    public bool? Duplicate { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
