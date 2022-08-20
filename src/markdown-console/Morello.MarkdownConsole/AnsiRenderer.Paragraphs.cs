using Markdig.Extensions.AutoIdentifiers;
using Markdig.Extensions.TaskLists;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Morello.Markdown.Console.Formatters;
using Morello.Markdown.Console.SyntaxHighlighters;
using Spectre.Console;
using MarkdownTable = Markdig.Extensions.Tables;

namespace Morello.Markdown.Console;

public partial class AnsiRenderer
{
    private void WriteParagraphBlock(IAnsiConsole console, ParagraphBlock block, bool suppressNewLine = false, string? markupTag = null)
    {
        if (block.Inline is not null)
        {
            WriteInlines(console, block.Inline, markupTag);

            if (!suppressNewLine)
            {
                console.Write("\n");
            }
        }
        else
        {
            // TODO: Is this possible?
            // TODO: Inform caller we fellback.
        }
    }
}
