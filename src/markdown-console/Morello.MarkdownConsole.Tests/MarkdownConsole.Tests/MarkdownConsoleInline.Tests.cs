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
    [InlineData("`code`",                            UseNerdFonts.Yes, $"{AnsiCodeStartEscapeNF}code{AnsiCodeEndEscapeNF}{AnsiResetEscape}\n\n")]
    [InlineData("some inline `code` and plain text", UseNerdFonts.Yes, $"some inline {AnsiCodeStartEscapeNF}code{AnsiCodeEndEscapeNF}{AnsiResetEscape} and plain text\n\n")]
    [InlineData("`code` and plain text",             UseNerdFonts.Yes, $"{AnsiCodeStartEscapeNF}code{AnsiCodeEndEscapeNF}{AnsiResetEscape} and plain text\n\n")]
    [InlineData("`code`",                            UseNerdFonts.No,  $"{AnsiCodeStartEscapeASCII}code{AnsiCodeEndEscapeASCII}{AnsiResetEscape}\n\n")]
    [InlineData("some inline `code` and plain text", UseNerdFonts.No,  $"some inline {AnsiCodeStartEscapeASCII}code{AnsiCodeEndEscapeASCII}{AnsiResetEscape} and plain text\n\n")]
    [InlineData("`code` and plain text",             UseNerdFonts.No,  $"{AnsiCodeStartEscapeASCII}code{AnsiCodeEndEscapeASCII}{AnsiResetEscape} and plain text\n\n")]
    public void Given_markdown_with_inline_code_should_return_correct_ansi_escaped_string(
        string markdown,
        UseNerdFonts useNerdFonts,
        string expected
    )
    {
        var actual = new TestConsole()
            .SetNerdFonts(useNerdFonts)
            .Write(markdown)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("_italic with **bold** text_",        UseNerdFonts.Yes, $"{AnsiItalicEscape}italic with {AnsiResetEscape}{AnsiBoldItalicEscape}bold{AnsiResetEscape}{AnsiItalicEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("**bold with _italic_ text**",        UseNerdFonts.Yes, $"{AnsiBoldEscape}bold with {AnsiResetEscape}{AnsiBoldItalicEscape}italic{AnsiResetEscape}{AnsiBoldEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("~strikethrough with **bold** text~", UseNerdFonts.Yes, $"{AnsiStrikethroughEscape}strikethrough with {AnsiResetEscape}{AnsiBoldStrikethroughEscape}bold{AnsiResetEscape}{AnsiStrikethroughEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("`code ignores embedded **styles**`", UseNerdFonts.Yes, $"{AnsiCodeStartEscapeNF}code ignores embedded **styles**{AnsiCodeEndEscapeNF}{AnsiResetEscape}\n\n")]
    [InlineData("_italic with **bold** text_",        UseNerdFonts.No, $"{AnsiItalicEscape}italic with {AnsiResetEscape}{AnsiBoldItalicEscape}bold{AnsiResetEscape}{AnsiItalicEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("**bold with _italic_ text**",        UseNerdFonts.No, $"{AnsiBoldEscape}bold with {AnsiResetEscape}{AnsiBoldItalicEscape}italic{AnsiResetEscape}{AnsiBoldEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("~strikethrough with **bold** text~", UseNerdFonts.No, $"{AnsiStrikethroughEscape}strikethrough with {AnsiResetEscape}{AnsiBoldStrikethroughEscape}bold{AnsiResetEscape}{AnsiStrikethroughEscape} text{AnsiResetEscape}\n\n")]
    [InlineData("`code ignores embedded **styles**`", UseNerdFonts.No, $"{AnsiCodeStartEscapeASCII}code ignores embedded **styles**{AnsiCodeEndEscapeASCII}{AnsiResetEscape}\n\n")]
    public void Given_markdown_with_embedded_inlines_should_render_mix_of_styles(
        string markdown,
        UseNerdFonts useNerdFonts,
        string expected
    )
    {
        var actual = new TestConsole()
            .SetNerdFonts(useNerdFonts)
            .Write(markdown)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
