using Morello.Markdown.Console.Formatters;
using Morello.Markdown.Console.Parsers;
using Morello.Markdown.Console.SyntaxHighlighters;
using Spectre.Console;

namespace Morello.Markdown.Console.Renderers;

/// <summary>
/// Creates an AnsiRenderer.
/// </summary>
public class AnsiRendererBuilder
{
    private static readonly SyntaxHighlighter _syntaxHighlighter = new();
    private static readonly NumberFormatter _numberFormatter = new();
    private static readonly MarkdownParser _parser = new();

    private TextWriter? _writer;

    internal AnsiRendererBuilder()
    {
        // no-op.
    }

    /// <summary>
    /// Adds a customer writer.
    /// Allows the caller to divert the renderer output.
    /// Useful for testing and debugging.
    /// </summary>
    /// <param name="writer">Ansi formatted string will be built using the supplied writer</param>
    /// <returns></returns>
    public AnsiRendererBuilder RedirectOutput(TextWriter writer)
    {
        _writer = writer;
        return this;
    }

    public AnsiRenderer Build()
    {
        return new AnsiRenderer(_parser, _syntaxHighlighter, _numberFormatter, GetConsole());
    }

    private IAnsiConsole GetConsole()
    {
        if (_writer is null)
        {
            return AnsiConsole.Console;
        }

        var console = AnsiConsole.Create(new AnsiConsoleSettings
        {
            ColorSystem = ColorSystemSupport.Detect,
            Ansi = AnsiSupport.Detect,
            Interactive = InteractionSupport.No,
            Out = new AnsiConsoleOutput(_writer)
        });

        return console;
    }
}
