namespace Morello.Markdown.Console.Tests;

public class TestConsole
{
    private readonly StringWriter _writer = new();

    public string Output => _writer.ToString();

    public TestConsole Write(string markdown)
    {
        MarkdownConsole.Write(markdown, _writer);

        return this;
    }
}
