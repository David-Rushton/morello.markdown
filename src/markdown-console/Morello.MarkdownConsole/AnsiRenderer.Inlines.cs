using Markdig.Extensions.TaskLists;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Spectre.Console;

namespace Morello.Markdown.Console;

public partial class AnsiRenderer
{

    private bool _isQuote = false;
    private string _quoteLinePrefix = "[purple] ❯ [/]";

    private void WriteInlines(IAnsiConsole console, IEnumerable<Inline> inlines, string? markupTag = null)
    {
        foreach (var inline in inlines)
        {
            switch (inline)
            {
                case LiteralInline literal:
                    WriteLiteralInline(console, literal.ToString(), markupTag);
                    break;

                case EmphasisInline emphasis:
                    WriteEmphasisInline(console, emphasis, markupTag);
                    break;

                case CodeInline code:
                    console.Markup($"[purple][invert]{ code.Content.EscapeMarkup() }[/][/]");
                    break;

                case LinkInline link:
                    WriteInlineLink(console, link);
                    break;

                case LineBreakInline:
                    if (_isQuote)
                    {
                        console.Markup($"\n{_quoteLinePrefix}");
                        break;
                    }
                    console.WriteLine();
                    break;

                case TaskList task:
                    console.Markup(task.Checked ? "[purple] [/]" : "[purple] [/]");
                    break;

                default:
                    // TODO: Inform caller we fellback.
                    console.Write(inline?.ToString() ?? string.Empty);
                    break;
            }
        }
    }

    private void WriteLiteralInline(IAnsiConsole console, string content, string? markupTag = null)
    {
        var result = content.EscapeMarkup();

        if (markupTag is not null)
        {
            result = $"[{markupTag.Trim()}]{result}[/]";
        }

        console.Markup(result);
    }

    private void WriteEmphasisInline(IAnsiConsole console, EmphasisInline emphasis, string? markupTag = null)
    {
        switch (emphasis.DelimiterChar)
        {
            case '*':
                markupTag += "bold ";
                WriteInlines(console, emphasis, markupTag);
                break;

            case '_':
                markupTag += "italic ";
                WriteInlines(console, emphasis, markupTag);
                break;

            case '~':
                markupTag += "strikethrough ";
                WriteInlines(console, emphasis, markupTag);
                break;

            default:
                // TODO: Inform caller we fellback.
                console.Write(emphasis?.ToString() ?? string.Empty);
                break;
        }
    }
}
