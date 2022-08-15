using System.Diagnostics.CodeAnalysis;

namespace Morello.MarkdownCli;

public static class PipelineReader
{
    public static bool TryReadAll([NotNullWhen(returnValue: true)] out string? pipeInput)
    {
        pipeInput = IsPipelineAvailable()
            ? System.Console.In.ReadToEnd()
            : null;

        return pipeInput is not null;
    }

    private static bool IsPipelineAvailable()
    {
        try
        {
            // This line will throw if there is not piped input.
            _ = System.Console.KeyAvailable;
            return false;
        }
        catch
        {
            return true;
        }
    }
}
