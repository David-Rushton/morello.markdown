using Markdig.Extensions.TaskLists;
using Markdig.Syntax;
using Morello.Markdown.Console.Extensions;
using Spectre.Console;

namespace Morello.Markdown.Console.Renderers;

public partial class AnsiRenderer
{
    private void WriteListBlock(ListBlock block)
    {
        var numberedListCounter = 1;

        foreach (var item in block)
        {
            var listBullet = $" [purple]{_characterSet.ListBullet}[/] ";
            var bullet = (block.BulletType) switch
            {
                '-' =>  IsTaskList(item) ? " " : listBullet,
                '1' => $" [purple]{_numberFormatter.Format(numberedListCounter++, _characterSet)} [/]",
                _ => listBullet
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
