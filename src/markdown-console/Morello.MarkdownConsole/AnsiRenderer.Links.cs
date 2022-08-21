using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Spectre.Console;

namespace Morello.Markdown.Console;

public partial class AnsiRenderer
{
    const string CannotDownloadMessage = "Cannot download image";
    private readonly HttpClient _client = new();

    private void WriteInlineLink(LinkInline link)
    {
        var label = link.FirstChild?.ToString() ?? string.Empty;
        var url = link.Url;

        if (url is null)
        {
            WriteInlineImageLinkFallback(label);
        }
        else if (link.IsImage)
        {
            WriteInlineImageLink(label, url);
        }
        else
        {
            WriteInlineTextLink(label, url);
        }
    }

    private void WriteInlineTextLink(string label, string url)
    {
        _console.Markup($"[purple link={url}]{label}[/]");
    }

    private void WriteInlineImageLink(string label, string url)
    {
        try
        {
            if (File.Exists(url))
            {
                WriteInlineImageLinkUsingFile(url);
            }
            else
            {
                // Best guess, this is a url.
                // We could check, but it fails we will fallback anyway.
                WriteInlineImageLinkUsingWebRequest(url);
            }
        }
        catch
        {
            // TODO: Inform caller we fellback.
            WriteInlineImageLinkFallback(label);
        }
    }

    private void WriteInlineImageLinkUsingFile(string path)
    {
        using var data = new FileInfo(path).OpenRead();
        var image = new CanvasImage(data);
        _console.Write(image);
    }

    private void WriteInlineImageLinkUsingWebRequest(string url)
    {
        using var data = _client.GetStreamAsync(url).Result ?? throw new System.Exception(CannotDownloadMessage);
        var image = new CanvasImage(data);
        _console.Write(image);
    }

    private void WriteInlineImageLinkFallback(string label)
    {
        // Use purple and italic to denote a broken link.
        _console.Markup($"[purple italic]{label}[/]");
    }

    private void WriteLinkReferenceDefinitionBlock(LinkReferenceDefinitionGroup linkBlock)
    {
        foreach (var item in linkBlock)
        {
           if (item is LinkReferenceDefinition linkReference)
            {
                _console.Markup($"[link={linkReference.Url}]{linkReference.Title}[/]");
            }

            // TODO: Not sure if we can get here.
            // TODO: Inform caller we fellback.
        }
    }

}
