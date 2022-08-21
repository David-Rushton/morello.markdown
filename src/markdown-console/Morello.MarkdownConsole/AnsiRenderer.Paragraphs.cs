using Markdig.Syntax;
using Spectre.Console;

namespace Morello.Markdown.Console;

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
        }
        else
        {
            // TODO: Is this possible?
            // TODO: Inform caller we fellback.
        }
    }
}
