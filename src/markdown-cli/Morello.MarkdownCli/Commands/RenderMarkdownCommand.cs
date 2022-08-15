using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.IO;
using Morello;
using Morello.MarkdownCli;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Morello.MarkdownCli.Commands;

/// <summary>
/// Render markdown in your terminal.
/// /// </summary>
public class RenderMarkdownCommand : Command<RenderMarkdownCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [Description("You can pass markdown or a path to a markdown file")]
        [CommandArgument(position: 0, "[markdown|file]")]
        public string[] MarkdownSources { get; set; } = Array.Empty<string>();

        [Description("Enables verbose error messages and validates usage examples")]
        [CommandOption("-d|--debug", IsHidden = true)]
        public bool IsDebugMode { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        foreach(var source in settings.MarkdownSources)
        {
            if (File.Exists(source))
            {
                var content = File.ReadAllText(source);
                MarkdownConsole.Write(content);
                continue;
            }
            else
            {

                // Markdown text.
                MarkdownConsole.Write(source);
            }
        }

        return 0;
    }

    public override ValidationResult Validate([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        // HACK!
        // Spectre.Console does not support piped input.
        // We added any piped input here ahead of validation.
        if (PipelineReader.TryReadAll(out var pipeInput))
        {
            settings.MarkdownSources = new[] { pipeInput }
                .Concat(settings.MarkdownSources)
                .ToArray();
        }

        return base.Validate(context, settings);
    }
}
