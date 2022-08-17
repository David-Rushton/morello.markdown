using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public partial class MarkdownConsoleThematicBreakTests : MarkdownConsoleTests
{
    [Fact]
    public void Given_markdown_with_thematic_block_should_return_line()
    {
        // Test console has a width of 80.
        // We draw lines as full width with a leading and omitted final char.
        var line = new string('═', 78);
        var actual = new TestConsole()
            .Write("***")
            .Output
            .NormaliseNewLines();

        var expected = $"{AnsiPurpleEscape} {line}{AnsiResetEscape}\n\n";

        Assert.Equal(expected, actual);
    }
}
