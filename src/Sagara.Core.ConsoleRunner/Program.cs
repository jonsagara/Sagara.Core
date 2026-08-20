using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sagara.Core.Google.Chat;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddGoogleChatService();
    })
    .Build();

var googleChatSvc = host.Services.GetRequiredService<GoogleChatService>();

const string jonWebhookUrl = "";

//// Send a simple message.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: "Hello, Jon!");

//// Send a simple message. Mention all users.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: "Hello, Jon!",
//    mentionAllUsers: true);

//// Send a simple message. Mention users by email.
//// NOTE: a non-existent email address will not cause an error; the mention will render as <chat-user>.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: "Hello, Jon!",
//    mentionUsers: [new GoogleWorkspaceUser(Email: "jon@example.com")]);

//// Send a multi-line text-only message.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: """
//    Hello, Jon!
//    This is a multi-line message.

//    Best regards,
//    Sagara.Core.ConsoleRunner
//    """);

//// Send a message formatted with Markdown.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: """
//    Hello, Jon!  
//    This is a message formatted with **Markdown**.

//    - Item 1
//    - Item 2
//    - Item 3

//    Best regards,  
//    Sagara.Core.ConsoleRunner  
//    [Sagara.org](https://www.sagara.org)
//    """);


//// Send a message formatted with Markdown, and also a card with text formatted as markdown.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: "This is my site: [Sagara.org](https://www.sagara.org)",
//    cards: [
//        new GoogleChatCardV2(
//            SectionHeader: null,//"This is a card header",
//            Title: "Card Title",
//            Subtitle: null,//"Card Subtitle",
//            AlertLevel: null,//GoogleChatAlertLevel.Error,
//            TextParagraphMarkdowns: [
//                "This is a card text widget with **Markdown** formatting. My blog: [Sagara.dev](https://www.sagara.dev)",
//            ],
//            Buttons: null)
//        ]);

//// Send a message formatted with Markdown, and also a card with text formatted as markdown.
////   Both demonstrate all supported formatting options.
//// NOTE: blockquotes are not supported in card text widgets.
//// NOTE: user mentions are not supported in card text widgets.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    body: """
//    This is my site: [Sagara.org](https://www.sagara.org)

//    **Bold**  
//    *Italic*  
//    ~~Strikethrough~~  
//    `single line code`  

//    ```csharp
//    var jon = "sagara";
//    var name = jon.ToUpperInvariant();
//    ```

//    - Simple
//    - List

//    - Nested
//      - List
//        - Third item
//          With subtext

//    1. Numbered
//    1. List

//    > This is a blockquote.  
//    > Also on multiple lines?

//    Mention a user: <chat-user data-user="users/all">
//    """,
//    additionalTextWidgetsMarkdown: [
//        """
//        This is my site: [Sagara.org](https://www.sagara.org)

//        **Bold**
//        *Italic*
//        ~~Strikethrough~~
//        `single line code`

//        ```csharp
//        var jon = "sagara";
//        var name = jon.ToUpperInvariant();
//        ```

//        - Simple
//        - List

//        - Nested
//          - List
//            - Third item
//              With subtext

//        1. Numbered
//        1. List
//        """,
//        ]);
