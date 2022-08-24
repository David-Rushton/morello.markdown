namespace Morello.Markdown.Console.Tests;

public class TestConsole
{
    private readonly StringWriter _writer = new();

    public string Output => _writer.ToString();

    public TestConsole()
    {
        // By default MarkdownConsole avoids throwing.
        // It fallsback to plain text.
        // This can hide issues during testing.
        Environment.SetEnvironmentVariable("MORELLO_MARKDOWN_CONSOLE_THROW_ON_UNSUPPORTED_TYPE", "true");

        // By default we try the Bat highlighter first.
        // However this is not available in all test environments.
        // So we drop down to the basic highlighter.
        Environment.SetEnvironmentVariable("MORELLO_MARKDOWN_CONSOLE_FORCE_BASIC_SYNTAX_HIGHLIGHTER", "true");

        // Test runners redirect output from the Console.
        // Spectre console can disable colour output, if it thinks the output doesn't support Ansi Escape Codes.
        // Force colour output.
        Environment.SetEnvironmentVariable("MORELLO_MARKDOWN_CONSOLE_FORCE_ANSI_COLOUR", "true");
    }

    public TestConsole FallbackToPlainText()
    {
        Environment.SetEnvironmentVariable("MORELLO_MARKDOWN_CONSOLE_THROW_ON_UNSUPPORTED_TYPE", "false");
        return this;
    }

    public TestConsole Write(string markdown)
    {
        MarkdownConsole.Write(markdown, _writer);

        return this;
    }
}
