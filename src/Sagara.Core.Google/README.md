# Sagara.Core.Google

Common Google API integration code that I use across many projects.


## Google Chat

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

