namespace Morello.Markdown.Console.Tests.Extensions;

public static class StringExtensions
{
    public static string NormaliseNewLines(this string original)
    {
        // Files in this repository are saved with a new line of LR.
        // Spectre Console uses the value stored in Environment.NewLine.
        // When running some tests on Windows we need to replace the lines delimiter to ensure expected and actual values match.

        const string CrLf = "\r\n";
        const string Lf = "\n";

        return original.Replace(oldValue: CrLf, newValue: Lf);
    }
}
