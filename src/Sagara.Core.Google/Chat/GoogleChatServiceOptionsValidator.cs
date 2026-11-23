using Microsoft.Extensions.Options;

namespace Sagara.Core.Google.Chat;

/// <summary>
/// Validates <see cref="GoogleChatServiceOptions"/> when they are first resolved, or at host startup via
/// <c>ValidateOnStart</c>.
/// </summary>
internal sealed class GoogleChatServiceOptionsValidator : IValidateOptions<GoogleChatServiceOptions>
{
    public ValidateOptionsResult Validate(string? name, GoogleChatServiceOptions options)
    {
        Check.ThrowIfNull(options);

        List<string> failures = [];

        if (!Enum.IsDefined(options.MentionStyle))
        {
            failures.Add($"{nameof(GoogleChatServiceOptions.MentionStyle)} has an unsupported value: {options.MentionStyle}.");
        }

        if (options.Relay is { } relay)
        {
            const string relayPrefix = $"{nameof(GoogleChatServiceOptions.Relay)}.";

            if (relay.BaseAddress is null)
            {
                failures.Add($"{relayPrefix}{nameof(GoogleChatRelayOptions.BaseAddress)} is required when {nameof(GoogleChatServiceOptions.Relay)} is configured.");
            }
            else if (!relay.BaseAddress.IsAbsoluteUri || !string.Equals(relay.BaseAddress.Scheme, Uri.UriSchemeHttps, StringComparison.Ordinal))
            {
                // Uri lower-cases the scheme, so an ordinal comparison is enough. The URI itself isn't echoed
                //   in the message, since it could carry user info or query-string credentials.
                failures.Add($"{relayPrefix}{nameof(GoogleChatRelayOptions.BaseAddress)} must be an absolute https URI.");
            }

            if (string.IsNullOrWhiteSpace(relay.ApiToken))
            {
                failures.Add($"{relayPrefix}{nameof(GoogleChatRelayOptions.ApiToken)} is required when {nameof(GoogleChatServiceOptions.Relay)} is configured.");
            }
        }

        return failures.Count > 0
            ? ValidateOptionsResult.Fail(failures)
            : ValidateOptionsResult.Success;
    }
}
