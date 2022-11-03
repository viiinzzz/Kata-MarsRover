using System.Text.RegularExpressions;

namespace MarsRover;

public enum Direction { N, W, S, E }


public record class RoverStatus(int PositionX, int PositionY, Direction Direction)
{
    public override string ToString() => $"{PositionX} {PositionY} {Direction}";

    public static implicit operator RoverStatus((int PositionX, int PositionY, Direction Direction) status)
        => new RoverStatus(status.PositionX, status.PositionY, status.Direction);

    public static implicit operator RoverStatus(string status)
    {
        var rx = StatusRx.Match(status);
        var parseInt = (string name) => int.Parse(rx.Groups[name].Value);
        var parseDirection = (string name) => Enum.Parse<Direction>(rx.Groups[name].Value);
        
        return (
            parseInt("PositionX"),
            parseInt("PositionY"),
            parseDirection("Direction")
        );
    }

    private static readonly string AllDirections = Enum.GetValues(typeof(Direction))
        .Cast<Direction>().Select(x => $"{x}")
        .Aggregate((x, y) => x + y);

    private static readonly Regex StatusRx =
        new Regex(@$"(?<PositionX>[+-]?\d+) (?<PositionY>[+-]?\d+) (?<Direction>[{AllDirections}])");
}