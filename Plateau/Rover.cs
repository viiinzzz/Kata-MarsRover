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

    public void Move(Move move) => (PositionX, PositionY, Direction) = move switch {
        MarsRover.Move.L => (PositionX, PositionY, Turn(false)),
        MarsRover.Move.R => (PositionX, PositionY, Turn(true)),
        MarsRover.Move.M => Direction switch {
            Direction.N => MoveY(true),
            Direction.W => (PositionX - 1, PositionY, Direction),
            Direction.S => (PositionX, PositionY - 1, Direction),
            Direction.E => MoveX(true),
            _ => throw new NotImplementedException()
        },
        _ => throw new NotImplementedException()
    };

    private (int PositionX, int PositionY, Direction Direction) MoveX(bool positive)
        => (PositionX + (positive ? 1 : -1), PositionY, Direction);

    private (int PositionX, int PositionY, Direction Direction) MoveY(bool positive)
        => (PositionX, PositionY + (positive ? 1 : -1), Direction);

    private Direction Turn(bool clockwise)
        => (Direction)(((int)Direction + (clockwise ? - 1 : 1) + 4) % 4);
}
