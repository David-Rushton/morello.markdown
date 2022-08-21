using Markdig.Syntax;
using Spectre.Console;

namespace Morello.Markdown.Console.Renderers;

public partial class AnsiRenderer
{
    private void WriteFencedCodeBlock(FencedCodeBlock block)
    {
        var code = string.Join("\n", block.Lines.Lines).TrimEnd();
        var lang = block.Info;
        var highlightedCode = _syntaxHighlighter.GetHighlightedSyntax(code, lang);

        // The syntax highlighter will use Ansi escape codes to add colour to the output, if it can.
        // Although the escape codes are not printed directly Ansi console will count them towards
        // the line limit.  This will lead to unexpected line breaks.  To work around this we override
        // the buffer width while writing syntax.
        _console.Profile.Width = int.MaxValue;

        _console.WriteLine(highlightedCode);

        _console.Profile.Width = GetConsoleWidth();
    }
}
