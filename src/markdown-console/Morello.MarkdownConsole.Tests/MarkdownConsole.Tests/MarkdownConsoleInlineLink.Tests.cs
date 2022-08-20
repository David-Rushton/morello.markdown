using System.Reflection;
using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public partial class MarkdownConsoleInlineLinkTests : MarkdownConsoleTests
{
    [Fact]
    public void Given_markdown_with_link_should_return_formatted_link()
    {
        var actual = new TestConsole()
            .Write("[Example](https://example.com)")
            .Output
            .NormaliseNewLines();

        // Spectre Console includes an id with every link.
        // There include a randmon element, and are therefore not deterministic.
        // We use regex to assert format is correct.
        // Format: "<AnsiEscapeCode>;id=<id-contains-random-element>;<link-url><AnsiEscapeCode><Text><AnsiEscape>"
        // Source: https://github.com/spectreconsole/spectre.console/blob/52c1d9122bce4cz73ae9205e2f2aff01cba03a4c4/src/Spectre.Console/Internal/Backends/Ansi/AnsiLinkHasher.cs
        var expected = $"^{AnsiLinkEscape}id=[0-9]+;https://example.com{AnsiEscape}\\\\\\{AnsiPurpleEscapePattern}Example{AnsiResetEscapePattern}{AnsiLinkEscape};+\u001b\\\\\n\n$";

        Assert.Matches(expected, actual);
    }

    [Fact(Skip = "Fails if console is not 80 characters wide")]
    public void Given_markdown_with_valid_image_link_should_render_image()
    {
        var expected = GetValidImageAsAnsiMarkup();
        var actual = new TestConsole()
            .Write($"![Example]({GetValidImageUrl()})")
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_markdown_with_invalid_image_link_should_render_fallback_text()
    {
        var expected = $"{AnsiPurpleItalicEscape}Example{AnsiResetEscape}\n\n";
        var actual = new TestConsole()
            .Write($"![Example]({GetInvalidImageUrl()})")
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    [Fact(Skip = "Requires fix to regex")]
    public void Given_markdown_with_link_reference_definitions_should_render_link()
    {
        var input = @"
[foo bar]:
<https://example.com>
[foo bar]
[foo bar]
";
        var expected = $"^{AnsiLinkEscape}id=[0-9]+;https://example.com{AnsiEscape}\\\\\\{AnsiPurpleEscapePattern}Example{AnsiResetEscapePattern}{AnsiLinkEscape};+\u001b\\\\\n\n$";
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Matches(expected, actual);
    }

    [Fact(Skip = "Requires fix to support AutoLinkInline type")]
    public void Given_markdown_with_auto_link_inline_should_render_link()
    {
        var input = "<https://example.com>";
        var expected = "";
        var actual = new TestConsole()
            .Write(input)
            .Output
            .NormaliseNewLines();

        Assert.Equal(expected, actual);
    }

    private string GetValidImageUrl()
    {
        var root = AppDomain.CurrentDomain.BaseDirectory;
        var path = Path.Join(root, "TestAssets", "valid-image.png");

        return path;
    }

    private string GetInvalidImageUrl()
    {
        var root = AppDomain.CurrentDomain.BaseDirectory;
        var path = Path.Join(root, "TestAssets", "invalid-image.png");

        return path;
    }

    private string GetValidImageAsAnsiMarkup()
    {
        var root = AppDomain.CurrentDomain.BaseDirectory;
        var path = Path.Join(root, "TestAssets", "ansi-escaped-cherry-icon.txt");

        return File.ReadAllText(path);
    }
}
