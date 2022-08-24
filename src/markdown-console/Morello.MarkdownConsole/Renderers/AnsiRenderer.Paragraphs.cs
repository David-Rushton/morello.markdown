using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Morello.Markdown.Console.Extensions;
using Spectre.Console;

namespace Morello.Markdown.Console.Renderers;

public partial class AnsiRenderer
{
    internal void WriteParagraphBlock(ParagraphBlock block, bool suppressNewLine = false, string? markupTag = null)
    {
        if (block.Inline is not null)
        {
            WriteInlines(block.Inline, markupTag);

            if (!suppressNewLine)
            {
                _console.Write("\n");
            }

            return;
        }

        // We shouldn't be able to get here.
        ThrowOrFallbackToPlainText(block);
    }
}
