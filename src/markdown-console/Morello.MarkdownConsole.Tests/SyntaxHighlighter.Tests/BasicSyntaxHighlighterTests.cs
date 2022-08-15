using Morello.Markdown.Console.SyntaxHighlighters;
using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public class BasicSyntaxHighlighterTests
{
    [Fact]
    public void Given_code_produces_highlighted_syntax()
    {
        var code = @"
-- some comment
SELECT Foo
FROM   Bar;
";

        var expected = "\u001b[38;5;244m   1 \u001b[0m\n\u001b[38;5;244m   2 \u001b[0m-- some comment\n\u001b[38;5;244m   3 \u001b[0mSELECT Foo\n\u001b[38;5;244m   4 \u001b[0mFROM   Bar;\n\u001b[38;5;244m   5 \u001b[0m\n";

        var highlighter = new BasicSyntaxHighlighter();
        if (!highlighter.TryGetHighlightSyntax(code, "sql", out var highlightedCode))
        {

            Assert.True(false, "Basic syntax highlighter should never fail");
        }

        Assert.Equal(expected, actual: highlightedCode);
    }
}
