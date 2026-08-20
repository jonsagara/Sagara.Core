# Sagara.Core.Google

Common Google API integration code that I use across many projects.


## Google Chat

### Markdown Formatting

This library uses Markdown formatting for both the message text and any `Text Paragraph` widgets inside of cards.

For the official documentation of what's supported, see [Format messages](https://developers.google.com/workspace/chat/format-messages).

#### Supported in message text

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
| Blockquote | `> This is a blockquote.` <br><br> For multiple lines, end each line with two spaces <br> `> This is a blockquote  ` <br> `> on multiple lines.`|
| Mention a user | `<chat-user data-email="user@inmyworkspaceexample.com">` or `<chat-user data-user="users/all">` |

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

