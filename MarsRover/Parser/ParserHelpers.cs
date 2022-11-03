namespace MarsRover.Parser;

public static class ParserHelpers
{
    public static string trimLines(this string lines)
        => joinLines(splitLines(lines));

    public static IEnumerable<string> splitLines(this string lines)
        => lines.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0);

    public static string joinLines(IEnumerable<string> lines)
        => lines.Count() == 0 ? ""
            : lines.Aggregate((x, y) => x + '\n' + y);
}