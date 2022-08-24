# Feature Flags

You can override default behaviour, via various environment variables.

## `MORELLO_MARKDOWN_CONSOLE_THROW_ON_UNSUPPORTED_TYPE`

| Type    | Default |
| ------- | ------- |
| Boolean | False   |

By default we make a best effort to render any passed markdown.  If we encounter unsupported syntax
we fallback to plain text.  

When this feature flag is `true`, we throw instead.

This is useful when testing and debugging.  Plain text fallback can hide errors, that are much more
obvious after a throw.

## `MORELLO_MARKDOWN_CONSOLE_FORCE_BASIC_SYNTAX_HIGHLIGHTER`

| Type    | Default |
| ------- | ------- |
| Boolean | False   |

We support several syntax highlighters.  The default highlighter is powered by [Bat](https://github.com/sharkdp/bat).
It provides the most features and the richest output.  However it is only available if Bat is installed
on the local system.  This isn't always the case.

When this feature flag is `true` we always use the basic highlighter.

This is useful when testing and debugging, by providing predictable output.

## `MORELLO_MARKDOWN_CONSOLE_FORCE_ANSI_COLOUR`

| Type    | Default |
| ------- | ------- |
| Boolean | False   |

We use Ansi Escape Codes to format our output.  Not all terminals support Ansi.  By default we detect
support.  But sometimes detection can return a false negative.  This results in plain text output.
Generally this happens when standard in/out have been redirected.  Which is common with test runners.

When `true` we always output Ansi formatted strings.

This is useful when testing and debugging.
