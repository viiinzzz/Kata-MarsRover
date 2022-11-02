using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRover;

public enum Direction { N, W, S, E }
public enum Move { L, R,  M }

public record class Rover(Plateau Plateau)
{
    public int PositionX { get; private set; } = 0;
    public int PositionY { get; private set; } = 0;
    public Direction Direction { get; private set; } = Direction.N;

    public Rover(int positionX, int positionY, Direction direction, Plateau plateau)
        : this(plateau) {
        PositionX = positionX;
        PositionY = positionY;
        Direction = direction;
    }

    private static readonly string AllHeadings = Enum.GetValues(typeof(Direction))
        .Cast<Direction>().Select(x => $"{x}")
        .Aggregate((x, y) => x + y);

    private static readonly Regex StatusRx = new Regex(@$"(?<PositionX>\d+) (?<PositionY>\d+) (?<Direction>[{AllHeadings}])");

    public Rover(string status, Plateau plateau)
        : this(0, 0, Direction.N, plateau)
        => (PositionX, PositionY, Direction) = (
            int.Parse(StatusRx.Match(status).Groups["PositionX"].Value),
            int.Parse(StatusRx.Match(status).Groups["PositionY"].Value),
            Enum.Parse<Direction>(StatusRx.Match(status).Groups["Direction"].Value)
        );

    public string Status => $"{PositionX} {PositionY} {Direction}";

    public void Move(Move move)
    {
        (PositionX, PositionY, Direction) = move switch
        {
            MarsRover.Move.L => (PositionX, PositionY, (Direction)(((int)Direction + 1) % 4)),
            MarsRover.Move.R => (PositionX, PositionY, turn(true)),
            MarsRover.Move.M => (PositionX, PositionY, Direction), //we don't know yet how to move forward
            _ => throw new NotImplementedException()
        };
    }

    private Direction turn(bool clockwise)
    {
        return (Direction)(((int)Direction + (clockwise ? - 1 : 1) + 4) % 4);
    }
}
