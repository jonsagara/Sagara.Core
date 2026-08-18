using System.IO;
using Markdig;
using Markdig.Extensions.EmphasisExtras;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Sagara.Core.Google.Chat.Internal;

/// <summary>
/// Converts standard markdown into the restricted HTML subset supported by Google Chat's TextParagraph widget
/// (&lt;b&gt;, &lt;i&gt;, &lt;s&gt;, &lt;a href&gt;, &lt;br&gt;). Everything else that CommonMark can produce
/// (headings, lists, blockquotes, code blocks, images, raw HTML) is flattened to plain text/line breaks or
/// escaped, rather than passed through as unsupported (or, for raw HTML, unsafe) tags.
/// </summary>
internal static class ChatCardHtmlRenderer
{
    private static readonly MarkdownPipeline s_pipeline = new MarkdownPipelineBuilder()
        .UseEmphasisExtras(EmphasisExtraOptions.Strikethrough)
        .Build();

    public static string ToTextParagraphHtml(string markdown)
    {
        using var writer = new StringWriter();
        var renderer = new TextParagraphHtmlRenderer(writer);

        Markdown.Convert(markdown, renderer, s_pipeline);

        return writer.ToString().Trim();
    }

    /// <summary>
    /// An <see cref="HtmlRenderer"/> whose default per-node-type renderers have been swapped out for ones that
    /// only ever emit tags from the TextParagraph-supported subset.
    /// </summary>
    private sealed class TextParagraphHtmlRenderer : HtmlRenderer
    {
        public TextParagraphHtmlRenderer(TextWriter writer) : base(writer)
        {
            // Neutralizes the untouched default renderers that check these flags: ThematicBreakRenderer (dropped),
            // CodeBlockRenderer (falls back to escaped plain text, no <pre>/<code>), AutolinkInlineRenderer (falls
            // back to escaped plain text instead of <a>). The renderers replaced below don't consult these flags.
            EnableHtmlForBlock = false;
            EnableHtmlForInline = false;

            ObjectRenderers.Replace<ParagraphRenderer>(new RestrictedParagraphRenderer());
            ObjectRenderers.Replace<HeadingRenderer>(new RestrictedHeadingRenderer());
            ObjectRenderers.Replace<ListRenderer>(new RestrictedListRenderer());
            ObjectRenderers.Replace<QuoteBlockRenderer>(new RestrictedQuoteBlockRenderer());
            ObjectRenderers.Replace<HtmlBlockRenderer>(new RestrictedHtmlBlockRenderer());

            ObjectRenderers.Replace<EmphasisInlineRenderer>(new RestrictedEmphasisInlineRenderer());
            ObjectRenderers.Replace<LinkInlineRenderer>(new RestrictedLinkInlineRenderer());
            ObjectRenderers.Replace<LineBreakInlineRenderer>(new RestrictedLineBreakInlineRenderer());
            ObjectRenderers.Replace<CodeInlineRenderer>(new RestrictedCodeInlineRenderer());
            ObjectRenderers.Replace<HtmlInlineRenderer>(new RestrictedHtmlInlineRenderer());
        }
    }

    private sealed class RestrictedParagraphRenderer : HtmlObjectRenderer<ParagraphBlock>
    {
        protected override void Write(HtmlRenderer renderer, ParagraphBlock obj)
        {
            if (renderer.ImplicitParagraph)
            {
                renderer.WriteLeafInline(obj);
                return;
            }

            if (!renderer.IsFirstInContainer)
            {
                renderer.Write("<br><br>");
            }

            renderer.WriteLeafInline(obj);
        }
    }

    private sealed class RestrictedHeadingRenderer : HtmlObjectRenderer<HeadingBlock>
    {
        protected override void Write(HtmlRenderer renderer, HeadingBlock obj)
        {
            if (!renderer.IsFirstInContainer)
            {
                renderer.Write("<br><br>");
            }

            renderer.Write("<b>");
            renderer.WriteLeafInline(obj);
            renderer.Write("</b>");
        }
    }

    private sealed class RestrictedListRenderer : HtmlObjectRenderer<ListBlock>
    {
        protected override void Write(HtmlRenderer renderer, ListBlock obj)
        {
            if (!renderer.IsFirstInContainer)
            {
                renderer.Write("<br><br>");
            }

            var savedImplicitParagraph = renderer.ImplicitParagraph;
            var isFirstItem = true;

            foreach (var item in obj)
            {
                if (!isFirstItem)
                {
                    renderer.Write("<br>");
                }
                isFirstItem = false;

                renderer.Write("• ");
                renderer.ImplicitParagraph = true;
                renderer.WriteChildren((ListItemBlock)item);
            }

            renderer.ImplicitParagraph = savedImplicitParagraph;
        }
    }

    private sealed class RestrictedQuoteBlockRenderer : HtmlObjectRenderer<QuoteBlock>
    {
        protected override void Write(HtmlRenderer renderer, QuoteBlock obj)
        {
            if (!renderer.IsFirstInContainer)
            {
                renderer.Write("<br><br>");
            }

            var savedImplicitParagraph = renderer.ImplicitParagraph;
            renderer.ImplicitParagraph = false;
            renderer.WriteChildren(obj);
            renderer.ImplicitParagraph = savedImplicitParagraph;
        }
    }

    /// <summary>
    /// Raw HTML blocks are, by CommonMark design, passed through by the default renderer without checking any
    /// "enable HTML" flag. Since TextParagraph doesn't support arbitrary tags, and passing them through unescaped
    /// would let markdown input inject arbitrary HTML, render the block's raw content as escaped text instead.
    /// </summary>
    private sealed class RestrictedHtmlBlockRenderer : HtmlObjectRenderer<HtmlBlock>
    {
        protected override void Write(HtmlRenderer renderer, HtmlBlock obj)
            => renderer.WriteLeafRawLines(obj, writeEndOfLines: false, escape: true);
    }

    private sealed class RestrictedEmphasisInlineRenderer : HtmlObjectRenderer<EmphasisInline>
    {
        protected override void Write(HtmlRenderer renderer, EmphasisInline obj)
        {
            var tag = obj.DelimiterChar switch
            {
                '*' or '_' => obj.DelimiterCount >= 2 ? "b" : "i",
                '~' => "s",
                _ => null,
            };

            if (tag is null)
            {
                renderer.WriteChildren(obj);
                return;
            }

            renderer.Write('<');
            renderer.Write(tag);
            renderer.Write('>');
            renderer.WriteChildren(obj);
            renderer.Write("</");
            renderer.Write(tag);
            renderer.Write('>');
        }
    }

    private sealed class RestrictedLinkInlineRenderer : HtmlObjectRenderer<LinkInline>
    {
        protected override void Write(HtmlRenderer renderer, LinkInline obj)
        {
            if (obj.IsImage)
            {
                // TextParagraph doesn't support images; fall back to just the alt text.
                renderer.WriteChildren(obj);
                return;
            }

            renderer.Write("<a href=\"");
            renderer.WriteEscapeUrl(obj.GetDynamicUrl?.Invoke() ?? obj.Url);
            renderer.Write("\">");
            renderer.WriteChildren(obj);
            renderer.Write("</a>");
        }
    }

    private sealed class RestrictedLineBreakInlineRenderer : HtmlObjectRenderer<LineBreakInline>
    {
        protected override void Write(HtmlRenderer renderer, LineBreakInline obj)
        {
            if (renderer.IsLastInContainer)
            {
                return;
            }

            renderer.Write(obj.IsHard ? "<br>" : " ");
        }
    }

    private sealed class RestrictedCodeInlineRenderer : HtmlObjectRenderer<CodeInline>
    {
        protected override void Write(HtmlRenderer renderer, CodeInline obj)
            => renderer.WriteEscape(obj.ContentSpan);
    }

    /// <summary>
    /// Literal inline HTML (e.g. a stray "&lt;script&gt;" typed mid-paragraph) is, like <see cref="RestrictedHtmlBlockRenderer"/>,
    /// rendered as escaped text rather than passed through.
    /// </summary>
    private sealed class RestrictedHtmlInlineRenderer : HtmlObjectRenderer<HtmlInline>
    {
        protected override void Write(HtmlRenderer renderer, HtmlInline obj)
            => renderer.WriteEscape(obj.Tag);
    }
}
