# ![cherry icon](../../images/cherry-64.png) Markdown Console

[![Console PR Build and Test](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_console_on_pull_request_to_main.yml/badge.svg)](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_console_on_pull_request_to_main.yml)
[![Console Publish to NuGet](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_console_on_push_to_main.yml/badge.svg)](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_console_on_push_to_main.yml)
![NuGet Downloads](https://img.shields.io/nuget/dt/Morello.MarkdownConsole?label=NuGet%20downloads)
![License](https://img.shields.io/github/license/david-rushton/morello.markdown)

A .NET library that pretty prints markdown in the console.

> 🐲 This is an early release 🐲  
> There be dragons  
> Report them [here](https://github.com/David-Rushton/morello.markdown/issues/new/choose)

## Download

Download from [NuGet](https://www.nuget.org/packages/Morello.MarkdownConsole).

```shell
dotnet add package Morello.MarkdownConsole --version 1.0.1
```

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
