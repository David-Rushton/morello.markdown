using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public class MarkdownConsoleCodeBlockTests :  MarkdownConsoleTests
{
    [Fact (Skip = "Flaky.  Crashes if run in debug mode.")]
    public void Given_markdown_with_fenced_code_block_should_return_correct_ansi_escaped_string()
    {
        var input = @"```sql
-- comment
SELECT  Foo
FROM    Bar;
```
";

        var expected =  $@"{AnsiEscape}[38;5;244m   1 {AnsiEscape}[0m-- comment
{AnsiEscape}[38;5;244m   2 {AnsiEscape}[0mSELECT  Foo
{AnsiEscape}[38;5;244m   3 {AnsiEscape}[0mFROM    Bar;\n\n
";

        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact(Skip = "Flaky.  Crashes if run in debug mode.")]
    public void Given_markdown_with_indented_code_block_should_return_correct_ansi_escaped_string()
    {
        var input = @"
    SELECT foo
    FROM bar
";

        var expected =  $@"{AnsiEscape}[38;5;244m   1 {AnsiEscape}[0m-- comment
{AnsiEscape}[38;5;244m   2 {AnsiEscape}[0mSELECT  Foo
{AnsiEscape}[38;5;244m   3 {AnsiEscape}[0mFROM    Bar;\n\n
";

        var actual = new TestConsole()
            .FallbackToPlainText()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
