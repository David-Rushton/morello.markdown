using Markdig.Extensions.AutoIdentifiers;
using Markdig.Extensions.TaskLists;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Morello.Markdown.Console.Formatters;
using Morello.Markdown.Console.SyntaxHighlighters;
using Spectre.Console;
using MarkdownTable = Markdig.Extensions.Tables;

namespace Morello.Markdown.Console;

public partial class AnsiRenderer
{
    private void WriteListBlock(ListBlock block)
    {
        var numberedListCounter = 1;

        foreach (var item in block)
        {
            var defaultBullet = "  [purple] [/] ";
            var bullet = (block.BulletType) switch
            {
                '-' =>  IsTaskList(item) ? "  " : defaultBullet,
                '1' => $"  [purple]{_numberFormatter.Format(numberedListCounter++)} [/]",
                _ => defaultBullet
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
