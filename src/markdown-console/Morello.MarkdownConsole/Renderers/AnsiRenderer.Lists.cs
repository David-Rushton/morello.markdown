using Markdig.Extensions.TaskLists;
using Markdig.Syntax;
using Morello.Markdown.Console.Extensions;
using Spectre.Console;

namespace Morello.Markdown.Console.Renderers;

public partial class AnsiRenderer
{
    private const string DefaultBullet = "  [purple] [/] ";

    private void WriteListBlock(ListBlock block)
    {
        var numberedListCounter = 1;

        foreach (var item in block)
        {
            var bullet = (block.BulletType) switch
            {
                '-' =>  IsTaskList(item) ? "  " : DefaultBullet,
                '1' => $"  [purple]{_numberFormatter.Format(numberedListCounter++)} [/]",
                _ => DefaultBullet
            };

            _console.Markup(bullet);

            foreach(var subItem in (ListItemBlock)item)
            {
                if (subItem is ParagraphBlock paragraphBlock)
                {
                    WriteParagraphBlock(paragraphBlock);
                    continue;
                }

                // We shouldn't be able to get here.
                ThrowOrFallbackToPlainText(subItem);
            }
        }

        static bool IsTaskList(Block itemToCheck)
        {
            return itemToCheck.Descendants().OfType<TaskList>().Any();
        }
    }
}
