using System.Text.RegularExpressions;

namespace MarsRover;

public enum Direction { N, W, S, E }
public enum Move { L, R,  M }

class BumpException : Exception
{
    public BumpException(string message) : base(message) { }
}

class OutsideException : Exception
{
    public OutsideException(string message) : base(message) { }
}

public class Rover
{
    public int PositionX { get; private set; } = 0;
    public int PositionY { get; private set; } = 0;
    public Direction Direction { get; private set; } = Direction.N;

    public Rover(int positionX, int positionY, Direction direction,
        Action<int, int> ValidatePosition)
    {
        PositionX = positionX;
        PositionY = positionY;
        Direction = direction;
        ValidatePosition(PositionX, PositionY);
    }

    private static readonly string AllDirections = Enum.GetValues(typeof(Direction))
        .Cast<Direction>().Select(x => $"{x}")
        .Aggregate((x, y) => x + y);

    private static readonly Regex StatusRx = new Regex(@$"(?<PositionX>[+-]?\d+) (?<PositionY>[+-]?\d+) (?<Direction>[{AllDirections}])");

    public Rover(string status,
        Action<int, int> ValidatePosition)
        : this(0, 0, Direction.N, ValidatePosition)
    {
        var rx = StatusRx.Match(status);
        var parseInt = (string name) => int.Parse(rx.Groups[name].Value);
        var parseDirection = (string name) => Enum.Parse<Direction>(rx.Groups[name].Value);
        (PositionX, PositionY, Direction) = (
            parseInt("PositionX"), parseInt("PositionY"),
            parseDirection("Direction")
        );
        ValidatePosition(PositionX, PositionY);
    }

    public string Status => $"{PositionX} {PositionY} {Direction}";

    public void Run(string moves,
        Action<int, int> ValidatePosition)
    {
        var movesCount = moves.Length;
        var movesList = moves.AsEnumerable()
            .Select((move, index) => (
                move,
                index: index + 1,
                before: moves.Substring(0, index),
                after: moves.Substring(index + 1)
            )).Select(move => (
                move.move,
                debugString: $"MOVE:{move.index}: {move.before}/{move.move}/{move.after}"
                ))
            .ToList();
        movesList.ForEach(
            move => Run(
                Enum.Parse<Move>($"{move.move}"),
                ValidatePosition,
                move.debugString
                )
            );
    }

    public void Run(Move move,
        Action<int, int> ValidatePosition,
        string debugString = "single move")
    {
        var next = DryRun(move);
        try
        {
            ValidatePosition(next.PositionX, next.PositionY);
        }
        catch (Exception e)
        {
            throw new BumpException($"{e.Message} -- {debugString}");
        }
        (PositionX, PositionY, Direction) = next;
    }

    public (int PositionX, int PositionY, Direction Direction) DryRun(Move move) => move switch
    {
        Move.R => Turn(true),
        Move.L => Turn(false),
        Move.M => Direction switch
        {
            Direction.N => MoveY(true),
            Direction.S => MoveY(false),
            Direction.E => MoveX(true),
            Direction.W => MoveX(false),
            _ => throw new NotImplementedException()
        },
        _ => throw new NotImplementedException()
    };

    private (int PositionX, int PositionY, Direction Direction) MoveX(bool right) => (
        PositionX + (right ? 1 : -1),
        PositionY,
        Direction
        );

    private (int PositionX, int PositionY, Direction Direction) MoveY(bool up) => (
        PositionX,
        PositionY + (up ? 1 : -1),
        Direction
        );

    private (int PositionX, int PositionY, Direction Direction) Turn(bool clockwise) => (
        PositionX,
        PositionY,
        (Direction)(((int)Direction + (clockwise ? - 1 : 1) + 4) % 4)
        );
}
