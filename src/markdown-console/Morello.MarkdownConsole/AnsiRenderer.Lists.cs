using Markdig.Extensions.TaskLists;
using Markdig.Syntax;
using Spectre.Console;

namespace Morello.Markdown.Console;

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
                WriteParagraphBlock((ParagraphBlock)subItem);
            }
        }

        static bool IsTaskList(Block itemToCheck)
        {
            return itemToCheck.Descendants().OfType<TaskList>().Any();
        }
    }
}
