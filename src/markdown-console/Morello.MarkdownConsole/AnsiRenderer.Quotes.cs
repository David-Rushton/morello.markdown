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
    private void WriteQuoteBlock(IAnsiConsole console, QuoteBlock block)
    {
        foreach (var subBlock in block)
        {
            if (subBlock is ParagraphBlock paragraph)
            {
                _isQuote = true;
                console.Markup(_quoteLinePrefix);
                WriteParagraphBlock(console, paragraph);
                _isQuote = false;
                return;
            }

            // TODO: Inform caller we fellback.
            console.Write(subBlock?.ToString() ?? string.Empty);
        }
    }
}
