# Release Notes

## `v0.3.0`

We 💖 [Nerd Fonts](https://www.nerdfonts.com/)❗  

However `Markdown.Cli` only looks its best if your terminal font has been patched.  For the majority
of our users this isn't the case.  Previously we relied on your fonts fallback glyph.  This is often
ugly, and seldom a good fit.

The new default sticks to the standard and extended ASCII table.

You can still opt-in into Nerd Fonts:

```csharp
using Morello;

MarkdownConsole.UseNerdFonts(UseNerdFonts.Yes);
MarkdownConsole.Write("markdown here");
```

## Without Nerd Fonts (Default)

![Withput Nerd Fonts](https://raw.githubusercontent.com/David-Rushton/morello.markdown/main/images/examples/nerd-fonts-disabled.png)

## With Nerd Fonts

![With Nerd Fonts](https://raw.githubusercontent.com/David-Rushton/morello.markdown/main/images/examples/nerd-fonts-enabled-and-supported.png)
