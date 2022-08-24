using Morello.Markdown.Console.Renderers;

namespace Morello;

/// <summary>
/// Renders markdown in the terminal.
/// </summary>
public static class MarkdownConsole
{
    private static readonly Lazy<AnsiRenderer> DefaultRenderer = new(() => new AnsiRendererBuilder().Build());

    /// <summary>
    /// Writes formatted markdown in the console.
    /// </summary>
    /// <param name="markdown">Markdown to format.</param>
    public static void Write(string markdown)
    {
        DefaultRenderer.Value.Write(markdown);
    }

    /// <summary>
    /// Writes formatted markdown in the console.
    /// </summary>
    /// <param name="markdown">Markdown to format.</param>
    /// <param name="writer">
    ///     Override the default console.
    ///     <remarks>
    ///     Useful for test and debugging only.
    ///     </remarks>
    /// </param>
    public static void Write(string markdown, TextWriter writer)
    {
        var renderer = new AnsiRendererBuilder()
            .RedirectOutput(writer)
            .Build();

        renderer.Write(markdown);
    }
}
