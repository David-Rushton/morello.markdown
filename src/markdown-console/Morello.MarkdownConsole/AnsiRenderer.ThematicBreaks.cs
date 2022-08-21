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
    private void WriteThematicBreakBlock(IAnsiConsole console, ThematicBreakBlock thematicBreakBlock)
    {
        const char lineCharacter = '═';
        var charactersRequired = GetConsoleWidth(console) - 2;
        var line = new string(lineCharacter, charactersRequired);

        console.MarkupLine($"[purple] {line}[/]");
    }
}
