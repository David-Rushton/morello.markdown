using Markdown.Console.Tests.Extensions;
using Xunit;

namespace Markdown.Console.Tests;

public partial class MarkdownConsoleTests
{
    [Fact(Skip = "Flakey || Test results depend on underlying console width")]
    public void Given_markdown_with_header_level_1_block_should_return_correct_ansi_escaped_string()
    {
        var expected = @$"{AnsiEscape}[38;5;5m  _   _                      _               {AnsiEscape}[0m
{AnsiEscape}[38;5;5m | | | |   ___    __ _    __| |   ___   _ __ {AnsiEscape}[0m
{AnsiEscape}[38;5;5m | |_| |  / _ \\  / _` |  / _` |  / _ \\ | '__|{AnsiEscape}[0m
{AnsiEscape}[38;5;5m |  _  | |  __/ | (_| | | (_| | |  __/ | |   {AnsiEscape}[0m
{AnsiEscape}[38;5;5m |_| |_|  \\___|  \\__,_|  \\__,_|  \\___| |_|   {AnsiEscape}[0m
{AnsiEscape}[38;5;5m                                             {AnsiEscape}[0m
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
