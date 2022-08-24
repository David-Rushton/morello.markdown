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
                    __console.Markup($"[purple][invert]{ code.Content.EscapeMarkup() }[/][/]");
                    break;

                case LinkInline link:
                    WriteInlineLink(link);
                    break;

                case LineBreakInline:
                    if (_isQuote)
                    {
                        __console.Markup($"\n{_quoteLinePrefix}");
                        break;
                    }
                    __console.WriteLine();
                    break;

                case TaskList task:
                    __console.Markup(task.Checked ? "[purple] [/]" : "[purple] [/]");
                    break;

                default:
                    // We shouldn't be able to get here.
                    ThrowOrFallbackToPlainText(inline);
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

        __console.Markup(result);
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
                // We shouldn't be able to get here.
                // All cases of emphasis should be handled above.
                var exceptionMessage = $"Unsupported emphasis delimited found: {emphasis.DelimiterChar}.";
                var fallbackText = emphasis?.ToString() ?? string.Empty;
                ThrowOrFallbackToPlainText(exceptionMessage, fallbackText);
                break;
        }
    }
}
