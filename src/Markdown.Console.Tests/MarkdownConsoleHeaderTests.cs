using Markdown.Console.Tests.Extensions;
using Xunit;

namespace Markdown.Console.Tests;

public partial class MarkdownConsoleTests
{
    [Fact(Skip = "We need to set TestConole to a fixed width to make this test deterministic.")]
    public void Given_markdown_with_header_level_1_block_should_return_correct_ansi_escaped_string()
    {
        var expected =
            @"                 [38;5;5m  _   _                      _               [0m
                 [38;5;5m | | | |   ___    __ _    __| |   ___   _ __ [0m
                 [38;5;5m | |_| |  / _ \  / _` |  / _` |  / _ \ | '__|[0m
                 [38;5;5m |  _  | |  __/ | (_| | | (_| | |  __/ | |   [0m
                 [38;5;5m |_| |_|  \___|  \__,_|  \__,_|  \___| |_|   [0m
                 [38;5;5m                                             [0m
";
        var actual = new TestConsole()
            .Write("# Header")
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("## Header",     $"{AnsiEscape}[1;38;5;5mHeader{AnsiResetEscape}\n\n\n\n")]
    [InlineData("### Header",    $"{AnsiEscape}[1;38;5;5mHeader{AnsiResetEscape}\n\n\n\n")]
    [InlineData("#### Header",   $"{AnsiEscape}[1;38;5;5mHeader{AnsiResetEscape}\n\n\n\n")]
    [InlineData("##### Header",  $"{AnsiEscape}[1;38;5;5mHeader{AnsiResetEscape}\n\n\n\n")]
    [InlineData("###### Header", $"{AnsiEscape}[1;38;5;5mHeader{AnsiResetEscape}\n\n\n\n")]
    public void Given_markdown_with_header_levels_2_to_6_block_should_return_correct_ansi_escaped_string(
        string input,
        string expected)
    {
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_invalid_header_level_block_should_return_correct_ansi_escaped_string()
    {
        var input = "####### Not a valid header - too many hashes";
        var expected = input;
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines()
            .TrimEnd();

        Assert.Equal(expected, actual);
    }
}
