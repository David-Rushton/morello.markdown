# Release Notes

## `v1.1.0`

This is a big update!

### New || Thematic Breaks

We've added support for thematic breaks.  Which is a line used to break up sections within your
documents.  There are several ways to request a break, including:  

```markdown
Some text above a break

***

Some text below a break.

You can use use hyphens or undersocres like so:

---
___

```

This fixes [issue #27](https://github.com/David-Rushton/morello.markdown/issues/27).

See the [CommonMark spec](https://spec.commonmark.org/0.30/#thematic-breaks) for more.

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
