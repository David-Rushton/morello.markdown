using Spectre.Console;

namespace Morello.Markdown.Console.Converters;

internal static class AnsiSupportConverter
{
    internal static AnsiSupport FromAnsiSupported(bool ansiSupported) =>
        ansiSupported
            ? AnsiSupport.Yes
            : AnsiSupport.No;
}
