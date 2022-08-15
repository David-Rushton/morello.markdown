using System;
using System.Linq;
using Morello.MarkdownCli.Commands;
using Spectre.Console.Cli;

BootstrapApp().Run(args);


CommandApp<RenderMarkdownCommand> BootstrapApp()
{
    var app = new CommandApp<RenderMarkdownCommand>();
    app.Configure(config =>
    {

        config.SetApplicationName("markdown cli");

        if (IsDebugModeEnabled())
        {
            config
                .PropagateExceptions()
                .ValidateExamples();
        }

        config
            .CaseSensitivity(CaseSensitivity.None)
            .AddExample(new[] { "'Some **markdown** text" });
    });

    return app;
}

bool IsDebugModeEnabled()
{
    var debugArgOptions = new[] { "-d", "--debug" };
    return args.Intersect(debugArgOptions).Any();
}
