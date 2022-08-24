using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public partial class MarkdownConsoleTableTests : MarkdownConsoleTests
{
    [Fact]
    public void Given_markdown_with_block_table_should_return_correct_ansi_escaped_string()
    {
        var input = @"| number | word  |
| ------ | ----- |
| 1      | one   |
| 2      | two   |
| 3      | three |
";

        var expected = $@"╭────────┬───────╮
│ {AnsiEscape}[38;5;5mnumber{AnsiResetEscape} │ {AnsiEscape}[38;5;5mword{AnsiResetEscape}  │
├────────┼───────┤
│ 1      │ one   │
│ 2      │ two   │
│ 3      │ three │
╰────────┴───────╯

";

        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

        [Fact]
    public void Given_markdown_with_block_table_and_formatted_cells_should_return_correct_ansi_escaped_string()
    {
        var input = @"| number | word  |
| ------ | ----- |
| 1      | _one_   |
| 2      | *two*   |
| 3      | ~three~ |
";

        var expected = $@"╭────────┬───────────╮
│ {AnsiEscape}[38;5;5mnumber{AnsiResetEscape} │ {AnsiEscape}[38;5;5mword{AnsiResetEscape}      │
├────────┼───────────┤
│ 1      │ {AnsiItalicEscape}one{AnsiResetEscape}   │
│ 2      │ {AnsiBoldEscape}two{AnsiResetEscape}   │
│ 3      │ {AnsiStrikethroughEscape}three{AnsiResetEscape} │
╰────────┴───────────╯

";

        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
