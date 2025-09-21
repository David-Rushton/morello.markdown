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
            var escapedHeader = rawContent.EscapeMarkup();

            if (block.Level == 1)
            {
                FigletText figletText = new(escapedHeader)
                {
                    Justification = Justify.Left,
                    Color = Color.Purple,
                };

                _console.Write(figletText);
                return;
            }

            // Levels 2 through 6 are rendered with a common format.
            // We could differentiate by colour and font weight.
            _console.MarkupLine($"[bold purple]{escapedHeader}[/]");
        }
    }
}
