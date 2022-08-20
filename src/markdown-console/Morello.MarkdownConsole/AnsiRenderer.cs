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


                    // if (block is LeafBlock leafBlock)
                    // {
                    //     var lines = string.Join("\n", leafBlock.Lines.Lines);
                    // }

                    // if (block is ContainerBlock containerBlock)
                    // {
                    //     containerBlock.
                    // }


                    // TODO: Replace message below either:
                    // 1) Plain text rendering (fallback option)
                    // 2) An exception (failure expected only during development option)
                    console.MarkupLine($"[yellow]Block type not supported: {block.GetType()}[/]");
                    break;
                    // throw new NotSupportedException($"Markdown document type not supported: {element.GetType}")
            };

            console.WriteLine();
        }
    }

    private static int GetConsoleWidth()
    {
        try
        {
            // This line will throw is there is no console attached.
            // This can happen when the code is executed by a test runner, like Rider and VS Code's
            // integrated test windows.
            return System.Console.BufferWidth;
        }
        catch
        {
            // There is no console.
            // Return some fixed value.
            // 80 picked for no other reason than it was the Windows default for a very long time.
            return 80;
        }
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
