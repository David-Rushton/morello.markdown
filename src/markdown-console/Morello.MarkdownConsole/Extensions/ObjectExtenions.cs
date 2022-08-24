using System;

namespace Morello.Markdown.Console.Extensions;

public static class ObjectExtensions
{
    public static string ToNotNullString(this object obj) =>
        obj.ToString() ?? string.Empty;
}
