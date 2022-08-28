# ![cherry icon](./images/cherry-64.png) Morello Markdown

[![Console PR Build and Test](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_console_on_pull_request_to_main.yml/badge.svg)](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_console_on_pull_request_to_main.yml)
[![Console Publish to NuGet](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_console_on_push_to_main.yml/badge.svg)](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_console_on_push_to_main.yml)
![NuGet Downloads](https://img.shields.io/nuget/dt/Morello.MarkdownConsole?label=NuGet%20downloads)
[![CLI PR Build and Test](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_cli_on_pull_request_to_main.yml/badge.svg)](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_cli_on_pull_request_to_main.yml)
[![CLI Publish to Releases](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_cli_on_push_to_main.yml/badge.svg)](https://github.com/David-Rushton/morello.markdown/actions/workflows/markdown_cli_on_push_to_main.yml)  
![License](https://img.shields.io/github/license/david-rushton/morello.markdown)

---

Tools for rendering markdown beautifully in your terminal.

## `MarkdownCli`

A console app that prints markdown in your console.

![example screen shot](./images/examples/markdown-cli-example.png)

### Install

Download the [latest binaries](https://github.com/David-Rushton/morello.markdown/releases).

Powered by [MarkdownConsole](##`MarkdownConsole`).

## `MarkdownConsole`

A .Net library that uses [Ansi Escape Codes](https://en.wikipedia.org/wiki/ANSI_escape_code) to pretty print markdown.

```shell
dotnet add package Morello.MarkdownConsole --version 1.0.1
```

## Acknowledgments

This library uses these amazing projects to make markdown look its best in your console.

### Dependencies

- [Spectre Console](https://github.com/spectreconsole/spectre.console)
- [Markdig](https://github.com/xoofx/markdig)
 
### Optional

If you have Bat or Nerd Fonts installed `Morello Markdown` will add extra flourishes.  Don't worry if you
don't use these fantastic projects.  We will still render beautiful markdown in your terminal.

- [Bat](https://github.com/sharkdp/bat)  
  When installed Bat provides colourful syntax highlighting.  
  When not we fallback to a basic highlighter.

- [Nerd Fonts](https://www.nerdfonts.com/)  
  When installed Nerd Fonts add extra decorations to the text.
  When not we use a mix of colours and styles.
