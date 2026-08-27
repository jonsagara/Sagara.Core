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

//const string jonWebhookUrl = "";

//// Send a simple message.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: "Hello, Jon!");

//// Send no body text. Results in error; guard catches it.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: null);

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
//    mentionUsers: [new GoogleWorkspaceUser(Id: "12345", Email: "jon@example.com")]);
////mentionAllUsers: true);

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
//        new GoogleChatCardV2(Title: "Card Title")
//        {
//            //SectionHeader = null,//"This is a card header",
//            //Subtitle = null,//"Card Subtitle",
//            //AlertLevel = null,//GoogleChatAlertLevel.Error,
//            TextParagraphMarkdowns = [
//                "This is a card text widget with **Markdown** formatting. My blog: [Sagara.dev](https://www.sagara.dev)",
//            ],
//            //Buttons = null,
//            //Buttons = [
//            //    new GoogleChatButton(
//            //        Text: "Click Me",
//            //        Url: "https://www.sagara.org"),
//            //],
//        }]);

//// No body text and no cards. Results in Error.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: null,
//    cards: []);

//// No body text and empty card. Results in Error.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: null,
//    cards: [
//        new GoogleChatCardV2(Title: null),
//        ]);

//// Send a message formatted with Markdown, and also a card with text formatted as markdown.
////   Both demonstrate all supported formatting options.
//// NOTE: blockquotes are not supported in card text widgets.
//// NOTE: user mentions are not supported in card text widgets.
//await googleChatSvc.SendMessageAsync(
//    webhookUrl: jonWebhookUrl,
//    bodyMarkdown: """
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
//    """,
//    cards: [
//        new GoogleChatCardV2(Title: null)
//        {
//            TextParagraphMarkdowns = [
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
//        """
//            ],
//        }
//        ]);
