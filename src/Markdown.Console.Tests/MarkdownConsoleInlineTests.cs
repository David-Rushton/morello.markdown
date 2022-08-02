using Markdown.Console.Tests.Extensions;
using Xunit;

namespace Markdown.Console.Tests;

public partial class MarkdownConsoleTests
{
    // https://en.wikipedia.org/wiki/ANSI_escape_code
    private const string AnsiEscape = "\u001b";
    private const string AnsiResetEscape = "\u001b[0m";
    private const string AnsiBoldEscape = "\u001b[1m";
    private const string AnsiItalicEscape = "\u001b[3m";
    private const string AnsiStrikethroughEscape = "\u001b[9m";
    private const string AnsiCodeStartEscape = "\u001b[38;5;5m\u001b[0m\u001b[7;38;5;5m";
    private const string AnsiCodeEndEscape = "\u001b[0m\u001b[38;5;5m";


    [Theory]
    [InlineData("**bold**",             $"{AnsiBoldEscape}bold{AnsiResetEscape}\n\n")]
    [InlineData("*bold*",               $"{AnsiBoldEscape}bold{AnsiResetEscape}\n\n")]
    [InlineData("some **bold** text",   $"some {AnsiBoldEscape}bold{AnsiResetEscape} text\n\n")]
    [InlineData("some *bold* text",     $"some {AnsiBoldEscape}bold{AnsiResetEscape} text\n\n")]
    [InlineData("**bold** text",        $"{AnsiBoldEscape}bold{AnsiResetEscape} text\n\n")]
    [InlineData("*bold* text",          $"{AnsiBoldEscape}bold{AnsiResetEscape} text\n\n")]
    public void Given_markdown_with_inline_bold_should_return_correct_ansi_escaped_string(
        string markdown,
        string expected
    )
    {
        var actual = new TestConsole()
            .Write(markdown)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("_italic_",             $"{AnsiItalicEscape}italic{AnsiResetEscape}\n\n")]
    [InlineData("some _italic_ text",   $"some {AnsiItalicEscape}italic{AnsiResetEscape} text\n\n")]
    [InlineData("_italic_ text",        $"{AnsiItalicEscape}italic{AnsiResetEscape} text\n\n")]
    public void Given_markdown_with_inline_italic_should_return_correct_ansi_escaped_string(
        string markdown,
        string expected
    )
    {
        var actual = new TestConsole()
            .Write(markdown)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("~strikethrough~",             $"{AnsiStrikethroughEscape}strikethrough{AnsiResetEscape}\n\n")]
    [InlineData("some ~strikethrough~ text",   $"some {AnsiStrikethroughEscape}strikethrough{AnsiResetEscape} text\n\n")]
    [InlineData("~strikethrough~ text",        $"{AnsiStrikethroughEscape}strikethrough{AnsiResetEscape} text\n\n")]
    public void Given_markdown_with_inline_strikethrough_should_return_correct_ansi_escaped_string(
        string markdown,
        string expected
    )
    {
        var actual = new TestConsole()
            .Write(markdown)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("`code`",                               $"{AnsiCodeStartEscape}code{AnsiCodeEndEscape}{AnsiResetEscape}\n\n")]
    [InlineData("some inline `code` and plain text",    $"some inline {AnsiCodeStartEscape}code{AnsiCodeEndEscape}{AnsiResetEscape} and plain text\n\n")]
    [InlineData("`code` and plain text",                $"{AnsiCodeStartEscape}code{AnsiCodeEndEscape}{AnsiResetEscape} and plain text\n\n")]
    public void Given_markdown_with_inline_code_should_return_correct_ansi_escaped_string(
        string markdown,
        string expected
    )
    {
        var actual = new TestConsole()
            .Write(markdown)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Theory(Skip = "Regex to match link incomplete")]
    [InlineData("[example](http://example.com) with link",    @"]8;id=[0-9]*;http://example.com[38;5;5mexample\[0m]8;;\ with link")]
    public void Given_markdown_with_inline_link_should_return_correct_ansi_escaped_string(
        string markdown,
        string expected
    )
    {
        var actual = new TestConsole()
            .Write(markdown)
            .Output;

        // Assert.Equal(expected, actual);
        Assert.Matches(expected, actual);
    }

    [Theory]
    [InlineData("## Header", $"{AnsiEscape}[1;38;5;5mHeader{AnsiResetEscape}\n\n\n\n")]
    public void Given_markdown_with_block_header_2_should_return_correct_ansi_escaped_string(
        string markdown,
        string expected
    )
    {
        var actual = new TestConsole()
            .Write(markdown)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
