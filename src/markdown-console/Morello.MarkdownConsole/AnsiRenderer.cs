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
    private readonly SyntaxHighlighter _syntaxHighlighter;
    private readonly NumberFormatter _numberFormatter;

    public AnsiRenderer()
    {
        _syntaxHighlighter = new SyntaxHighlighter();
        _numberFormatter = new NumberFormatter();
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
                    ConvertParagraphBlock(paragraphBlock, buffer);
                    break;

                case QuoteBlock quoteBlock:
                    ConvertQuoteBlock(quoteBlock, buffer);
                    break;

                case ListBlock listBlock:
                    ConvertListBlock(listBlock, buffer);
                    break;

                case MarkdownTable.Table tableBlock:
                    WriteTableBlock(buffer, tableBlock);
                    break;

                case FencedCodeBlock fencedCodeBlock:
                    ConvertFencedCodeBlock(fencedCodeBlock, buffer);
                    break;

                case LinkReferenceDefinitionGroup linkBlock:
                    WriteLinkReferenceDefinitionBlock(buffer, linkBlock);
                    break;

                case ThematicBreakBlock thematicBreakBlock:
                    ConvertThematicBreakBlock(thematicBreakBlock, buffer);
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

    private void ConvertQuoteBlock(QuoteBlock block, IAnsiConsole buffer)
    {
        foreach (var subBlock in block)
        {
            if (subBlock is ParagraphBlock paragraph)
            {
                _isQuote = true;
                buffer.Markup(_quoteLinePrefix);
                ConvertParagraphBlock(paragraph, buffer);
                _isQuote = false;
                return;
            }

            // TODO: Plain text or exception?
            throw new NotSupportedException($"Unexpected type within QuoteBlock: {subBlock.GetType()}");
        }
    }

    private void ConvertListBlock(ListBlock block, IAnsiConsole buffer)
    {
        var numberedListCounter = 1;

        foreach (var item in block)
        {
            var defaultBullet = "  [purple] [/] ";
            var bullet = (block.BulletType) switch
            {
                '-' =>  IsTaskList(item) ? "  " : defaultBullet,
                '1' => $"  [purple]{_numberFormatter.Format(numberedListCounter++)} [/]",
                _ => defaultBullet
            };
            buffer.Markup(bullet);
            foreach(var subItem in (ListItemBlock)item)
            {
                ConvertParagraphBlock((ParagraphBlock)subItem, buffer);
            }
        }

        bool IsTaskList(Block itemToCheck)
        {
            return itemToCheck.Descendants().OfType<TaskList>().Any();
        }
    }

    private void ConvertParagraphBlock(ParagraphBlock block, IAnsiConsole buffer, bool suppressNewLine = false, string? markupTag = null)
    {
        if (block.Inline is not null)
        {
            WriteInlines(buffer, block.Inline, markupTag);

            if (!suppressNewLine)
            {
                buffer.Write("\n");
            }
        }
    }

    private void ConvertFencedCodeBlock(FencedCodeBlock block, IAnsiConsole buffer)
    {
        var code = string.Join("\n", block.Lines.Lines).TrimEnd();
        var lang = block.Info;
        var highlightedCode = _syntaxHighlighter.GetHighlightedSyntax(code, lang);


        // The syntax highlighter will use Ansi escape codes to add colour to the output, if it can.
        // Although the escape codes are not printed directly Ansi console will count them towards
        // the line limit.  This will lead to unexpected line breaks.  To work around this we override
        // the buffer width while writing syntax.
        buffer.Profile.Width = int.MaxValue;

        // TODO: Support fallback highlighter, which returns markup
        buffer.WriteLine(highlightedCode);
        buffer.Profile.Width = GetConsoleWidth();
    }

    private void ConvertThematicBreakBlock(ThematicBreakBlock thematicBreakBlock, IAnsiConsole buffer)
    {
        const char lineCharacter = '═';
        var charactersRequired = GetConsoleWidth() - 2;
        var line = new string(lineCharacter, charactersRequired);

        buffer.MarkupLine($"[purple] {line}[/]");
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
