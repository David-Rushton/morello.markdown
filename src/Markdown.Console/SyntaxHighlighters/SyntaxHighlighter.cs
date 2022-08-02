using System.Collections.Generic;

namespace Markdown.Console.SyntaxHighlighters;

public class SyntaxHighlighter
{
    // Order is important here.
    // Syntax highlighters are tried from first until last.
    private readonly List<ISyntaxHighlighter> _highlighters = new()
    {
        new BatSyntaxHighlighter(),
        new BasicSyntaxHighlighter()
    };

    /// <summary>
    /// Highlights syntax within a code block.
    /// </summary>
    /// <param name="code">Highlight the syntax within this code block</param>
    /// <param name="language">Hint provided to the syntax highlighter</param>
    /// <returns></returns>
    public string GetHighlightedSyntax(string code, string? language)
    {
        foreach (var highlighter in _highlighters)
        {
            if (highlighter.TryGetHighlightSyntax(code, language, out var highlightedCode))
            {
                return highlightedCode;
            }
        }

        throw new Exception("Syntax highlighting failed");
    }
}
