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
    private void WriteParagraphBlock(ParagraphBlock block, IAnsiConsole? console = null, bool suppressNewLine = false, string? markupTag = null)
    {
        console = console ?? _console;

        if (block.Inline is not null)
        {
            WriteInlines(console, block.Inline, markupTag);

            if (!suppressNewLine)
            {
                console.Write("\n");
            }
        }
    }
}
