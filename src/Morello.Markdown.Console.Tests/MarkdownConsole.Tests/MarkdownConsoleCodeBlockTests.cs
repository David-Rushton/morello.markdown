using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public partial class MarkdownConsoleTests
{
    [Fact (Skip = "Flaky.  Results differ when run vs when debugged")]
    public void Given_markdown_with_fenced_code_block_should_return_correct_ansi_escaped_string()
    {
        var input = @"```sql
-- comment
SELECT  Foo
FROM    Bar;
```
";

        var expected =  $@"{AnsiEscape}[38;5;238m   1{AnsiEscape}[0m {AnsiEscape}[38;5;242m--{AnsiEscape}[0m{AnsiEscape}[38;5;242m comment{AnsiEscape}[0m
{AnsiEscape}[38;5;238m   2{AnsiEscape}[0m {AnsiEscape}[38;5;203mSELECT{AnsiEscape}[0m{AnsiEscape}[38;5;231m  Foo{AnsiEscape}[0m
{AnsiEscape}[38;5;238m   3{AnsiEscape}[0m {AnsiEscape}[38;5;203mFROM{AnsiEscape}[0m{AnsiEscape}[38;5;231m    Bar;{AnsiEscape}[0m
";

        // TODO: fenced code blocks are produced by one of two syntax highlighters.
        // One of these syntax highlighters is only available if Bat is installed.
        // This test cannot (currently) request a particular syntax highlighter, and so the test is not deterministic.
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
