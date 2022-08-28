using System.Text;
using Markdig.Extensions.TaskLists;
using Markdig.Syntax.Inlines;
using Spectre.Console;

namespace Morello.Markdown.Console.Renderers;

public partial class AnsiRenderer
{

    private bool _isQuote = false;

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
                    WriteCodeInline(code);
                    break;

                case LinkInline link:
                    WriteInlineLink(link);
                    break;

                case AutolinkInline link:
                    WriteAutoInlineLink(link);
                    break;

                case LineBreakInline:
                    if (_isQuote)
                    {
                        var _quoteLinePrefix = $"[purple] {_characterSet.QuotePrefix} [/]";
                        _console.Markup($"\n{_quoteLinePrefix}");
                        break;
                    }
                    _console.WriteLine();
                    break;

                case TaskList task:
                    var bullet = task.Checked ? _characterSet.TaskListBulletDone : _characterSet.TaskListBulletToDo;
                    _console.Markup($"[purple]{bullet.EscapeMarkup()}[/]");
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
                // We shouldn't be able to get here.
                // All cases of emphasis should be handled above.
                var exceptionMessage = $"Unsupported emphasis delimited found: {emphasis.DelimiterChar}.";
                var fallbackText = emphasis?.ToString() ?? string.Empty;
                ThrowOrFallbackToPlainText(exceptionMessage, fallbackText);
                break;
        }
    }

    private void WriteCodeInline(CodeInline code)
    {
        var sb = new StringBuilder();

        sb.Append("[purple]");
        sb.Append(_characterSet.InlineCodeOpening);
        sb.Append("[invert]");
        sb.Append(code.Content.EscapeMarkup());
        sb.Append("[/]");
        sb.Append(_characterSet.InlineCodeClosing);
        sb.Append("[/]");

        _console.Markup(sb.ToString());
    }
}
