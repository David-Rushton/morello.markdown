using Markdig.Syntax;
using Morello.Markdown.Console.Formatters;
using Morello.Markdown.Console.Parsers;
using Morello.Markdown.Console.SyntaxHighlighters;
using Spectre.Console;
using MarkdownTable = Markdig.Extensions.Tables;

namespace Morello.Markdown.Console.Renderers;

/// <summary>
/// Renders Markdown in the terminal, using Ansi Escape Codes to apply formatting.
/// </summary>
public partial class AnsiRenderer
{
    private readonly MarkdownParser _markdownParser;
    private readonly SyntaxHighlighter _syntaxHighlighter;
    private readonly NumberFormatter _numberFormatter;
    private readonly IAnsiConsole _console;

    internal AnsiRenderer(
        MarkdownParser markdownParser,
        SyntaxHighlighter syntaxHighlighter,
        NumberFormatter numberFormatter,
        IAnsiConsole console)
    {
        _markdownParser = markdownParser;
        _syntaxHighlighter = syntaxHighlighter;
        _numberFormatter = numberFormatter;
        _console = console;
    }

    public void Write(string markdown)
    {
        var doc = _markdownParser.ConvertToMarkdownDocument(markdown);
        WriteBlocks(doc);
    }

    private void WriteBlocks(IEnumerable<Block> blocks)
    {
        foreach (var block in blocks)
        {
            switch (block)
            {
                case HeadingBlock headingBlock:
                    WriteHeadingBlock(headingBlock);
                    break;

                case ParagraphBlock paragraphBlock:
                    WriteParagraphBlock(paragraphBlock);
                    break;

                case QuoteBlock quoteBlock:
                    WriteQuoteBlock(quoteBlock);
                    break;

                case ListBlock listBlock:
                    WriteListBlock(listBlock);
                    break;

                case MarkdownTable.Table tableBlock:
                    WriteTableBlock(tableBlock);
                    break;

                case FencedCodeBlock fencedCodeBlock:
                    WriteFencedCodeBlock(fencedCodeBlock);
                    break;

                case LinkReferenceDefinitionGroup linkBlock:
                    WriteLinkReferenceDefinitionBlock(linkBlock);
                    break;

                case ThematicBreakBlock thematicBreakBlock:
                    WriteThematicBreakBlock(thematicBreakBlock);
                    break;

                default:
                    // We shouldn't be able to get here.
                    // The case above should handle all possibilities.
                    // Fallback to plain text.

                    // TODO: Inform caller we fellback.
                    foreach (var descendant in block.Descendants())
                    {
                        _console.Write(descendant?.ToString() ?? string.Empty);
                    }
                    break;
            };

            _console.WriteLine();
        }
    }

    private int GetConsoleWidth()
    {
        return _console.Profile.Width;
    }
}
