# Roadmap

This is a list of some of the bigger changes I'd like to make.

| Item                                                                                         | Status   |
| -------------------------------------------------------------------------------------------- | -------- |
| [Merge `Markdown.Console` and `Markdown.Cli`](##Merge-`Markdown.Console`-and-`Markdown.Cli`) | Planning |
| [Add Prefix to Project](##Add-Prefix-to-Project)                                             | Idea     |
| [Use GitHub Releases](##Use-GitHub-Releases)                                                 | Idea     |
| [Better Nerd Fonts Support](##Better-Nerd-Fonts-Support)                                     | Idea     |
| [Remove Exceptions from `AnsiRenderer`](##Remove-Exceptions-from-`AnsiRenderer`)             | Idea     |

## Merge `Markdown.Console` and `Markdown.Cli`

| Property | Value                                                               |
| -------- | ------------------------------------------------------------------- |
| Issue    | [Issue](https://github.com/David-Rushton/markdown.console/issues/3) |
| Status   | Planning                                                            |

I'd like to combine the two projects into repository.  This would make both projects easier to discover and manage.  It would also reduce confusion around names (which do I need?).

## Add Prefix to Project

| Property | Value                                                               |
| -------- | ------------------------------------------------------------------- |
| Issue    | To be created                                                       |
| Status   | Idea                                                                |

I feel bad about using Markdown as a top level namespace.  For several reasons:

- I don't own Markdown
- NuGet recommends `<CompanyName>.<Product|Technology>.<Feature>`
- Which is aligned with DotNet [recommendations](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces)
- Once imported there is a greater chance of namespace clashes

## Use GitHub Releases

| Property | Value                                                               |
| -------- | ------------------------------------------------------------------- |
| Issue    | To be created                                                       |
| Status   | Idea                                                                |

Make prebuilt binaries available for download.

## Better Nerd Fonts Support

| Property | Value                                                               |
| -------- | ------------------------------------------------------------------- |
| Issue    | [Issue](https://github.com/David-Rushton/markdown.console/issues/1) |
| Status   | Planning                                                            |

Nerd Font fallback prints `?`.  Not great.

## Remove Exceptions from `AnsiRenderer`

| Property | Value                                                               |
| -------- | ------------------------------------------------------------------- |
| Issue    | To be created                                                       |
| Status   | Planning                                                            |

### Issue

- There are a number of places where the `AnsiRenderer` throws a `NotSupportedException`
- The `AnsiRenderer` can return yellow text reading: `Block type not supported: <TypeName>`

In pratice I don't think this is an issue.  I am fairly confident you cannot reach these statements.
However:

- There could be edge cases I haven't considered
- In time the `Markdig` could evolve to expose this lines

It would be better if the library fellback to rendering in plain text.  Possible with some visual hint
or warning in debug mode.
