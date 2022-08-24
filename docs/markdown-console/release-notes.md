# Release Notes

## `v1.3.0`

This release contains mostly internal changes.

Our test suite now runs in Rider.  Previously all tests failed in Rider because we incorrectly
detected the level of Ansi escape code support.  

We've improved how we fallback to plain text.  The previous approach proved to be too complex.  We
now fallback - or fail if in test mode.

## `v1.2.0`

Internal changes only.

We've moved a few things around to simplify future changes.

## `v1.1.0`

### Plain Text Fallback

`MarkdownConsole` now falls back to plain text for any unsupported markdown elements.  Previously we
threw an exception or printed an ugly warning message.  This was bad for two reasons:

- Mixing exceptions and printed warnings is inconsistent
- If we do not support a type we shouldn't omit the content

`MarkdownConsole` is now a best effort renderer.  It will always print all of the text passed to it.
Where we can we will format it.  Where we cannot we will print plain text.

A future update will introduce a callback/logger/return value that informs the caller where and why
we have fallen back to plain text.  That is still in the planning.  However I expect this to be available
in the next few updates.

### Other Changes

We've added support for:

- Thematic breaks  
  `***`, `---` or `___` adds a horizontal line.

- Image Links  
  Format: `![fallback text](file_path_or_url_to_image).  
  See below for more details.  

- Embedded inlines  
  You can now embed inline styles within each other.  
  Example: `**bold text with _bold and italic_ section**`.  

And we've fixed a few bugs:

- 🐛 Incorrect multiline quotes  
  Quotes are prefixed with a space and a horizontal chevron.  
  Multiline quotes omitted the space on the 2nd and subsequent lines.  

- 🐛 Test runners could crash
  We didn't support the case where `MarkdownConsole` was not attached to a terminal.

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
