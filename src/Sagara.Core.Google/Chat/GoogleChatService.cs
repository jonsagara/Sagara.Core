using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Markdig;
using Markdig.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sagara.Core.Google.Chat.Payloads;

namespace Sagara.Core.Google.Chat;

/// <summary>
/// Sends messages to a Google Chat space, either directly via an incoming webhook (<c>SendMessageAsync</c>), or
/// through the Google Chat throttling relay, which queues them to stay within Google's per-space rate limit
/// (<c>EnqueueMessageAsync</c>).
/// </summary>
public sealed class GoogleChatService
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly HttpClient _httpClient;
    private readonly ILogger<GoogleChatService> _logger;
    private readonly GoogleChatServiceOptions _options;

    public GoogleChatService(HttpClient httpClient, ILogger<GoogleChatService> logger, IOptions<GoogleChatServiceOptions> options)
    {
        Check.ThrowIfNull(options);

        _httpClient = httpClient;
        _logger = logger;
        _options = options.Value;
    }


    /// <summary>
    /// Sends a Markdown message to a Google Chat space via an incoming webhook.
    /// </summary>
    /// <param name="webhookUrl">The incoming webhook URL for the target Google Chat space.</param>
    /// <param name="bodyMarkdown">The message body, in Markdown.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="ArgumentNullException"><paramref name="webhookUrl"/> or <paramref name="bodyMarkdown"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="webhookUrl"/> or <paramref name="bodyMarkdown"/> is empty or whitespace.</exception>
    /// <remarks>
    /// Failures while sending (HTTP errors or unhandled exceptions) are logged and swallowed rather than thrown.
    /// </remarks>
    public async Task SendMessageAsync(
        string webhookUrl,
        string bodyMarkdown,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);

        try
        {
            var payload = BuildPayload(
                bodyMarkdown: bodyMarkdown,
                mentionAllUsers: false,
                mentionUsers: null,
                cards: null);

            await PostMessageAsync(webhookUrl, payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Log and swallow.
            _logger.Error_UnhandledException(ex, bodyMarkdown);
        }
    }

    /// <summary>
    /// Sends a Markdown message to a Google Chat space via an incoming webhook, optionally mentioning all
    /// space members.
    /// </summary>
    /// <param name="webhookUrl">The incoming webhook URL for the target Google Chat space.</param>
    /// <param name="bodyMarkdown">The message body, in Markdown.</param>
    /// <param name="mentionAllUsers">When <see langword="true"/>, appends an @all mention that notifies every member of the space.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="ArgumentNullException"><paramref name="webhookUrl"/> or <paramref name="bodyMarkdown"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="webhookUrl"/> or <paramref name="bodyMarkdown"/> is empty or whitespace.</exception>
    /// <remarks>
    /// Failures while sending (HTTP errors or unhandled exceptions) are logged and swallowed rather than thrown.
    /// </remarks>
    public async Task SendMessageAsync(
        string webhookUrl,
        string bodyMarkdown,
        bool mentionAllUsers,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);

        try
        {
            var payload = BuildPayload(
                bodyMarkdown: bodyMarkdown,
                mentionAllUsers: mentionAllUsers,
                mentionUsers: null,
                cards: null);

            await PostMessageAsync(webhookUrl, payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Log and swallow.
            _logger.Error_UnhandledException(ex, bodyMarkdown);
        }
    }

    /// <summary>
    /// Sends a Markdown message to a Google Chat space via an incoming webhook, mentioning specific users.
    /// </summary>
    /// <param name="webhookUrl">The incoming webhook URL for the target Google Chat space.</param>
    /// <param name="bodyMarkdown">The message body, in Markdown.</param>
    /// <param name="mentionUsers">The Google Workspace users to mention (notify) in the message.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="ArgumentNullException"><paramref name="webhookUrl"/>, <paramref name="bodyMarkdown"/>, or <paramref name="mentionUsers"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="webhookUrl"/> or <paramref name="bodyMarkdown"/> is empty or whitespace.</exception>
    /// <remarks>
    /// Failures while sending (HTTP errors or unhandled exceptions) are logged and swallowed rather than thrown.
    /// </remarks>
    public async Task SendMessageAsync(
        string webhookUrl,
        string bodyMarkdown,
        IReadOnlyCollection<GoogleWorkspaceUser> mentionUsers,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);
        Check.ThrowIfNull(mentionUsers);

        try
        {
            var payload = BuildPayload(
                bodyMarkdown: bodyMarkdown,
                mentionAllUsers: false,
                mentionUsers: mentionUsers,
                cards: null);

            await PostMessageAsync(webhookUrl, payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Log and swallow.
            _logger.Error_UnhandledException(ex, bodyMarkdown);
        }
    }

    /// <summary>
    /// Sends a message containing cards, and optionally a Markdown body, to a Google Chat space via an
    /// incoming webhook.
    /// </summary>
    /// <param name="webhookUrl">The incoming webhook URL for the target Google Chat space.</param>
    /// <param name="bodyMarkdown">The message body, in Markdown. May be <see langword="null"/> or empty if <paramref name="cards"/> supplies the content.</param>
    /// <param name="cards">The cards to attach to the message.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="ArgumentNullException"><paramref name="webhookUrl"/> or <paramref name="cards"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="webhookUrl"/> is empty or whitespace; or both <paramref name="bodyMarkdown"/> and
    /// <paramref name="cards"/> are empty; or a card in <paramref name="cards"/> has no content set.
    /// </exception>
    /// <remarks>
    /// Failures while sending (HTTP errors or unhandled exceptions) are logged and swallowed rather than thrown.
    /// </remarks>
    public async Task SendMessageAsync(
        string webhookUrl,
        string? bodyMarkdown,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNull(cards);
        ValidateContentToSend(bodyMarkdown, cards);

        try
        {
            var payload = BuildPayload(
                bodyMarkdown: bodyMarkdown,
                mentionAllUsers: false,
                mentionUsers: null,
                cards: cards);

            await PostMessageAsync(webhookUrl, payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Log and swallow.
            _logger.Error_UnhandledException(ex, bodyMarkdown);
        }
    }

    /// <summary>
    /// Sends a message containing cards, and optionally a Markdown body, to a Google Chat space via an
    /// incoming webhook, optionally mentioning all space members.
    /// </summary>
    /// <param name="webhookUrl">The incoming webhook URL for the target Google Chat space.</param>
    /// <param name="bodyMarkdown">The message body, in Markdown. May be <see langword="null"/> or empty if <paramref name="cards"/> supplies the content.</param>
    /// <param name="mentionAllUsers">When <see langword="true"/>, appends an @all mention that notifies every member of the space.</param>
    /// <param name="cards">The cards to attach to the message.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="ArgumentNullException"><paramref name="webhookUrl"/> or <paramref name="cards"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="webhookUrl"/> is empty or whitespace; or both <paramref name="bodyMarkdown"/> and
    /// <paramref name="cards"/> are empty; or a card in <paramref name="cards"/> has no content set.
    /// </exception>
    /// <remarks>
    /// Failures while sending (HTTP errors or unhandled exceptions) are logged and swallowed rather than thrown.
    /// </remarks>
    public async Task SendMessageAsync(
        string webhookUrl,
        string? bodyMarkdown,
        bool mentionAllUsers,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNull(cards);
        ValidateContentToSend(bodyMarkdown, cards);

        try
        {
            var payload = BuildPayload(
                bodyMarkdown: bodyMarkdown,
                mentionAllUsers: mentionAllUsers,
                mentionUsers: null,
                cards: cards);

            await PostMessageAsync(webhookUrl, payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Log and swallow.
            _logger.Error_UnhandledException(ex, bodyMarkdown);
        }
    }

    /// <summary>
    /// Sends a message containing cards, and optionally a Markdown body, to a Google Chat space via an
    /// incoming webhook, mentioning specific users.
    /// </summary>
    /// <param name="webhookUrl">The incoming webhook URL for the target Google Chat space.</param>
    /// <param name="bodyMarkdown">The message body, in Markdown. May be <see langword="null"/> or empty if <paramref name="cards"/> supplies the content.</param>
    /// <param name="mentionUsers">The Google Workspace users to mention (notify) in the message.</param>
    /// <param name="cards">The cards to attach to the message.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="ArgumentNullException"><paramref name="webhookUrl"/>, <paramref name="mentionUsers"/>, or <paramref name="cards"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="webhookUrl"/> is empty or whitespace; or both <paramref name="bodyMarkdown"/> and
    /// <paramref name="cards"/> are empty; or a card in <paramref name="cards"/> has no content set.
    /// </exception>
    /// <remarks>
    /// Failures while sending (HTTP errors or unhandled exceptions) are logged and swallowed rather than thrown.
    /// </remarks>
    public async Task SendMessageAsync(
        string webhookUrl,
        string? bodyMarkdown,
        IReadOnlyCollection<GoogleWorkspaceUser> mentionUsers,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);
        Check.ThrowIfNull(mentionUsers);
        Check.ThrowIfNull(cards);
        ValidateContentToSend(bodyMarkdown, cards);

        try
        {
            var payload = BuildPayload(
                bodyMarkdown: bodyMarkdown,
                mentionAllUsers: false,
                mentionUsers: mentionUsers,
                cards: cards);

            await PostMessageAsync(webhookUrl, payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Log and swallow.
            _logger.Error_UnhandledException(ex, bodyMarkdown);
        }
    }


    /// <summary>
    /// Enqueues a Markdown message with the Google Chat throttling relay, which forwards it to the named space
    /// while staying within Google's per-space rate limit.
    /// </summary>
    /// <param name="spaceName">The relay's name for the target Google Chat space (a key in the relay's webhook map, not a webhook URL).</param>
    /// <param name="bodyMarkdown">The message body, in Markdown.</param>
    /// <param name="enqueueOptions">Optional thread and idempotency keys.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The outcome. <see cref="GoogleChatEnqueueResult.Succeeded"/> means the relay accepted the message, not that Google delivered it.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="spaceName"/> or <paramref name="bodyMarkdown"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="spaceName"/> or <paramref name="bodyMarkdown"/> is empty or whitespace; or a key in <paramref name="enqueueOptions"/> is invalid.</exception>
    /// <exception cref="InvalidOperationException"><see cref="GoogleChatServiceOptions.Relay"/> is not configured.</exception>
    /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
    /// <remarks>
    /// Other failures (HTTP errors or unhandled exceptions) are logged and returned as an unsuccessful result rather than thrown.
    /// </remarks>
    public async Task<GoogleChatEnqueueResult> EnqueueMessageAsync(
        string spaceName,
        string bodyMarkdown,
        GoogleChatEnqueueOptions? enqueueOptions = null,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(spaceName);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);

        return await EnqueueCoreAsync(
            spaceName: spaceName,
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: false,
            mentionUsers: null,
            cards: null,
            enqueueOptions: enqueueOptions,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Enqueues a Markdown message with the Google Chat throttling relay, optionally mentioning all space members.
    /// </summary>
    /// <param name="spaceName">The relay's name for the target Google Chat space (a key in the relay's webhook map, not a webhook URL).</param>
    /// <param name="bodyMarkdown">The message body, in Markdown.</param>
    /// <param name="mentionAllUsers">When <see langword="true"/>, appends an @all mention that notifies every member of the space.</param>
    /// <param name="enqueueOptions">Optional thread and idempotency keys.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The outcome. <see cref="GoogleChatEnqueueResult.Succeeded"/> means the relay accepted the message, not that Google delivered it.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="spaceName"/> or <paramref name="bodyMarkdown"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="spaceName"/> or <paramref name="bodyMarkdown"/> is empty or whitespace; or a key in <paramref name="enqueueOptions"/> is invalid.</exception>
    /// <exception cref="InvalidOperationException"><see cref="GoogleChatServiceOptions.Relay"/> is not configured.</exception>
    /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
    /// <remarks>
    /// Other failures (HTTP errors or unhandled exceptions) are logged and returned as an unsuccessful result rather than thrown.
    /// </remarks>
    public async Task<GoogleChatEnqueueResult> EnqueueMessageAsync(
        string spaceName,
        string bodyMarkdown,
        bool mentionAllUsers,
        GoogleChatEnqueueOptions? enqueueOptions = null,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(spaceName);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);

        return await EnqueueCoreAsync(
            spaceName: spaceName,
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: mentionAllUsers,
            mentionUsers: null,
            cards: null,
            enqueueOptions: enqueueOptions,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Enqueues a Markdown message with the Google Chat throttling relay, mentioning specific users.
    /// </summary>
    /// <param name="spaceName">The relay's name for the target Google Chat space (a key in the relay's webhook map, not a webhook URL).</param>
    /// <param name="bodyMarkdown">The message body, in Markdown.</param>
    /// <param name="mentionUsers">The Google Workspace users to mention (notify) in the message.</param>
    /// <param name="enqueueOptions">Optional thread and idempotency keys.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The outcome. <see cref="GoogleChatEnqueueResult.Succeeded"/> means the relay accepted the message, not that Google delivered it.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="spaceName"/>, <paramref name="bodyMarkdown"/>, or <paramref name="mentionUsers"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="spaceName"/> or <paramref name="bodyMarkdown"/> is empty or whitespace; or a key in <paramref name="enqueueOptions"/> is invalid.</exception>
    /// <exception cref="InvalidOperationException"><see cref="GoogleChatServiceOptions.Relay"/> is not configured.</exception>
    /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
    /// <remarks>
    /// Other failures (HTTP errors or unhandled exceptions) are logged and returned as an unsuccessful result rather than thrown.
    /// </remarks>
    public async Task<GoogleChatEnqueueResult> EnqueueMessageAsync(
        string spaceName,
        string bodyMarkdown,
        IReadOnlyCollection<GoogleWorkspaceUser> mentionUsers,
        GoogleChatEnqueueOptions? enqueueOptions = null,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(spaceName);
        Check.ThrowIfNullOrWhiteSpace(bodyMarkdown);
        Check.ThrowIfNull(mentionUsers);

        return await EnqueueCoreAsync(
            spaceName: spaceName,
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: false,
            mentionUsers: mentionUsers,
            cards: null,
            enqueueOptions: enqueueOptions,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Enqueues a message containing cards, and optionally a Markdown body, with the Google Chat throttling relay.
    /// </summary>
    /// <param name="spaceName">The relay's name for the target Google Chat space (a key in the relay's webhook map, not a webhook URL).</param>
    /// <param name="bodyMarkdown">The message body, in Markdown. May be <see langword="null"/> or empty if <paramref name="cards"/> supplies the content.</param>
    /// <param name="cards">The cards to attach to the message.</param>
    /// <param name="enqueueOptions">Optional thread and idempotency keys.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The outcome. <see cref="GoogleChatEnqueueResult.Succeeded"/> means the relay accepted the message, not that Google delivered it.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="spaceName"/> or <paramref name="cards"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="spaceName"/> is empty or whitespace; or both <paramref name="bodyMarkdown"/> and
    /// <paramref name="cards"/> are empty; or a card in <paramref name="cards"/> has no content set; or a key in
    /// <paramref name="enqueueOptions"/> is invalid.
    /// </exception>
    /// <exception cref="InvalidOperationException"><see cref="GoogleChatServiceOptions.Relay"/> is not configured.</exception>
    /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
    /// <remarks>
    /// Other failures (HTTP errors or unhandled exceptions) are logged and returned as an unsuccessful result rather than thrown.
    /// </remarks>
    public async Task<GoogleChatEnqueueResult> EnqueueMessageAsync(
        string spaceName,
        string? bodyMarkdown,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        GoogleChatEnqueueOptions? enqueueOptions = null,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(spaceName);
        Check.ThrowIfNull(cards);
        ValidateContentToSend(bodyMarkdown, cards);

        return await EnqueueCoreAsync(
            spaceName: spaceName,
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: false,
            mentionUsers: null,
            cards: cards,
            enqueueOptions: enqueueOptions,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Enqueues a message containing cards, and optionally a Markdown body, with the Google Chat throttling relay,
    /// optionally mentioning all space members.
    /// </summary>
    /// <param name="spaceName">The relay's name for the target Google Chat space (a key in the relay's webhook map, not a webhook URL).</param>
    /// <param name="bodyMarkdown">The message body, in Markdown. May be <see langword="null"/> or empty if <paramref name="cards"/> supplies the content.</param>
    /// <param name="mentionAllUsers">When <see langword="true"/>, appends an @all mention that notifies every member of the space.</param>
    /// <param name="cards">The cards to attach to the message.</param>
    /// <param name="enqueueOptions">Optional thread and idempotency keys.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The outcome. <see cref="GoogleChatEnqueueResult.Succeeded"/> means the relay accepted the message, not that Google delivered it.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="spaceName"/> or <paramref name="cards"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="spaceName"/> is empty or whitespace; or both <paramref name="bodyMarkdown"/> and
    /// <paramref name="cards"/> are empty; or a card in <paramref name="cards"/> has no content set; or a key in
    /// <paramref name="enqueueOptions"/> is invalid.
    /// </exception>
    /// <exception cref="InvalidOperationException"><see cref="GoogleChatServiceOptions.Relay"/> is not configured.</exception>
    /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
    /// <remarks>
    /// Other failures (HTTP errors or unhandled exceptions) are logged and returned as an unsuccessful result rather than thrown.
    /// </remarks>
    public async Task<GoogleChatEnqueueResult> EnqueueMessageAsync(
        string spaceName,
        string? bodyMarkdown,
        bool mentionAllUsers,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        GoogleChatEnqueueOptions? enqueueOptions = null,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(spaceName);
        Check.ThrowIfNull(cards);
        ValidateContentToSend(bodyMarkdown, cards);

        return await EnqueueCoreAsync(
            spaceName: spaceName,
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: mentionAllUsers,
            mentionUsers: null,
            cards: cards,
            enqueueOptions: enqueueOptions,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Enqueues a message containing cards, and optionally a Markdown body, with the Google Chat throttling relay,
    /// mentioning specific users.
    /// </summary>
    /// <param name="spaceName">The relay's name for the target Google Chat space (a key in the relay's webhook map, not a webhook URL).</param>
    /// <param name="bodyMarkdown">The message body, in Markdown. May be <see langword="null"/> or empty if <paramref name="cards"/> supplies the content.</param>
    /// <param name="mentionUsers">The Google Workspace users to mention (notify) in the message.</param>
    /// <param name="cards">The cards to attach to the message.</param>
    /// <param name="enqueueOptions">Optional thread and idempotency keys.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The outcome. <see cref="GoogleChatEnqueueResult.Succeeded"/> means the relay accepted the message, not that Google delivered it.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="spaceName"/>, <paramref name="mentionUsers"/>, or <paramref name="cards"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="spaceName"/> is empty or whitespace; or both <paramref name="bodyMarkdown"/> and
    /// <paramref name="cards"/> are empty; or a card in <paramref name="cards"/> has no content set; or a key in
    /// <paramref name="enqueueOptions"/> is invalid.
    /// </exception>
    /// <exception cref="InvalidOperationException"><see cref="GoogleChatServiceOptions.Relay"/> is not configured.</exception>
    /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
    /// <remarks>
    /// Other failures (HTTP errors or unhandled exceptions) are logged and returned as an unsuccessful result rather than thrown.
    /// </remarks>
    public async Task<GoogleChatEnqueueResult> EnqueueMessageAsync(
        string spaceName,
        string? bodyMarkdown,
        IReadOnlyCollection<GoogleWorkspaceUser> mentionUsers,
        IReadOnlyCollection<GoogleChatCardV2> cards,
        GoogleChatEnqueueOptions? enqueueOptions = null,
        CancellationToken cancellationToken = default)
    {
        Check.ThrowIfNullOrWhiteSpace(spaceName);
        Check.ThrowIfNull(mentionUsers);
        Check.ThrowIfNull(cards);
        ValidateContentToSend(bodyMarkdown, cards);

        return await EnqueueCoreAsync(
            spaceName: spaceName,
            bodyMarkdown: bodyMarkdown,
            mentionAllUsers: false,
            mentionUsers: mentionUsers,
            cards: cards,
            enqueueOptions: enqueueOptions,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }


    //
    // Private methods
    //

    private static void ValidateEnqueueOptions(GoogleChatEnqueueOptions? enqueueOptions)
    {
        if (enqueueOptions is null)
        {
            return;
        }

        ValidateKey(enqueueOptions.ThreadKey, nameof(GoogleChatEnqueueOptions.ThreadKey));
        ValidateKey(enqueueOptions.IdempotencyKey, nameof(GoogleChatEnqueueOptions.IdempotencyKey));

        static void ValidateKey(string? key, string keyName)
        {
            // Mirror the relay's own validation, so a bad key fails fast here instead of as a 400 from the relay.
            if (key is not null && (string.IsNullOrWhiteSpace(key) || key.Length > GoogleChatEnqueueOptions.MaxKeyLength))
            {
                throw new ArgumentException($"{nameof(GoogleChatEnqueueOptions)}.{keyName} must be non-blank and at most {GoogleChatEnqueueOptions.MaxKeyLength} characters.", nameof(enqueueOptions));
            }
        }
    }

    private (Uri SendUri, string ApiToken) GetRelayEndpoint()
    {
        // The validator guarantees both values are present whenever Relay is set; Relay itself is optional.
        if (_options.Relay is not { BaseAddress: { } baseAddress, ApiToken: { } apiToken })
        {
            throw new InvalidOperationException($"The Google Chat throttling relay is not configured. Set {nameof(GoogleChatServiceOptions)}.{nameof(GoogleChatServiceOptions.Relay)} to use EnqueueMessageAsync.");
        }

        // Ensure a trailing slash so a base address with a path prefix keeps it when "send" is appended.
        //   (Indexing sidesteps conflicting analyzer advice: CA1307 on net11.0 vs. CA1865 for EndsWith overloads.)
        var baseWithSlash = baseAddress.AbsoluteUri[^1] == '/'
            ? baseAddress
            : new Uri($"{baseAddress.AbsoluteUri}/");

        return (new Uri(baseWithSlash, "send"), apiToken);
    }

    private async Task<GoogleChatEnqueueResult> EnqueueCoreAsync(
        string spaceName,
        string? bodyMarkdown,
        bool mentionAllUsers,
        IReadOnlyCollection<GoogleWorkspaceUser>? mentionUsers,
        IReadOnlyCollection<GoogleChatCardV2>? cards,
        GoogleChatEnqueueOptions? enqueueOptions,
        CancellationToken cancellationToken)
    {
        // Configuration and argument errors are thrown, not swallowed: they're bugs, not transient failures.
        var (sendUri, apiToken) = GetRelayEndpoint();
        ValidateEnqueueOptions(enqueueOptions);

        try
        {
            var payload = new RelaySendRequestPayload
            {
                Space = spaceName,
                Message = BuildPayload(
                    bodyMarkdown: bodyMarkdown,
                    mentionAllUsers: mentionAllUsers,
                    mentionUsers: mentionUsers,
                    cards: cards),
                ThreadKey = enqueueOptions?.ThreadKey,
                IdempotencyKey = enqueueOptions?.IdempotencyKey,
            };

            // The bearer token goes on this request only. Never put it in HttpClient.DefaultRequestHeaders: this
            //   same client posts directly to Google in SendMessageAsync, and the token would leak there.
            using var request = new HttpRequestMessage(HttpMethod.Post, sendUri)
            {
                Content = JsonContent.Create(payload, options: _jsonSerializerOptions),
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            using var response = await _httpClient
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(false);

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            var responsePayload = TryDeserializeRelayResponse(responseContent);

            if (response.IsSuccessStatusCode && responsePayload is { Id: { } id, QueuePosition: { } queuePosition })
            {
                return new GoogleChatEnqueueResult
                {
                    Succeeded = true,
                    StatusCode = response.StatusCode,
                    Id = id,
                    QueuePosition = queuePosition,
                    Duplicate = responsePayload.Duplicate ?? false,
                };
            }

            _logger.Error_RelayRequestFailed(
                spaceName: spaceName,
                payloadJson: JsonSerializer.Serialize(payload, _jsonSerializerOptions),
                statusCodeInt: (int)response.StatusCode,
                statusCode: response.StatusCode,
                responseBody: responseContent);

            return new GoogleChatEnqueueResult
            {
                Succeeded = false,
                StatusCode = response.StatusCode,
                Error = responsePayload?.Error
                    ?? (response.IsSuccessStatusCode
                        ? "The relay's response did not contain the expected queue fields."
                        : response.ReasonPhrase ?? $"HTTP {(int)response.StatusCode}"),
            };
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // The caller canceled; that's not a failure to report. (An HttpClient timeout also surfaces as an
            //   OperationCanceledException, but without the caller's token canceled, so it's handled below.)
            throw;
        }
        catch (Exception ex)
        {
            // Log and return an unsuccessful result.
            _logger.Error_RelayUnhandledException(ex, spaceName, bodyMarkdown);

            return new GoogleChatEnqueueResult
            {
                Succeeded = false,
                StatusCode = null,
                Error = ex.Message,
            };
        }
    }

    private static RelaySendResponsePayload? TryDeserializeRelayResponse(string responseContent)
    {
        if (string.IsNullOrWhiteSpace(responseContent))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<RelaySendResponsePayload>(responseContent, _jsonSerializerOptions);
        }
        catch (JsonException)
        {
            // Not JSON (e.g., an HTML error page from a proxy). The raw body is still logged by the caller.
            return null;
        }
    }

    private static void ValidateContentToSend(string? bodyMarkdown, IReadOnlyCollection<GoogleChatCardV2> cards)
    {
        // We must have at least one of bodyMarkdown or cards to send a message. Google Chat will
        //   reject a message with neither.
        if (string.IsNullOrWhiteSpace(bodyMarkdown) && cards.Count == 0)
        {
            throw new ArgumentException($"At least one of {nameof(bodyMarkdown)} or {nameof(cards)} must be provided.", nameof(bodyMarkdown));
        }

        // Sending empty cards does not result in an error from Google Chat, but it does result in
        //   a message with no content, which is not useful. Ensure each card has at least something to send.
        foreach (var card in cards)
        {
            if (string.IsNullOrWhiteSpace(card.SectionHeader) &&
                string.IsNullOrWhiteSpace(card.Title) &&
                string.IsNullOrWhiteSpace(card.Subtitle) &&
                card.AlertLevel is null &&
                (card.TextParagraphMarkdowns is null || card.TextParagraphMarkdowns.Count == 0) &&
                (card.Buttons is null || card.Buttons.Count == 0))
            {
                throw new ArgumentException($"Card must have at least one of {nameof(GoogleChatCardV2.SectionHeader)}, {nameof(GoogleChatCardV2.Title)}, {nameof(GoogleChatCardV2.Subtitle)}, {nameof(GoogleChatCardV2.AlertLevel)}, {nameof(GoogleChatCardV2.TextParagraphMarkdowns)}, or {nameof(GoogleChatCardV2.Buttons)} set.", nameof(cards));
            }
        }
    }

    private ChatMessagePayload BuildPayload(
        string? bodyMarkdown,
        bool mentionAllUsers,
        IReadOnlyCollection<GoogleWorkspaceUser>? mentionUsers,
        IReadOnlyCollection<GoogleChatCardV2>? cards = null)
    {
        return new ChatMessagePayload
        {
            Text = BuildText(
                bodyMarkdown: bodyMarkdown,
                mentionAllUsers: mentionAllUsers,
                mentionUsers: mentionUsers),
            // When the body is converted to classic markup the field is omitted: classic markup is the
            //   webhook default, and MARKUP_SYNTAX_MARKDOWN is not honored for the top-level body anyway.
            MarkupSyntax = _options.ConvertBodyToClassicMarkup
                ? null
                : "MARKUP_SYNTAX_MARKDOWN",
            // A card is only worth emitting when there's card-specific content to show.
            CardsV2 = cards is { Count: > 0 }
                ? BuildCards(cards)
                : null,
        };
    }

    private string BuildText(string? bodyMarkdown, bool mentionAllUsers, IReadOnlyCollection<GoogleWorkspaceUser>? mentionUsers)
    {
        var body = _options.ConvertBodyToClassicMarkup && !string.IsNullOrEmpty(bodyMarkdown)
            ? GoogleChatMarkupConverter.MarkdownToClassicMarkup(bodyMarkdown)
            : bodyMarkdown;

        var text = new StringBuilder(body);

        if (mentionAllUsers)
        {
            text.Append("\n\n");
            text.Append(_options.ConvertBodyToClassicMarkup
                ? "<users/all>"
                : "<chat-user data-user=\"users/all\">");
        }
        else if (mentionUsers is { Count: > 0 })
        {
            text.Append("\n\n");
            text.AppendJoin(' ', mentionUsers.Select(BuildUserMention));
        }

        return text.ToString();
    }

    private string BuildUserMention(GoogleWorkspaceUser user)
    {
        if (_options.ConvertBodyToClassicMarkup)
        {
            // Classic markup has no email-mention form, so mention by ID regardless of MentionStyle.
            return $"<users/{user.Id}>";
        }

        return _options.MentionStyle switch
        {
            GoogleChatMentionStyle.Id => $"<chat-user data-user=\"users/{WebUtility.HtmlEncode(user.Id)}\">",
            GoogleChatMentionStyle.Email => $"<chat-user data-email=\"{WebUtility.HtmlEncode(user.Email)}\">",
            _ => throw new InvalidOperationException($"Unsupported {nameof(GoogleChatMentionStyle)}: {_options.MentionStyle}."),
        };
    }

    /// <summary>
    /// Replaces newlines with &lt;br&gt; tags everywhere in <paramref name="markdown"/> except where doing so would
    /// break Markdown block structure that Google's parser relies on real newlines to recognize. Rather than
    /// guessing at that structure with regexes, the markdown is parsed with Markdig and every newline that falls
    /// inside a code block (plus the newline touching each side of it), or between sibling blocks inside a list
    /// (item-to-item, or a list item's own content to a nested list), is left untouched. Newlines in the middle of
    /// a single list item's wrapped content aren't part of either protected category, so they're still converted.
    /// </summary>
    private static string ReplaceNewlinesOutsideCodeBlocks(string markdown)
    {
        var markdownNormalizedNewlines = markdown.Replace("\r\n", "\n", StringComparison.Ordinal);
        var document = Markdown.Parse(markdownNormalizedNewlines);

        var protectedNewlineIndexes = new HashSet<int>();

        foreach (var codeBlock in document.Descendants<CodeBlock>())
        {
            ProtectSpan(protectedNewlineIndexes, markdownNormalizedNewlines, codeBlock.Span.Start, codeBlock.Span.End);
        }

        foreach (var listBlock in document.Descendants<ListBlock>())
        {
            // A list also needs its outer boundary (the newline into it from whatever precedes it, and out of it
            //   to whatever follows) protected, on top of the gaps between its own items/nested lists below - the
            //   same "marker must start on a real line" requirement that applies to a fence's own boundary.
            ProtectBoundaryNewlines(protectedNewlineIndexes, markdownNormalizedNewlines, listBlock.Span.Start, listBlock.Span.End);
            ProtectChildGaps(protectedNewlineIndexes, markdownNormalizedNewlines, listBlock);
        }

        foreach (var listItemBlock in document.Descendants<ListItemBlock>())
        {
            ProtectChildGaps(protectedNewlineIndexes, markdownNormalizedNewlines, listItemBlock);
        }

        var result = new StringBuilder(markdownNormalizedNewlines.Length);

        for (var i = 0; i < markdownNormalizedNewlines.Length; i++)
        {
            if (markdownNormalizedNewlines[i] == '\n' && !protectedNewlineIndexes.Contains(i))
            {
                result.Append("<br>");
            }
            else
            {
                result.Append(markdownNormalizedNewlines[i]);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Marks every newline from <paramref name="start"/> to <paramref name="end"/> (inclusive) as protected, plus
    /// the single newline immediately touching each side of that range if present. Google's Markdown parser only
    /// recognizes a fenced code block as a block-level element when its ``` markers sit on their own line, so the
    /// boundary newlines have to stay real too, or the parser fails to see the fence as closed.
    /// </summary>
    private static void ProtectSpan(HashSet<int> protectedNewlineIndexes, string text, int start, int end)
    {
        if (start > 0 && text[start - 1] == '\n')
        {
            start--;
        }

        if (end + 1 < text.Length && text[end + 1] == '\n')
        {
            end++;
        }

        for (var i = start; i <= end && i < text.Length; i++)
        {
            if (text[i] == '\n')
            {
                protectedNewlineIndexes.Add(i);
            }
        }
    }

    /// <summary>
    /// Marks just the single newline immediately touching each side of the [<paramref name="start"/>,
    /// <paramref name="end"/>] range as protected (unlike <see cref="ProtectSpan"/>, the interior is left alone).
    /// </summary>
    private static void ProtectBoundaryNewlines(HashSet<int> protectedNewlineIndexes, string text, int start, int end)
    {
        if (start > 0 && text[start - 1] == '\n')
        {
            protectedNewlineIndexes.Add(start - 1);
        }

        if (end + 1 < text.Length && text[end + 1] == '\n')
        {
            protectedNewlineIndexes.Add(end + 1);
        }
    }

    /// <summary>
    /// Protects the newline(s) that fall between each pair of consecutive children of <paramref name="container"/>
    /// (e.g. list item to list item, or a list item's own content to a nested list), so those block boundaries
    /// survive for Google's Markdown parser instead of being collapsed into the preceding line's text.
    /// </summary>
    private static void ProtectChildGaps(HashSet<int> protectedNewlineIndexes, string text, ContainerBlock container)
    {
        for (var i = 0; i < container.Count - 1; i++)
        {
            ProtectSpan(protectedNewlineIndexes, text, container[i].Span.End + 1, container[i + 1].Span.Start - 1);
        }
    }

    private static List<ChatCardWrapper> BuildCards(IReadOnlyCollection<GoogleChatCardV2> cards)
    {
        List<ChatCardWrapper> cardWrappers = new(cards.Count);

        foreach (var card in cards)
        {
            var cardWrapper = BuildCard(
                sectionHeader: card.SectionHeader,
                title: card.Title,
                subtitle: card.Subtitle,
                alertLevel: card.AlertLevel,
                textParagraphMarkdowns: card.TextParagraphMarkdowns,
                buttons: card.Buttons);

            cardWrappers.Add(cardWrapper);
        }

        return cardWrappers;
    }

    private static ChatCardWrapper BuildCard(
        string? sectionHeader,
        string? title,
        string? subtitle,
        GoogleChatAlertLevel? alertLevel,
        IReadOnlyCollection<string>? textParagraphMarkdowns,
        IReadOnlyCollection<GoogleChatButton>? buttons)
    {
        List<ChatCardWidget> widgets = [];


        //
        // If the caller specified an alert level and a title, prepend the emoji to the card title.
        //

        if (alertLevel is { } level)
        {
            if (title is not null)
            {
                var alertLevelInfo = GoogleChatAlertLevelInfo.For(level);

                title = $"{alertLevelInfo.Emoji} {title}";
            }
        }


        //
        // Next widget(s): Text paragraphs, if any.
        //

        if (textParagraphMarkdowns is not null)
        {
            foreach (var textParagraphMarkdown in textParagraphMarkdowns)
            {
                // Google Chat collapses card widgets with Markdown treats newlines as space/whitespace separators
                //   instead of HTML paragraph breaks, collapsing consecutive line breaks into a single line. To
                //   preserve formatting, we have to replace newlines with <br> tags. Skip this inside fenced code
                //   blocks so multi-line code samples don't get mangled with literal <br> tags.
                var textParagraphMarkdownWithBRs = ReplaceNewlinesOutsideCodeBlocks(textParagraphMarkdown);

                widgets.Add(new ChatCardWidget
                {
                    TextParagraph = new ChatTextParagraph
                    {
                        Text = textParagraphMarkdownWithBRs,
                        TextSyntax = "MARKDOWN",
                    },
                });
            }
        }


        //
        // Next widget(s): Buttons, if any.

        if (buttons is { Count: > 0 })
        {
            widgets.Add(new ChatCardWidget
            {
                ButtonList = new ChatButtonList
                {
                    Buttons = buttons
                        .Select(button => new ChatButton
                        {
                            Text = button.Text,
                            OnClick = new ChatOnClick
                            {
                                OpenLink = new ChatOpenLink { Url = button.Url },
                            },
                        })
                        .ToList(),
                },
            });
        }

        return new ChatCardWrapper
        {
            CardId = Guid.NewGuid().ToString(),
            Card = new ChatCard
            {
                Header = title is not null || subtitle is not null
                    ? new ChatCardHeader { Title = title, Subtitle = subtitle }
                    : null,
                Sections = [new ChatCardSection { Header = sectionHeader, Widgets = widgets }],
            },
        };
    }

    private async Task PostMessageAsync(string webhookUrl, ChatMessagePayload payload, CancellationToken cancellationToken)
    {
        using var response = await _httpClient
            .PostAsJsonAsync(webhookUrl, payload, _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            _logger.Error_RequestFailed(
                payloadJson: JsonSerializer.Serialize(payload, _jsonSerializerOptions),
                statusCodeInt: (int)response.StatusCode,
                statusCode: response.StatusCode,
                responseBody: responseContent);
        }
    }
}
