using Sagara.Core.Google.Chat.Internal;

namespace Sagara.Core.Google.Tests.Chat;

public class ChatCardHtmlRendererTests
{
    [Theory]
    [InlineData("**bold**", "<b>bold</b>")]
    [InlineData("__bold__", "<b>bold</b>")]
    [InlineData("*italic*", "<i>italic</i>")]
    [InlineData("_italic_", "<i>italic</i>")]
    [InlineData("~~strike~~", "<s>strike</s>")]
    [InlineData("[text](https://example.com)", "<a href=\"https://example.com\">text</a>")]
    [InlineData("line one  \nline two", "line one<br>line two")]
    [InlineData("`code`", "code")]
    public void ToTextParagraphHtml_SingleInlineConstruct_RendersExpectedHtml(string markdown, string expected)
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml(markdown);

        Assert.Equal(expected, html);
    }

    [Fact]
    public void ToTextParagraphHtml_MultipleParagraphs_SeparatedByDoubleBr()
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml("para one\n\npara two");

        Assert.Equal("para one<br><br>para two", html);
    }

    [Fact]
    public void ToTextParagraphHtml_Heading_RendersAsBoldLine()
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml("# Title\n\nbody");

        Assert.Equal("<b>Title</b><br><br>body", html);
    }

    [Fact]
    public void ToTextParagraphHtml_List_RendersAsBulletedLines()
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml("- one\n- two\n- three");

        Assert.Equal("• one<br>• two<br>• three", html);
    }

    [Fact]
    public void ToTextParagraphHtml_Blockquote_RendersChildrenOnly()
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml("> quoted text");

        Assert.Equal("quoted text", html);
    }

    [Fact]
    public void ToTextParagraphHtml_RawHtmlBlock_IsEscapedNotPassedThrough()
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml("<script>alert(1)</script>");

        Assert.DoesNotContain("<script>", html, StringComparison.Ordinal);
        Assert.Contains("&lt;script&gt;", html, StringComparison.Ordinal);
    }

    [Fact]
    public void ToTextParagraphHtml_RawInlineHtml_IsEscapedNotPassedThrough()
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml("before <b>raw</b> after");

        Assert.DoesNotContain("<b>raw</b>", html, StringComparison.Ordinal);
        Assert.Contains("&lt;b&gt;", html, StringComparison.Ordinal);
    }

    [Fact]
    public void ToTextParagraphHtml_Image_RendersAltTextOnlyNoImgTag()
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml("![alt text](https://example.com/image.png)");

        Assert.DoesNotContain("<img", html, StringComparison.Ordinal);
        Assert.Equal("alt text", html);
    }

    [Fact]
    public void ToTextParagraphHtml_ThematicBreak_ProducesNoOutput()
    {
        var html = ChatCardHtmlRenderer.ToTextParagraphHtml("before\n\n---\n\nafter");

        Assert.DoesNotContain("<hr", html, StringComparison.Ordinal);
    }
}
