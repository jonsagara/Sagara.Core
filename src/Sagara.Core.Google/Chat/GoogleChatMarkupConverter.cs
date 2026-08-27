using System.Text;
using Markdig;
using Markdig.Extensions.EmphasisExtras;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Sagara.Core.Google.Chat;

/// <summary>
/// Converts Markdown into Google Chat "classic" markup for the top-level message <c>text</c> field, which
/// (unlike card <c>TextParagraph</c> widgets) does not render Markdown when a message is sent through an
/// incoming webhook.
/// </summary>
/// <remarks>
/// Classic markup is a small subset of Markdown with different delimiters:
/// <list type="bullet">
///   <item><description><c>**bold**</c> / <c>__bold__</c> becomes <c>*bold*</c></description></item>
///   <item><description><c>*italic*</c> / <c>_italic_</c> becomes <c>_italic_</c></description></item>
///   <item><description><c>~~strikethrough~~</c> becomes <c>~strikethrough~</c></description></item>
///   <item><description><c>[text](url)</c> becomes <c>&lt;url|text&gt;</c></description></item>
///   <item><description>a fenced code block keeps its <c>```</c> fence but drops the language info string</description></item>
/// </list>
/// Constructs with no classic equivalent degrade gracefully: a heading becomes a bold line, a block quote
/// keeps a literal <c>&gt; </c> prefix, an image becomes its link form, a thematic break is dropped, and any
/// raw HTML (for example a caller-supplied <c>&lt;chat-user&gt;</c> mention) passes through untouched.
/// </remarks>
internal static class GoogleChatMarkupConverter
{
    // The bare Markdown pipeline plus strikethrough, which Google Chat markup supports but the default
    //   pipeline does not parse. Nothing else Google lacks (tables, task lists, …) is enabled, which keeps
    //   the set of node types this converter has to handle bounded.
    private static readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
        .UseEmphasisExtras(EmphasisExtraOptions.Strikethrough)
        .Build();

    /// <summary>
    /// Converts <paramref name="markdown"/> to Google Chat classic markup.
    /// </summary>
    public static string MarkdownToClassicMarkup(string markdown)
    {
        Check.ThrowIfNull(markdown);

        var normalized = markdown.Replace("\r\n", "\n", StringComparison.Ordinal);
        var document = Markdown.Parse(normalized, _pipeline);

        var sb = new StringBuilder(normalized.Length);
        WriteBlocks(sb, document, indent: "", blockSeparator: "\n\n");

        return sb.ToString().TrimEnd();
    }

    //
    // Blocks
    //

    private static void WriteBlocks(StringBuilder sb, ContainerBlock container, string indent, string blockSeparator)
    {
        var wroteAny = false;

        foreach (var block in container)
        {
            if (block is LinkReferenceDefinitionGroup)
            {
                // Reference-link definitions produce no visible output; Markdig has already resolved the
                //   links that use them.
                continue;
            }

            var blockStart = sb.Length;

            if (wroteAny)
            {
                sb.Append(blockSeparator);
            }

            var contentStart = sb.Length;
            WriteBlock(sb, block, indent);

            if (sb.Length == contentStart)
            {
                // The block rendered to nothing (e.g. a thematic break); roll back the separator too.
                sb.Length = blockStart;
            }
            else
            {
                wroteAny = true;
            }
        }
    }

    private static void WriteBlock(StringBuilder sb, Block block, string indent)
    {
        switch (block)
        {
            case ParagraphBlock paragraph:
                sb.Append(indent);
                WriteInlines(sb, paragraph.Inline, indent);
                break;

            case HeadingBlock heading:
                // Classic markup has no headings; a bold line is the closest rendering.
                sb.Append(indent).Append('*');
                WriteInlines(sb, heading.Inline, indent);
                sb.Append('*');
                break;

            case QuoteBlock quote:
                WriteQuote(sb, quote, indent);
                break;

            case ListBlock list:
                WriteList(sb, list, indent);
                break;

            // FencedCodeBlock derives from CodeBlock, so it has to be matched first.
            case CodeBlock code:
                WriteCodeBlock(sb, code, indent);
                break;

            case ThematicBreakBlock:
                // No classic equivalent; the surrounding block separators already provide the visual break.
                break;

            case HtmlBlock html:
                WriteRawLines(sb, html.Lines.ToString(), indent);
                break;

            case LeafBlock leaf when leaf.Lines.Count > 0:
                WriteRawLines(sb, leaf.Lines.ToString(), indent);
                break;
        }
    }

    private static void WriteQuote(StringBuilder sb, QuoteBlock quote, string indent)
    {
        // Classic markup has no block quote, so keep a literal "> " prefix on every line.
        var inner = new StringBuilder();
        WriteBlocks(inner, quote, indent: "", blockSeparator: "\n");

        var first = true;
        foreach (var line in inner.ToString().Split('\n'))
        {
            if (!first)
            {
                sb.Append('\n');
            }
            first = false;

            sb.Append(indent).Append("> ").Append(line);
        }
    }

    private static void WriteList(StringBuilder sb, ListBlock list, string indent)
    {
        var number = list.IsOrdered && int.TryParse(list.OrderedStart, out var start) ? start : 1;
        var wroteAny = false;

        foreach (var item in list.Cast<ListItemBlock>())
        {
            if (wroteAny)
            {
                sb.Append('\n');
            }
            wroteAny = true;

            var marker = list.IsOrdered
                ? $"{number}{list.OrderedDelimiter} "
                : "- ";
            number++;

            var childIndent = indent + new string(' ', marker.Length);

            var itemContent = new StringBuilder();
            WriteBlocks(itemContent, item, childIndent, blockSeparator: "\n");

            sb.Append(indent).Append(marker);

            // Every rendered block starts with childIndent; the first line's copy is replaced by the marker
            //   we just wrote, and continuation lines keep their alignment underneath it.
            if (itemContent.Length >= childIndent.Length)
            {
                sb.Append(itemContent.ToString(childIndent.Length, itemContent.Length - childIndent.Length));
            }
        }
    }

    private static void WriteCodeBlock(StringBuilder sb, CodeBlock code, string indent)
    {
        // Classic markup supports a ``` fence but not a language info string, so it is dropped.
        sb.Append(indent).Append("```");

        foreach (var line in code.Lines.ToString().TrimEnd('\n').Split('\n'))
        {
            sb.Append('\n').Append(indent).Append(line);
        }

        sb.Append('\n').Append(indent).Append("```");
    }

    private static void WriteRawLines(StringBuilder sb, string text, string indent)
    {
        var first = true;
        foreach (var line in text.TrimEnd('\n').Split('\n'))
        {
            if (!first)
            {
                sb.Append('\n');
            }
            first = false;

            sb.Append(indent).Append(line);
        }
    }

    //
    // Inlines
    //

    private static void WriteInlines(StringBuilder sb, ContainerInline? container, string indent)
    {
        if (container is null)
        {
            return;
        }

        foreach (var inline in container)
        {
            WriteInline(sb, inline, indent);
        }
    }

    private static void WriteInline(StringBuilder sb, Inline inline, string indent)
    {
        switch (inline)
        {
            case LiteralInline literal:
                sb.Append(literal.Content.AsSpan());
                break;

            case EmphasisInline emphasis:
            {
                var delimiter = emphasis.DelimiterChar == '~'
                    ? "~"
                    : emphasis.DelimiterCount >= 2 ? "*" : "_";

                sb.Append(delimiter);
                WriteInlines(sb, emphasis, indent);
                sb.Append(delimiter);
                break;
            }

            case CodeInline code:
                sb.Append('`').Append(code.Content).Append('`');
                break;

            case LinkInline link:
                WriteLink(sb, link, indent);
                break;

            case AutolinkInline autolink:
                // An email autolink has no classic form; emit the address as plain text.
                if (autolink.IsEmail)
                {
                    sb.Append(autolink.Url);
                }
                else
                {
                    sb.Append('<').Append(autolink.Url).Append('>');
                }
                break;

            case LineBreakInline:
                // Classic markup honours real newlines in the body, so keep the author's line breaks.
                sb.Append('\n').Append(indent);
                break;

            case HtmlInline html:
                // Pass raw HTML through untouched (e.g. a caller-supplied <chat-user> mention chip).
                sb.Append(html.Tag);
                break;

            case HtmlEntityInline entity:
                sb.Append(entity.Transcoded.AsSpan());
                break;

            case ContainerInline containerInline:
                WriteInlines(sb, containerInline, indent);
                break;

            default:
                sb.Append(inline.ToString());
                break;
        }
    }

    private static void WriteLink(StringBuilder sb, LinkInline link, string indent)
    {
        var url = link.Url ?? string.Empty;

        var labelBuilder = new StringBuilder();
        WriteInlines(labelBuilder, link, indent);
        var label = labelBuilder.ToString();

        if (url.Length == 0)
        {
            // Nothing to link to (e.g. an image with no src); fall back to the label.
            sb.Append(label);
            return;
        }

        if (label.Length == 0 || string.Equals(label, url, StringComparison.Ordinal))
        {
            sb.Append('<').Append(url).Append('>');
        }
        else
        {
            sb.Append('<').Append(url).Append('|').Append(label).Append('>');
        }
    }
}
