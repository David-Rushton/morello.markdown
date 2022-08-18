using Markdig.Extensions.AutoIdentifiers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Spectre.Console;

namespace Morello.Markdown.Console;

public partial class AnsiRenderer
{
    const string CannotDownloadMessage = "Cannot download image";
    private readonly HttpClient _client = new();

    private void WriteInlineLink(IAnsiConsole console, LinkInline link)
    {
        var label = link.FirstChild?.ToString() ?? string.Empty;
        var url = link.Url;

        if (url is null)
        {
            WriteInlineImageLinkFallback(console, label);
        }
        else if (link.IsImage)
        {
            WriteInlineImageLink(console, label, url);
        }
        else
        {
            WriteInlineTextLink(console, label, url);
        }
    }

    private void WriteInlineTextLink(IAnsiConsole console, string label, string url)
    {
        console.Markup($"[purple link={url}]{label}[/]");
    }

    private void WriteInlineImageLink(IAnsiConsole console, string label, string url)
    {
        try
        {
            if (File.Exists(url))
            {
                WriteInlineImageLinkUsingFile(console, url);
            }
            else
            {
                // Best guess, this is a url.
                // We could check, but it fails we will fallback anyway.
                WriteInlineImageLinkUsingWebRequest(console, url);
            }
        }
        catch
        {
            // TODO: We should log or report failures.
            WriteInlineImageLinkFallback(console, label);
        }
    }

    private void WriteInlineImageLinkUsingFile(IAnsiConsole console, string path)
    {
        using var data = new FileInfo(path).OpenRead();
        var image = new CanvasImage(data);
        console.Write(image);
    }

    private void WriteInlineImageLinkUsingWebRequest(IAnsiConsole console, string url)
    {
        using var data = _client.GetStreamAsync(url).Result ?? throw new System.Exception(CannotDownloadMessage);
        var image = new CanvasImage(data);
        console.Write(image);
    }

    private void WriteInlineImageLinkFallback(IAnsiConsole console, string label)
    {
        // Use purple and italic to denote a broken link.
        console.Markup($"[purple italic]{label}[/]");
    }

    private void WriteLinkReferenceDefinitionBlock(IAnsiConsole console, LinkReferenceDefinitionGroup linkBlock)
    {
        foreach (var item in linkBlock)
        {
           if (item is LinkReferenceDefinition linkReference)
            {
                console.Markup($"[link={linkReference.Url}]{linkReference.Title}[/]");
            }

            // TODO: Not sure if we can get here.
        }
    }

}
