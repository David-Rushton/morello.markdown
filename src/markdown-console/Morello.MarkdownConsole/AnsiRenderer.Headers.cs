using Markdig.Syntax;
using Spectre.Console;

namespace Morello.Markdown.Console;

public partial class AnsiRenderer
{
    private void WriteHeadingBlock(IAnsiConsole console, HeadingBlock block)
    {
        var rawContent = block.Inline?.FirstChild?.ToString() ?? throw new CannotConvertMarkdownException("Cannot read content of HeadingBlock");

        if (block.Level == 1)
        {
            console
                .Write(new FigletText(rawContent)
                .Alignment(Justify.Left)
                .Color(Color.Purple));
            return;
        }

        // Levels 2 through 6 are rendered with a common format.
        // We could differentiate by colour and font weight.
        console.MarkupLine($"[bold purple]{rawContent.EscapeMarkup()}[/]");
    }
}
