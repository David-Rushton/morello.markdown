using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public partial class MarkdownConsoleInlineTests : MarkdownConsoleTests
{
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
