using Markdig.Syntax;
using Morello.Markdown.Console.Formatters;
using Morello.Markdown.Console.SyntaxHighlighters;
using Spectre.Console;
using MarkdownTable = Markdig.Extensions.Tables;

namespace Morello.Markdown.Console;

/// <summary>
/// Renders Markdown in the terminal, using Ansi Escape Codes to apply formatting.
/// </summary>
public partial class AnsiRenderer
{
    private static readonly SyntaxHighlighter _syntaxHighlighter = new();
    private static readonly NumberFormatter _numberFormatter = new();

    public void Write(MarkdownDocument document, IAnsiConsole console)
    {
        WriteBlocks(document, console);
    }

    private void WriteBlocks(IEnumerable<Block> blocks, IAnsiConsole console)
    {
        foreach (var block in blocks)
        {
            switch (block)
            {
                case HeadingBlock headingBlock:
                    WriteHeadingBlock(console, headingBlock);
                    break;

                case ParagraphBlock paragraphBlock:
                    WriteParagraphBlock(console, paragraphBlock);
                    break;

                case QuoteBlock quoteBlock:
                    WriteQuoteBlock(console, quoteBlock);
                    break;

                case ListBlock listBlock:
                    WriteListBlock(console, listBlock);
                    break;

                case MarkdownTable.Table tableBlock:
                    WriteTableBlock(console, tableBlock);
                    break;

                case FencedCodeBlock fencedCodeBlock:
                    WriteFencedCodeBlock(console, fencedCodeBlock);
                    break;

                case LinkReferenceDefinitionGroup linkBlock:
                    WriteLinkReferenceDefinitionBlock(console, linkBlock);
                    break;

                case ThematicBreakBlock thematicBreakBlock:
                    WriteThematicBreakBlock(console, thematicBreakBlock);
                    break;

                default:
                    // We shouldn't be able to get here.
                    // The case above should handle all possibilities.
                    // Fallback to plain text.

                    // TODO: Inform caller we fellback.
                    foreach (var descendant in block.Descendants())
                    {
                        console.Write(descendant?.ToString() ?? string.Empty);
                    }
                    break;
            };

            console.WriteLine();
        }
    }

    private static int GetConsoleWidth(IAnsiConsole console)
    {
        return console.Profile.Width;
    }

    internal class CannotConvertMarkdownException : Exception
    {
        internal CannotConvertMarkdownException(string message)
            : base(message)
        {
            // no-op
        }
    }
}
