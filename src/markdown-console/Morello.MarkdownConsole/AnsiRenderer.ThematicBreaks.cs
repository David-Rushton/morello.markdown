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
    private void WriteThematicBreakBlock(ThematicBreakBlock thematicBreakBlock)
    {
        const char lineCharacter = '═';
        var charactersRequired = GetConsoleWidth() - 2;
        var line = new string(lineCharacter, charactersRequired);

        _console.MarkupLine($"[purple] {line}[/]");
    }
}
