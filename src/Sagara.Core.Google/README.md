# Sagara.Core.Google

Common Google API integration code that I use across many projects.


## Google Chat

### Breaking changes in 7.0

- `GoogleChatService`'s constructor now takes `IOptions<GoogleChatServiceOptions>` instead of
  `GoogleChatServiceOptions`.
- `GoogleChatServiceOptions` is no longer registered in DI as a plain singleton. Resolve
  `IOptions<GoogleChatServiceOptions>` instead.
- Options are now validated when first resolved, and at host startup via `ValidateOnStart`. Invalid options throw
  `OptionsValidationException`.

### Registration and configuration

Register the service in code, from a configuration section, or from a configuration path. In every case, the
optional callback runs after binding, so values set in code override bound values.

```csharp
// In code
services.AddGoogleChatService(o => o.MentionStyle = GoogleChatMentionStyle.Email);

// From a configuration section
services.AddGoogleChatService(builder.Configuration.GetSection("GoogleChat"));

// From a path in the app's registered IConfiguration
services.AddGoogleChatService("GoogleChat");
```

```json
"GoogleChat": {
  "MentionStyle": "Id",
  "ConvertBodyToClassicMarkup": true,
  "Relay": {
    "BaseAddress": "https://chatrelay.example.com"
  }
}
```

Keep the relay's API token out of `appsettings.json`. Put it in user-secrets locally
(`dotnet user-secrets set "GoogleChat:Relay:ApiToken" "..."`) and in the `GoogleChat__Relay__ApiToken` environment
variable or a secret store in production. Both are merged into the same configuration section automatically.

Options are validated when they're first resolved. In hosted apps (ASP.NET Core, Generic Host) that happens at
startup via `ValidateOnStart`. In console apps without a host, the `OptionsValidationException` is thrown the first
time `GoogleChatService` is resolved.

Configuration binding uses reflection. Apps that publish trimmed or Native AOT can turn on the configuration binder
source generator with `<EnableConfigurationBindingGenerator>true</EnableConfigurationBindingGenerator>`.

### Sending directly vs. enqueueing through the throttling relay

Google Workspace allows one message per second per Chat space. A burst of messages sent straight to a webhook gets
`429` responses.

- **`SendMessageAsync(webhookUrl, ...)`** posts directly to a Google Chat incoming webhook. Failures are logged and
  swallowed.
- **`EnqueueMessageAsync(spaceName, ...)`** posts to the Google Chat throttling relay. The relay queues messages per
  space and forwards them to Google in order, no faster than the limit allows. It requires
  `GoogleChatServiceOptions.Relay`; without it, `EnqueueMessageAsync` throws `InvalidOperationException`.

Each `EnqueueMessageAsync` overload matches a `SendMessageAsync` overload (body, @all mention, user mentions, cards),
with two differences:

- It takes the relay's **space name** (a key in the relay's webhook map, such as `"alerts"`) instead of a webhook
  URL. Webhook URLs stay inside the relay.
- It accepts an optional `GoogleChatEnqueueOptions`:
  - `ThreadKey` replies in that thread, or starts it.
  - `IdempotencyKey` stops repeats within 24 hours from being sent again, so retries are safe.

  Each key must be non-blank and at most 200 characters.

```csharp
var result = await googleChatService.EnqueueMessageAsync(
    "alerts",
    "Disk is **95%** full",
    new GoogleChatEnqueueOptions { ThreadKey = "disk-alerts", IdempotencyKey = $"disk-{alertId}" },
    cancellationToken);

if (!result.Succeeded)
{
    // result.StatusCode is null when no response came back (network error or timeout).
    logger.LogWarning("Relay rejected message: {Status} {Error}", result.StatusCode, result.Error);
}
```

`GoogleChatEnqueueResult` reports:

| Property | Meaning |
|---|---|
| `Succeeded` | The relay accepted the message into its queue. |
| `StatusCode` | The relay's HTTP status, or `null` if no response was received. |
| `Error` | The relay's error message (for example, `Unknown space "alerts"`) or the exception message. |
| `Id` | The relay's per-space sequence number for the message. |
| `QueuePosition` | The message's place in the queue: `1` is next, and `0` means a duplicate that was already sent. |
| `Duplicate` | `IdempotencyKey` matched a message from the last 24 hours, so this one wasn't queued again. |

`Succeeded` means the relay **accepted** the message, not that Google delivered it. The relay delivers at least once
(a message can rarely arrive twice) and moves messages that Google permanently rejects to its dead letters.
Argument and configuration errors throw. Canceling the caller's `CancellationToken` throws
`OperationCanceledException`. Every other failure is logged and returned as `Succeeded = false`.

### Markdown Formatting

Callers always pass the message body and any card `Text Paragraph` widgets as **Markdown**.

For the official documentation of what's supported, see [Format messages](https://developers.google.com/workspace/chat/format-messages).

#### Message body: Markdown is converted to classic markup

Incoming webhooks do **not** render Markdown in the top-level message body, so by default the library
converts the Markdown body to Google Chat "classic" markup before sending
(`GoogleChatServiceOptions.ConvertBodyToClassicMarkup`, default `true`). Set it to `false` to send the body
bytes exactly as supplied. This conversion **never** touches Markdown in card `Text Paragraph` widgets — those
are always sent with `MARKDOWN` text syntax and rendered by Google.

| Markdown you write | Sent as classic markup |
|---|---|
| `[Sagara.org](https://www.sagara.org)` | `<https://www.sagara.org\|Sagara.org>` |
| `**Bold**` / `__Bold__` | `*Bold*` |
| `*Italic*` / `_Italic_` | `_Italic_` |
| `~~Strikethrough~~` | `~Strikethrough~` |
| `` `single line code` `` | `` `single line code` `` (unchanged) |
| `` ```csharp `` … `` ``` `` | `` ``` `` … `` ``` `` (language info string dropped) |
| `- Simple` / `1. Numbered` lists | unchanged (nested indentation preserved) |
| `# Heading` | `*Heading*` (bold line — classic markup has no headings) |
| `> Blockquote` | `> Blockquote` (literal `> ` prefix — classic markup has no blockquote) |
| `![alt](url)` image | `<url\|alt>` (link form — classic markup has no images) |
| Raw HTML, e.g. a `<chat-user …>` mention chip | passed through untouched |

When `ConvertBodyToClassicMarkup` is enabled, user mentions are also emitted in classic form
(`<users/all>`, `<users/USER_ID>`). Google Chat classic markup has **no email-mention form**, so users are
always mentioned by ID while this option is enabled, even when `GoogleChatMentionStyle.Email` is configured.

#### Supported in `Text Paragraph` in cards

> Blockquotes and user mentions are not supported in card `Text Paragraph` widgets.

| Feature | Markdown |
|---|---|
| Link | `[Sagara.org](https://www.sagara.org)` |
| Bold | `**Bold**` |
| Italic | `*Italic*` |
| Strikethrough | `~~Strikethrough~~` |
| Inline code | `` `single line code` `` |
| Code block | `` ```csharp `` <br> `var jon = "sagara";` <br> `var name = jon.ToUpperInvariant();` <br> `` ``` `` |
| Simple list | `- Simple` <br> `- List` |
| Nested list | `- Nested` <br> &nbsp;&nbsp;`  - List` <br>&nbsp;&nbsp;&nbsp;&nbsp; `    - Third item` |
| Numbered list | `1. Numbered` <br> `1. List` |

