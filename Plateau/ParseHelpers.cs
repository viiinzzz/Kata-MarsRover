namespace MarsRover;

public static class ParseHelpers
{
    public static string trimLines(this string lines)
        => joinLines(lines.Split(new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0));

    static string joinLines(IEnumerable<string> lines)
        => lines.Count() == 0 ? ""
            : lines.Aggregate((x, y) => x + '\n' + y);
}