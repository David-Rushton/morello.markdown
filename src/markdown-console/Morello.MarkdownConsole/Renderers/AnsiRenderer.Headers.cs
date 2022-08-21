using Markdig.Syntax;
using Spectre.Console;

namespace Morello.Markdown.Console.Renderers;

public partial class AnsiRenderer
{
    private void WriteHeadingBlock(HeadingBlock block)
    {
        var rawContent = block.Inline?.FirstChild?.ToString();

        if (rawContent is not null)
        {
            if (block.Level == 1)
            {
                _console
                    .Write(new FigletText(rawContent.EscapeMarkup())
                    .Alignment(Justify.Left)
                    .Color(Color.Purple));
                return;
            }

            // Levels 2 through 6 are rendered with a common format.
            // We could differentiate by colour and font weight.
            _console.MarkupLine($"[bold purple]{rawContent.EscapeMarkup()}[/]");
        }
    }
}
