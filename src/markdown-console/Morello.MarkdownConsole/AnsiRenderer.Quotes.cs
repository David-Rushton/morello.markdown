using Markdig.Syntax;
using Spectre.Console;

namespace Morello.Markdown.Console;

public partial class AnsiRenderer
{
    private void WriteQuoteBlock(QuoteBlock block)
    {
        foreach (var subBlock in block)
        {
            if (subBlock is ParagraphBlock paragraph)
            {
                _isQuote = true;
                _console.Markup(_quoteLinePrefix);
                WriteParagraphBlock(paragraph);
                _isQuote = false;
                return;
            }

            // TODO: Inform caller we fellback.
            _console.Write(subBlock?.ToString() ?? string.Empty);
        }
    }
}
