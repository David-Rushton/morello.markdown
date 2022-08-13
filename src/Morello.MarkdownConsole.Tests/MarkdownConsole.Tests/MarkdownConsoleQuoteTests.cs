using Morello.Markdown.Console;
using Morello.Markdown.Console.Tests.Extensions;
using Xunit;


namespace Morello.Markdown.Console.Tests;

public partial class MarkdownConsoleTests
{
    [Fact]
    public void Given_markdown_with_block_quote_should_return_correct_ansi_escaped_string()
    {
        var input = "> Some quote";
        var expected = $"{AnsiEscape}[38;5;5m ❯ {AnsiResetEscape}Some quote\n\n";
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_block_quote_containing_bold_inline_should_return_correct_ansi_escaped_string()
    {
        var input = "> Some *quote*";
        var expected = $"{AnsiEscape}[38;5;5m ❯ {AnsiResetEscape}Some {AnsiBoldEscape}quote{AnsiResetEscape}\n\n";
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_block_quote_containing_italic_inline_should_return_correct_ansi_escaped_string()
    {
        var input = "> Some _quote_";
        var expected = $"{AnsiEscape}[38;5;5m ❯ {AnsiResetEscape}Some {AnsiItalicEscape}quote{AnsiResetEscape}\n\n";
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_block_quote_containing_code_inline_should_return_correct_ansi_escaped_string()
    {
        var input = "> Some `quote`";
        var expected = $"{AnsiEscape}[38;5;5m ❯ {AnsiResetEscape}Some {AnsiCodeStartEscape}quote{AnsiCodeEndEscape}{AnsiResetEscape}\n\n";
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_block_quote_containing_new_line_should_return_correct_ansi_escaped_string()
    {
        var input = @"> Some quote over
> two lines";

        var expected = @$"{AnsiEscape}[38;5;5m ❯ {AnsiResetEscape}Some quote over
{AnsiEscape}[38;5;5m❯ {AnsiResetEscape}two lines

";
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
