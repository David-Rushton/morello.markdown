using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;

namespace Markdown.Console.SyntaxHighlighters;

/// <summary>
/// A wrapper around Bat.  A cli tool that provides syntax highlighting.
///
/// <remarks>
/// Requires Bat.exe to available in the current working directory or via the path
/// environmental variable.
/// </remarks>
///
/// <seealso href="https://github.com/sharkdp/bat">Bat/seealso>
/// </summary>
public class BatSyntaxHighlighter : ISyntaxHighlighter
{
    /// <inheritdoc/>
    public bool TryGetHighlightSyntax(
        string code,
        string? language,
        [NotNullWhen(returnValue: true)]
        out string? highlightedCode)
    {
        try
        {
            var info = new ProcessStartInfo
            {
                FileName = "bat.exe",
                ArgumentList =
                {
                    "--number",
                    "--color", "always",
                    "--terminal-width", System.Console.BufferWidth.ToString(),
                    // The file name doesn't need to exist.
                    // Bat uses the file extension to configure highlighting.
                    // See also: bat --help
                    "--file-name", $"lang.{language ?? "unknown"}"
                },
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                CreateNoWindow = true
            };
            var process = Process.Start(info) ?? throw new Exception("Cannot start Bat");
            var writer = process.StandardInput;
            var output = process.StandardOutput;

            writer.WriteLine(code);
            writer.Close();
            process.WaitForExit();

            highlightedCode = output.ReadToEnd();
            return true;
        }
        catch
        {
            highlightedCode = null;
            return false;
        }
    }
}
