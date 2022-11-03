using System.Text.RegularExpressions;

namespace MarsRover.Parser;

public class RoverStatusParser: IRoverStatusParser
{
    private static readonly string AllDirections = Enum.GetValues(typeof(Direction))
        .Cast<Direction>().Select(x => $"{x}")
        .Aggregate((x, y) => x + y);

    private static readonly Regex StatusRx =
        new (@$"(?<PositionX>[+-]?\d+) +(?<PositionY>[+-]?\d+) +(?<Direction>[{AllDirections}])");

    private static Func<string, int> ParseInt(Match rx)
        => name => int.Parse(rx.Groups[name].Value);

    private static Func<string, Direction> ParseDirection(Match match)
        => name => Enum.Parse<Direction>(match.Groups[name].Value);

    public RoverStatus Parse(string status)
    {
        var rx = StatusRx.Match(status);
        var parseInt = ParseInt(rx);
        var parseDirection = ParseDirection(rx);

        return (
            parseInt("PositionX"),
            parseInt("PositionY"),
            parseDirection("Direction")
        );
    }
}