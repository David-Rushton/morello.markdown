using Markdown.Console.Tests.Extensions;
using Xunit;

namespace Markdown.Console.Tests;

public partial class MarkdownConsoleTests
{
    [Fact]
    public void Given_markdown_with_bullet_list_block_should_return_correct_ansi_escaped_string()
    {
        var input = @"Some list:
- item one
- item two
- item three
";

        var expected = $@"Some list:

  {AnsiEscape}[38;5;5m {AnsiEscape}[0m item one
  {AnsiEscape}[38;5;5m {AnsiEscape}[0m item two
  {AnsiEscape}[38;5;5m {AnsiEscape}[0m item three

";

        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_task_list_block_should_return_correct_ansi_escaped_string()
    {
        var input = @"Some list:

- [ ] item one
- [x] item two
- [ ] item three

";

        var expected = $@"Some list:

  {AnsiEscape}[38;5;5m {AnsiEscape}[0m item one
  {AnsiEscape}[38;5;5m {AnsiEscape}[0m item two
  {AnsiEscape}[38;5;5m {AnsiEscape}[0m item three

";

        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_numbered_list_block_should_return_correct_ansi_escaped_string()
    {
        var input = @"Some list:

1. item 1
1. item 2
1. item 3
1. item 4
1. item 5
1. item 6
1. item 7
1. item 8
1. item 9
1. item 10

";

        var expected = $@"Some list:

  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 1
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 2
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 3
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 4
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 5
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 6
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 7
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 8
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 9
  {AnsiEscape}[38;5;5m {AnsiEscape}[0mitem 10

";

        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
