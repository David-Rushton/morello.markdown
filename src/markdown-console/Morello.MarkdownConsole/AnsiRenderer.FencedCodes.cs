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
    private void WriteFencedCodeBlock(IAnsiConsole console, FencedCodeBlock block)
    {
        var code = string.Join("\n", block.Lines.Lines).TrimEnd();
        var lang = block.Info;
        var highlightedCode = _syntaxHighlighter.GetHighlightedSyntax(code, lang);

        // The syntax highlighter will use Ansi escape codes to add colour to the output, if it can.
        // Although the escape codes are not printed directly Ansi console will count them towards
        // the line limit.  This will lead to unexpected line breaks.  To work around this we override
        // the buffer width while writing syntax.
        console.Profile.Width = int.MaxValue;

        console.WriteLine(highlightedCode);

        console.Profile.Width = GetConsoleWidth();
    }
}
