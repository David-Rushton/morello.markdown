# Release Notes

## `v1.1.0`

This is a big update!

We've added support for:

- Thematic breaks  
  `***`, `---` or `___` adds a horizontal line.

- Image Links  
  Format: `![fallback text](file_path_or_url_to_image).  
  See below for more details.  

- Embedded inlines  
  You can now embed inline styles within each other.  
  Example: `**bold text with _bold and italic_ section**`.  

- 🐛 Fixed a bug that lead to incorrect multiline quotes  
  Quotes are prefixed with a space and a horizontal chevron.  
  Multiline quotes omitted the space on the 2nd and subsequent lines.  

### Image Links

Powered by the amazing [ImageSharp](https://github.com/SixLabors/ImageSharp); we support the
following:

- TIFF
- BMP
- PNG
- JPEG
- GIF
- PBM
- TGA
- Webp
## `v1.0.2`

Improved README.

I want to warn users that this library is very new, and still in pre-release.

## `v1.0.1`

Small bug fixes.

## `v1.0.0`

We have a project name - `Morello`!

I've moved the `MarkdownConsole` into a our top-level namespace.

```cs
using Morello;

MarkdownConsole.Write("# Markdown");
```

## `v.0.3.0`

Titles are now rendered left-aligned.  Previously centre-aligned.  Centre alignment didn't look great
in wide terminals.  The heading looked detected from the content.  Left alignment ensures the heading
appears above the content.

## `v.0.2.0`

We have added unit tests.

## `v.0.1.0`

Big Bang!

Converts markdown to beautifully formatted terminal output.

```csharp
using Markdown.Console;

MarkdownConsole.Write(@"
# Markdown Console

Some content that includes *bold*, _italic_ and ~strikethrough~ text.

We support:

- Tables
- Fenced code blocks
- Quotes
- Lists

Syntax highlighting is available if you have [Bat](https://github.com/sharkdp/bat) on your path.
");

```

> ⚠️ Warning ⚠️
> We used [Nerd Fonts](https://www.nerdfonts.com/) when rendering to the console.  If your font doesn't support the fall range of characters some elements may not render correctly.
