using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public partial class MarkdownConsoleListTests : MarkdownConsoleTests
{
    [Fact]
    public void Given_markdown_with_bullet_list_block_should_return_correct_nerd_font_ansi_escaped_string()
    {
        var input = @"Some list:
- item one
- item two
- item three
";

        var expected = $@"Some list:

 {AnsiEscape}[38;5;5m{AnsiEscape}[0m item one
 {AnsiEscape}[38;5;5m{AnsiEscape}[0m item two
 {AnsiEscape}[38;5;5m{AnsiEscape}[0m item three

";

        var actual = new TestConsole()
            .SetNerdFonts(UseNerdFonts.Yes).Write(input).Output.NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_bullet_list_block_should_return_correct_ascii_font_ansi_escaped_string()
    {
        var input = @"Some list:
- item one
- item two
- item three
";

        var expected = $@"Some list:

 {AnsiEscape}[38;5;5m■{AnsiEscape}[0m item one
 {AnsiEscape}[38;5;5m■{AnsiEscape}[0m item two
 {AnsiEscape}[38;5;5m■{AnsiEscape}[0m item three

";

        var actual = new TestConsole()
            .SetNerdFonts(UseNerdFonts.No)
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_task_list_block_should_return_correct_nerd_font_ansi_escaped_string()
    {
        var input = @"Some list:

- [ ] item one
- [x] item two
- [ ] item three

";

        var expected = $@"Some list:

 {AnsiEscape}[38;5;5m{AnsiEscape}[0m item one
 {AnsiEscape}[38;5;5m{AnsiEscape}[0m item two
 {AnsiEscape}[38;5;5m{AnsiEscape}[0m item three

";

        var actual = new TestConsole()
            .SetNerdFonts(UseNerdFonts.Yes)
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_task_list_block_should_return_correct_ascii_font_ansi_escaped_string()
    {
        var input = @"Some list:

- [ ] item one
- [x] item two
- [ ] item three

";

        var expected = $@"Some list:

 {AnsiEscape}[38;5;5m[ ]{AnsiEscape}[0m item one
 {AnsiEscape}[38;5;5m[x]{AnsiEscape}[0m item two
 {AnsiEscape}[38;5;5m[ ]{AnsiEscape}[0m item three

";

        var actual = new TestConsole()
            .SetNerdFonts(UseNerdFonts.No)
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_numbered_list_block_should_return_correct_nerd_font_ansi_escaped_string()
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
            .SetNerdFonts(UseNerdFonts.Yes)
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void Given_markdown_with_numbered_list_block_should_return_correct_ascii_font_ansi_escaped_string()
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

 {AnsiEscape}[38;5;5m1 {AnsiEscape}[0mitem 1
 {AnsiEscape}[38;5;5m2 {AnsiEscape}[0mitem 2
 {AnsiEscape}[38;5;5m3 {AnsiEscape}[0mitem 3
 {AnsiEscape}[38;5;5m4 {AnsiEscape}[0mitem 4
 {AnsiEscape}[38;5;5m5 {AnsiEscape}[0mitem 5
 {AnsiEscape}[38;5;5m6 {AnsiEscape}[0mitem 6
 {AnsiEscape}[38;5;5m7 {AnsiEscape}[0mitem 7
 {AnsiEscape}[38;5;5m8 {AnsiEscape}[0mitem 8
 {AnsiEscape}[38;5;5m9 {AnsiEscape}[0mitem 9
 {AnsiEscape}[38;5;5m10 {AnsiEscape}[0mitem 10

";

        var actual = new TestConsole()
            .SetNerdFonts(UseNerdFonts.No)
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }
}
