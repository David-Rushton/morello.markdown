using Markdig;
using Markdig.Syntax;
using Morello.Markdown.Console;
using Spectre.Console;

namespace Morello;

/// <summary>
/// Renders markdown in the terminal.
/// </summary>
public static class MarkdownConsole
{
    private readonly static MarkdownPipeline _markdownPipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    private readonly static Lazy<IAnsiConsole> _defaultConsole = new(() =>
    {
        return AnsiConsole.Create(new AnsiConsoleSettings
        {
            ColorSystem = ColorSystemSupport.Detect,
            Ansi = AnsiSupport.Detect,
            Interactive = InteractionSupport.No,
            Out = new AnsiConsoleOutput(System.Console.Out)
        });
    });

    /// <summary>
    /// Writes formatted markdown in the console.
    /// </summary>
    /// <param name="markdown">Markdown to format.</param>
    public static void Write(string markdown)
    {
        Write(markdown, _defaultConsole.Value);
    }

    /// <summary>
    /// Writes formatted markdown in the console.
    /// </summary>
    /// <param name="markdown">Markdown to format.</param>
    /// <param name="console">
    ///     Override the default console.
    ///     <remarks>
    ///     Useful for test and debugging only.
    ///     </remarks>
    /// </param>
    public static void Write(string markdown, IAnsiConsole console)
    {
        var doc = GetMarkdownDocument(markdown);
        new AnsiRenderer(console).Write(doc, console);
    }

    private static MarkdownDocument GetMarkdownDocument(string markdown)
    {
        return Markdig.Markdown.Parse(markdown, _markdownPipeline);
    }
}
