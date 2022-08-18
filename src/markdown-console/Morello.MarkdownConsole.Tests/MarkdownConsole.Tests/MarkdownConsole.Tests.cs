using Morello.Markdown.Console.Tests.Extensions;
using Xunit;

namespace Morello.Markdown.Console.Tests;

public abstract class MarkdownConsoleTests
{
    // https://en.wikipedia.org/wiki/ANSI_escape_code
    public const string AnsiEscape = "\u001b";
    public const string AnsiResetEscape = "\u001b[0m";
    public const string AnsiResetEscapePattern = "\u001b\\[0m";
    public const string AnsiBoldEscape = "\u001b[1m";
    public const string AnsiItalicEscape = "\u001b[3m";
    public const string AnsiBoldItalicEscape = "\u001b[1;3m";
    public const string AnsiStrikethroughEscape = "\u001b[9m";
    public const string AnsiBoldStrikethroughEscape = "\u001b[1;9m";
    public const string AnsiCodeStartEscape = "\u001b[38;5;5m\u001b[0m\u001b[7;38;5;5m";
    public const string AnsiCodeEndEscape = "\u001b[0m\u001b[38;5;5m";
    public const string AnsiLinkEscape = "\u001b]8;";
    public const string AnsiPurpleEscape = "\u001b[38;5;5m";
    public const string AnsiPurpleItalicEscape = "\u001b[3;38;5;5m";
    public const string AnsiPurpleEscapePattern = "\u001b\\[38;5;5m";
}
