using Sagara.Core.Google.Chat;

namespace Sagara.Core.Google.Tests.Chat;

public class GoogleChatMarkupConverterTests
{
    [Theory]
    // Emphasis: Markdown delimiters map onto classic ones, and bold vs. italic must not collide.
    [InlineData("**bold**", "*bold*")]
    [InlineData("__bold__", "*bold*")]
    [InlineData("*italic*", "_italic_")]
    [InlineData("_italic_", "_italic_")]
    [InlineData("~~struck~~", "~struck~")]
    [InlineData("***both***", "_*both*_")]
    [InlineData("**bold with _italic_ inside**", "*bold with _italic_ inside*")]
    // A lone asterisk in prose is not emphasis and is left alone.
    [InlineData("2 * 3 = 6", "2 * 3 = 6")]
    // Links become the <url|text> form; when text and URL match, the bare <url> form.
    [InlineData("[Sagara](https://www.sagara.org)", "<https://www.sagara.org|Sagara>")]
    [InlineData("[https://www.sagara.org](https://www.sagara.org)", "<https://www.sagara.org>")]
    [InlineData("<https://www.sagara.org>", "<https://www.sagara.org>")]
    [InlineData("**see [x](https://y)**", "*see <https://y|x>*")]
    // Inline code is already backtick-delimited in classic markup.
    [InlineData("`single line code`", "`single line code`")]
    // Headings have no classic equivalent; render as a bold line.
    [InlineData("# Heading", "*Heading*")]
    [InlineData("### Heading", "*Heading*")]
    // Lists pass through; nested indentation is preserved.
    [InlineData("- a\n- b", "- a\n- b")]
    [InlineData("* a\n* b", "- a\n- b")]
    [InlineData("1. a\n2. b", "1. a\n2. b")]
    [InlineData("- a\n  - b", "- a\n  - b")]
    // Block quotes keep a literal "> " prefix.
    [InlineData("> quote\n> more", "> quote\n> more")]
    // A hard line break inside a paragraph stays a real newline.
    [InlineData("line1  \nline2", "line1\nline2")]
    // Paragraph breaks are preserved.
    [InlineData("p1\n\np2", "p1\n\np2")]
    // Raw HTML (e.g. a caller-supplied mention chip) passes through untouched.
    [InlineData("see <chat-user data-user=\"users/123\"> now", "see <chat-user data-user=\"users/123\"> now")]
    // Plain text is unchanged.
    [InlineData("hello", "hello")]
    public void MarkdownToClassicMarkup_ConvertsAsExpected(string markdown, string expected)
    {
        Assert.Equal(expected, GoogleChatMarkupConverter.MarkdownToClassicMarkup(markdown));
    }

    [Fact]
    public void MarkdownToClassicMarkup_FencedCodeBlock_DropsLanguageInfoString()
    {
        var markdown = "```csharp\nvar jon = \"sagara\";\nvar name = jon.ToUpperInvariant();\n```";
        var expected = "```\nvar jon = \"sagara\";\nvar name = jon.ToUpperInvariant();\n```";

        Assert.Equal(expected, GoogleChatMarkupConverter.MarkdownToClassicMarkup(markdown));
    }

    [Fact]
    public void MarkdownToClassicMarkup_CodeBlockContent_IsNotReinterpretedAsMarkup()
    {
        var markdown = "```\n**not bold** and [not a link](x)\n```";
        var expected = "```\n**not bold** and [not a link](x)\n```";

        Assert.Equal(expected, GoogleChatMarkupConverter.MarkdownToClassicMarkup(markdown));
    }

    [Fact]
    public void MarkdownToClassicMarkup_MixedDocument_KeepsBlockStructure()
    {
        var markdown = "# Deploy failed\n\nThe **prod** deploy failed. See [logs](https://example.com/logs).\n\n- check 1\n- check 2";
        var expected = "*Deploy failed*\n\nThe *prod* deploy failed. See <https://example.com/logs|logs>.\n\n- check 1\n- check 2";

        Assert.Equal(expected, GoogleChatMarkupConverter.MarkdownToClassicMarkup(markdown));
    }

    [Fact]
    public void MarkdownToClassicMarkup_NormalizesWindowsNewlines()
    {
        Assert.Equal("a\n\nb", GoogleChatMarkupConverter.MarkdownToClassicMarkup("a\r\n\r\nb"));
    }
}
