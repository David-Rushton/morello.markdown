# Release Notes

## `v0.2.0`

### Combined Styles

You can now combine styles.  

```md
**Bold text with _italic_ section**
```

In previous versions the word italic would have been written in italic text.  It is now rendered as both bold and italic.  This matches the [CommonMark spec](https://spec.commonmark.org/).

### Thematic Breaks

We've added support for thematic breaks.  Any of the following will render a horizontal line.

```md
***
---
___
```

This is part of the journey to supporting the full [CommonMark spec](https://spec.commonmark.org/).

### Image Links

We can now display images downloaded from the web.

```md
![fallback text](https://example.com/some-image.png)
```

This feature is powered by [ImageSharp](https://github.com/SixLabors/ImageSharp).  We support the following file formats:

- TIFF
- BMP
- PNG
- JPEG
- GIF
- PBM
- TGA
- Webp
