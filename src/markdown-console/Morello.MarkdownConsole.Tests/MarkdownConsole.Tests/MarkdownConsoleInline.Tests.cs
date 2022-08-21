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
    [InlineData("_italic with **bold** text_",          $"{AnsiItalicEscape}italic with {AnsiResetEscape}{AnsiBoldItalicEscape}bold{AnsiResetEscape}{AnsiItalicEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("**bold with _italic_ text**",          $"{AnsiBoldEscape}bold with {AnsiResetEscape}{AnsiBoldItalicEscape}italic{AnsiResetEscape}{AnsiBoldEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("~strikethrough with **bold** text~",   $"{AnsiStrikethroughEscape}strikethrough with {AnsiResetEscape}{AnsiBoldStrikethroughEscape}bold{AnsiResetEscape}{AnsiStrikethroughEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("`code ignores embedded **styles**`",   $"{AnsiCodeStartEscape}code ignores embedded **styles**{AnsiCodeEndEscape}{AnsiResetEscape}\n\n")]
    public void Given_markdown_with_embedded_inlines_should_render_mix_of_styles(
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
