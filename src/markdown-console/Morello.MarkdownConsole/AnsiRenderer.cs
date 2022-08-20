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
    private static readonly SyntaxHighlighter _syntaxHighlighter = new();
    private static readonly NumberFormatter _numberFormatter = new();

    private readonly IAnsiConsole _console;

    public AnsiRenderer(IAnsiConsole console)
    {
        _console = console;
    }

    public void Write(MarkdownDocument document, IAnsiConsole buffer)
    {
        WriteBlocks(document, buffer);
    }

    private void WriteBlocks(IEnumerable<Block> blocks, IAnsiConsole buffer)
    {
        foreach (var block in blocks)
        {
            switch (block)
            {
                case HeadingBlock headingBlock:
                    WriteHeadingBlock(buffer, headingBlock);
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
                    WriteTableBlock(buffer, tableBlock);
                    break;

                case FencedCodeBlock fencedCodeBlock:
                    WriteFencedCodeBlock(fencedCodeBlock);
                    break;

                case LinkReferenceDefinitionGroup linkBlock:
                    WriteLinkReferenceDefinitionBlock(buffer, linkBlock);
                    break;

                case ThematicBreakBlock thematicBreakBlock:
                    WriteThematicBreakBlock(thematicBreakBlock);
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
                    buffer.MarkupLine($"[yellow]Block type not supported: {block.GetType()}[/]");
                    break;
                    // throw new NotSupportedException($"Markdown document type not supported: {element.GetType}")
            };

            buffer.WriteLine();
        }
    }

    private int GetConsoleWidth()
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
