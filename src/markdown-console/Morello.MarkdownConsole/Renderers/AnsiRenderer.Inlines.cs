using Markdig.Extensions.TaskLists;
using Markdig.Syntax.Inlines;
using Spectre.Console;

namespace Morello.Markdown.Console.Renderers;

public partial class AnsiRenderer
{

    private bool _isQuote = false;
    private string _quoteLinePrefix = "[purple] ❯ [/]";

    private void WriteInlines(IEnumerable<Inline> inlines, string? markupTag = null)
    {
        foreach (var inline in inlines)
        {
            switch (inline)
            {
                case LiteralInline literal:
                    WriteLiteralInline(literal.ToString(), markupTag);
                    break;

                case EmphasisInline emphasis:
                    WriteEmphasisInline(emphasis, markupTag);
                    break;

                case CodeInline code:
                    _console.Markup($"[purple][invert]{ code.Content.EscapeMarkup() }[/][/]");
                    break;

                case LinkInline link:
                    WriteInlineLink(link);
                    break;

                case LineBreakInline:
                    if (_isQuote)
                    {
                        _console.Markup($"\n{_quoteLinePrefix}");
                        break;
                    }
                    _console.WriteLine();
                    break;

                case TaskList task:
                    _console.Markup(task.Checked ? "[purple] [/]" : "[purple] [/]");
                    break;

                default:
                    // TODO: Inform caller we fellback.
                    _console.Write(inline?.ToString() ?? string.Empty);
                    break;
            }
        }
    }

    private void WriteLiteralInline(string content, string? markupTag = null)
    {
        var result = content.EscapeMarkup();

        if (markupTag is not null)
        {
            result = $"[{markupTag.Trim()}]{result}[/]";
        }

        _console.Markup(result);
    }

    private void WriteEmphasisInline(EmphasisInline emphasis, string? markupTag = null)
    {
        switch (emphasis.DelimiterChar)
        {
            case '*':
                markupTag += "bold ";
                WriteInlines(emphasis, markupTag);
                break;

            case '_':
                markupTag += "italic ";
                WriteInlines(emphasis, markupTag);
                break;

            case '~':
                markupTag += "strikethrough ";
                WriteInlines(emphasis, markupTag);
                break;

            default:
                // TODO: Inform caller we fellback.
                _console.Write(emphasis?.ToString() ?? string.Empty);
                break;
        }
    }
}
