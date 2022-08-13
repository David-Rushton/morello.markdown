using Spectre.Console;

namespace Morello.Markdown.Console.Tests;

public class TestConsole
{
    private readonly StringWriter _writer = new();

    public string Output => _writer.ToString();

    public TestConsole Write(string markdown)
    {
        var settings = new AnsiConsoleSettings
        {
            ColorSystem = ColorSystemSupport.TrueColor,
            Ansi = AnsiSupport.Yes,
            Interactive = InteractionSupport.No,
            Out = new AnsiConsoleOutput(_writer),
        };
        var buffer = AnsiConsole.Create(settings);

        MarkdownConsole.Write(markdown, buffer);

        return this;
    }
}
