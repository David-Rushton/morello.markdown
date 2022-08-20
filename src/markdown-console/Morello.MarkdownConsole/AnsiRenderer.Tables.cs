using Markdig.Syntax;
using MarkdownTable = Markdig.Extensions.Tables;
using Spectre.Console;

namespace Morello.Markdown.Console;

public partial class AnsiRenderer
{
    private void WriteTableBlock(IAnsiConsole console, MarkdownTable.Table block)
    {
        var table = new Table().Border(TableBorder.Rounded);

        foreach (var item in block)
        {
            if (item is MarkdownTable.TableRow row)
            {
                var rows = new List<Markup>();

                foreach (var cellItem in row)
                {
                    if (cellItem is MarkdownTable.TableCell cell)
                    {
                        foreach (var paragraphItem in cell)
                        {
                            if (paragraphItem is ParagraphBlock paragraph)
                            {
                                var cellWriter = new StringWriter();
                                var settings = new AnsiConsoleSettings
                                {
                                    ColorSystem = ColorSystemSupport.Detect,
                                    Ansi = AnsiSupport.Detect,
                                    Interactive = InteractionSupport.No,
                                    Out = new AnsiConsoleOutput(cellWriter)
                                };
                                var cellBuffer = AnsiConsole.Create(settings);

                                WriteParagraphBlock(paragraph, cellBuffer, suppressNewLine: true);

                                if (row.IsHeader)
                                {
                                    table.AddColumn($"[purple]{cellWriter}[/]");
                                }
                                else
                                {
                                    rows.Add(new Markup(cellWriter.ToString()));
                                }
                            }
                        }
                    }

                }

                if (rows.Any())
                {
                    table.AddRow(rows);
                }
            }
        }

        console.Write(table);
    }
}
