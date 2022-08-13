# Markdown Console

![MC PR Build and Test](https://github.com/David-Rushton/markdown.console/actions/workflows/on_pull_request_to_main.yml/badge.svg?branch=main)
![MC Publish to NuGet](https://github.com/David-Rushton/markdown.console/actions/workflows/on_push_to_main.yml/badge.svg?branch=main)
![NuGet Downloads](https://img.shields.io/nuget/dt/Morello.MarkdownConsole?label=NuGet%20downloads)
![License](https://img.shields.io/github/license/david-rushton/morello.markdown)

A .NET library that pretty prints markdown in the console.

## Download

You can download from [NuGet](https://www.nuget.org/packages/Morello.MarkdownConsole).

## Acknowledgments

This library uses these amazing projects to make markdown look its best in your console:

- [Spectre Console](https://github.com/spectreconsole/spectre.console)
- [Markdig](https://github.com/xoofx/markdig)
- [Bat](https://github.com/sharkdp/bat)
- [Nerd Fonts](https://www.nerdfonts.com/)

Bat and Nerd Fonts both need to be installed on your local system.  When they are not available this library
will still work, but the output will not support the full range of features.

> ⚠️ WARNING ⚠️  
> Nerd Fonts fallback is a bit [messy at the moment](https://github.com/David-Rushton/morello.markdown/issues/1)
