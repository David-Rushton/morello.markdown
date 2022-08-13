using System.Runtime.InteropServices;
using Morello.Markdown.Console.Formatters;
using Morello.Markdown.Console.SyntaxHighlighters;
using Markdig.Extensions.AutoIdentifiers;
using Markdig.Extensions.TaskLists;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using MarkdownTable = Markdig.Extensions.Tables;
using Spectre.Console;

namespace Morello.Markdown.Console;

public class AnsiRenderer
{
    private readonly SyntaxHighlighter _syntaxHighlighter;
    private readonly NumberFormatter _numberFormatter;

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

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
        foreach (var element in blocks)
        {
            switch (element)
            {
                case HeadingBlock headingBlock:
                    ConvertHeadingBlock(headingBlock, buffer);
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
                    ConvertTableBlock(tableBlock, buffer);
                    break;

                case FencedCodeBlock fencedCodeBlock:
                    ConvertFencedCodeBlock(fencedCodeBlock, buffer);
                    break;

                case LinkReferenceDefinitionGroup linkBlock:
                    ConvertLinkReferenceDefinitionBlock(linkBlock, buffer);
                    break;

                default:
                    // TODO: Replace message below either:
                    // 1) Plain text rendering (fallback option)
                    // 2) An exception (failure expected only during development option)
                    buffer.MarkupLine($"[yellow]Block type not supported: {element.GetType()}[/]");
                    break;
                    // throw new NotSupportedException($"Markdown document type not supported: {element.GetType}")
            };

            buffer.WriteLine();
        }
    }

    private void WriteInlines(IEnumerable<Inline> inlines, IAnsiConsole buffer, string? markupTag = null)
    {
        foreach (var inline in inlines)
        {
            switch (inline)
            {
                case LiteralInline literal:

                    var content = literal.ToString().EscapeMarkup();
                    if (markupTag is not null)
                    {
                        content = $"[{markupTag}]{ content }[/]";
                    }
                    buffer.Markup(content);
                    break;

                case EmphasisInline emphasis:
                    switch (emphasis.DelimiterChar)
                    {
                        case '*':
                            WriteInlines(emphasis, buffer, "bold");
                            break;

                        case '_':
                            WriteInlines(emphasis, buffer, "italic");
                            break;

                        case '~':
                            WriteInlines(emphasis, buffer, "strikethrough");
                            break;

                        default:
                            // TODO: Consider if exception or plain text.
                            buffer.MarkupLine($"[yellow]Emphasis type not supported: { emphasis.DelimiterChar }[/]");
                            break;
                    }
                    break;

                case CodeInline code:
                    buffer.Markup($"[purple][invert]{ code.Content.EscapeMarkup() }[/][/]");
                    break;

                case LinkInline link:
                    ConvertLinkInline(link, buffer);
                    break;

                case LineBreakInline:
                    // Can only occur within a QuoteBlock.
                    buffer.Markup("\n[purple]❯ [/]");
                    break;

                case TaskList task:
                    buffer.Markup(task.Checked ? "[purple] [/]" : "[purple] [/]");
                    break;

                default:
                    // TODO: Plain text or exception.
                    buffer.MarkupLine($"[yellow]Could not process type: {inline.GetType()}[/]");
                    break;
            }
        }
    }

    private void ConvertLinkInline(LinkInline link, IAnsiConsole buffer)
    {
        var label = link.FirstChild?.ToString() ?? string.Empty;

        if (link.Url is null)
        {
            buffer.Markup(link.IsImage ? $"[purple italic]{ label } [/]" : label );
            return;
        }

        if (link.IsImage)
        {
            var image = new CanvasImage(link.Url);
            buffer.Write(image);
            return;
        }

        buffer.Markup($"[purple link={ link.Url }]{ label }[/]");
    }

    private void ConvertHeadingBlock(HeadingBlock block, IAnsiConsole buffer)
    {
        var rawContent = block.Inline?.FirstChild?.ToString() ?? throw new CannotConvertMarkdownException("Cannot read content of HeadingBlock");

        if (block.Level == 1)
        {
            buffer.Write(new FigletText(rawContent).Alignment(Justify.Left).Color(Color.Purple));
            return;
        }

        buffer.MarkupLine($"[bold purple]{rawContent.EscapeMarkup()}[/]");
    }

    private void ConvertQuoteBlock(QuoteBlock block, IAnsiConsole buffer)
    {
        foreach (var subBlock in block)
        {
            if (subBlock is ParagraphBlock paragraph)
            {
                buffer.Markup("[purple] ❯ [/]");
                ConvertParagraphBlock(paragraph, buffer);
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
            WriteInlines(block.Inline, buffer, markupTag);

            if (!suppressNewLine)
            {
                buffer.Write("\n");
            }
        }
    }

    private void ConvertTableBlock(MarkdownTable.Table block, IAnsiConsole buffer)
    {
        var table = new Table().Border(TableBorder.Rounded);

        foreach (var item in block)
        {
            if (item is MarkdownTable.TableRow row)
            {
                var rows = new List<Markup>();

                foreach (var cellItem in row)
                {

                    if (cellItem is MarkdownTable.TableCell cell)
                    {
                        foreach (var paragraphItem in cell)
                        {
                            if (paragraphItem is ParagraphBlock paragraph)
                            {
                                var cellWriter = new StringWriter();
                                var settings = new AnsiConsoleSettings
                                {
                                    ColorSystem = ColorSystemSupport.Detect,
                                    Ansi = AnsiSupport.Detect,
                                    Interactive = InteractionSupport.No,
                                    Out = new AnsiConsoleOutput(cellWriter)
                                };
                                var cellBuffer = AnsiConsole.Create(settings);

                                ConvertParagraphBlock(paragraph, cellBuffer, suppressNewLine: true);

                                if (row.IsHeader)
                                {
                                    table.AddColumn($"[purple]{ cellWriter.ToString() }[/]");
                                }
                                else
                                {
                                    rows.Add(new Markup(cellWriter.ToString()));
                                }
                            }
                        }
                    }

                }

                if (rows.Any())
                {
                    // buffer.Write($"Rows to add: {rows.Count()}");
                    table.AddRow(rows);
                }
            }
        }

        buffer.Write(table);
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

        if (GetConsoleWindow() != IntPtr.Zero)
        {
            // Throws if no console is attached.
            // This can happen if the code is executed by some unit test runners.
            // Rider for example.
            buffer.Profile.Width = System.Console.BufferWidth;
        }
    }

    private void ConvertLinkReferenceDefinitionBlock(LinkReferenceDefinitionGroup linkBlock, IAnsiConsole buffer)
    {
        foreach (var item in linkBlock)
        {
            buffer.WriteLine(string.Join("\n", ((HeadingLinkReferenceDefinition)item)?.Lines));
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
