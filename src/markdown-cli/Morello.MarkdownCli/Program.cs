using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Morello.MarkdownCli;
using Morello.MarkdownCli.Commands;
using Spectre.Console.Cli;

string[] defaultArg = new[] { "--help" };

BootstrapApp().Run(GetArgsOrDefault());

CommandApp<RenderMarkdownCommand> BootstrapApp()
{
    var app = new CommandApp<RenderMarkdownCommand>();
    app.Configure(config =>
    {
        config
            .SetApplicationName("md-cli")
            .SetApplicationVersion(GetVersion());

        if (IsDebugModeEnabled())
        {
            config
                .PropagateExceptions()
                .ValidateExamples();
        }

        config
            .CaseSensitivity(CaseSensitivity.None)
            .UseStrictParsing();

        config.AddExample(new[] { "'Some **markdown** text'" });
        config.AddExample(new[] { "/path/to/markdown/file.md" });
        config.AddExample(new[] { "/path/to/markdown/file.md", "'Some **markdown** text'" });
    });

    return app;
}

IEnumerable<string> GetArgsOrDefault()
{
    if (PipelineReader.TryReadAll(out var pipeInput))
    {
        return args.Concat(new[] { pipeInput });
    }

    return args.Length == 0
        ? defaultArg
        : args;
}

string GetVersion() =>
    Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "v0.0.0";

bool IsDebugModeEnabled()
{
    var debugArgOptions = new[] { "-d", "--debug" };
    return args.Intersect(debugArgOptions).Any();
}
