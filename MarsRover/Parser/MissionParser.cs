
namespace MarsRover.Parser;

public class MissionParser
{
    private IEnumerable<(string line, int index)> lines;
    private (string line, int index)[] took = { ("", 0) };

    public MissionParser(string config)
        => lines = config.splitLines()
            .Select((line, index) => (line, index + 1));

    const int debugLines_Last = int.MaxValue;
    const int debugLines_All = -1;

    public void Try(
        string purpose, Action action,
        Func<Exception, string> errorMessage, int index
    ) {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            string debugLines = "";
            if (index == debugLines_Last)
            {
                var last = took.Last();
                debugLines = $"LINE:{last.index}: {last.line}";
            }
            else if (index == debugLines_All)
            {
                debugLines = took
                    .Select(line => $"LINE:{line.index}: {line.line}")
                    .Aggregate((x, y) => x + " -- " + y);
            }
            else
            {
                var line = took[index];
                debugLines = $"LINE:{line.index}: {line.line}";
            }

            throw new Exception(@$"parse error: {purpose} {errorMessage(exception)} -- {debugLines}.");
        }
    }

    public void TryTakeLines(
        string purpose,
        int count,
        Action<string[]> parseLines
    ) {
        if (count <= 0) return;

        Try(purpose, () =>
        {
            took = lines.Take(count).ToArray();
            lines = lines.Skip(count);
        }, exception => $"expected {count} more line{(count > 1 ? "s" : "")} after", debugLines_Last);

        Try(purpose, () =>
        {
            var data = took.Select(line => line.line).ToArray();
            parseLines(data);
        }, exception => "invalid data", debugLines_All);
    }

    public void TryTakeOneLine(string purpose, Action<string> parseLine)
        => TryTakeLines(purpose, 1, data => parseLine(data[0]));

    public bool CanTake() => lines.Any();
}
