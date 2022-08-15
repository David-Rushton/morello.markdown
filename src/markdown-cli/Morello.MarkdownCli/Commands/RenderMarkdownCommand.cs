using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
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
        [Description("Markdown or path to markdown file")]
        [CommandArgument(position: 0, "[markdown|path]")]
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

        if (settings.MarkdownSources.Length == 0)
        {
            return ValidationResult.Error("Markdown is required");
        }

        return base.Validate(context, settings);
    }
}
