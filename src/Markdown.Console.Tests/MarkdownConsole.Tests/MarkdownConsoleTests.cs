using Markdown.Console.Tests.Extensions;
using Xunit;

namespace Markdown.Console.Tests;

public partial class MarkdownConsoleTests
{
    // https://en.wikipedia.org/wiki/ANSI_escape_code
    private const string AnsiEscape = "\u001b";
    private const string AnsiResetEscape = "\u001b[0m";
    private const string AnsiBoldEscape = "\u001b[1m";
    private const string AnsiItalicEscape = "\u001b[3m";
    private const string AnsiStrikethroughEscape = "\u001b[9m";
    private const string AnsiCodeStartEscape = "\u001b[38;5;5m\u001b[0m\u001b[7;38;5;5m";
    private const string AnsiCodeEndEscape = "\u001b[0m\u001b[38;5;5m";

}
