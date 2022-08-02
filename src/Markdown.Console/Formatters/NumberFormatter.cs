namespace Markdown.Console.Formatters;

public class NumberFormatter
{
    public string Format(int number)
    {
        // TODO: There is no fallback if the users font does not support nerd fonts.
        // Detecting support could be very hard.
        // Nerd font may need to be a opt in/out feature.
        return number.ToString()
            .Replace("0", "")
            .Replace("1", "")
            .Replace("2", "")
            .Replace("3", "")
            .Replace("4", "")
            .Replace("5", "")
            .Replace("6", "")
            .Replace("7", "")
            .Replace("8", "")
            .Replace("9", "");
    }
}
