using System.Reflection;
using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public partial class MarkdownConsoleRegressionTests : MarkdownConsoleTests
{
    [Fact]
    public void Given_markdown_with_line_break_should_not_quote_second_line()
    {
        // Bug:
        /*
            foo
            bar
        */

        // Returned unexpected quote on second line:
        /*
            foo
            ❯ bar
        */

        var input = @"foo
bar";
        var expected = $"{input}\n\n";
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
