namespace MarsRover;

public enum Move { L, R, M }

public class Rover
{
    public RoverStatus Status { get; private set; } = new(0, 0, Direction.N);

    public Rover(int positionX, int positionY, Direction direction,
        Action<RoverStatus> ValidatePosition)
    {
        Status = new(positionX, positionY, direction);
        ValidatePosition(Status);
    }


    public Rover(string status,
        Action<RoverStatus> ValidatePosition)
        : this(0, 0, Direction.N, ValidatePosition)
    {
        var next = (RoverStatus)status;
        ValidatePosition(next);
        Status = next;
    }

    public void Run(string moves,
        Action<RoverStatus> ValidatePosition)
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
        Action<RoverStatus> ValidatePosition,
        string debugString = "single move")
    {
        try
        {
            var next = DryRun(move);
            ValidatePosition(next);
            Status = next;
        }
        catch (Exception e)
        {
            throw new BumpException($"{e.Message} -- {debugString}");
        }
    }

    public RoverStatus DryRun(Move move) => move switch
    {
        Move.R => TurnRight(),
        Move.L => TurnLeft(),
        Move.M => Status.Direction switch
        {
            Direction.N => MoveNorth(),
            Direction.S => MoveSouth(),
            Direction.E => MoveEast(),
            Direction.W => MoveWest(),
            _ => throw new NotImplementedException()
        },
        _ => throw new NotImplementedException()
    };

    RoverStatus MoveEast() => MoveX(true);
    RoverStatus MoveWest() => MoveX(false);

    RoverStatus MoveX(bool increase) => Status with
    {
        PositionX = Status.PositionX + (increase ? 1 : -1)
    };

    RoverStatus MoveNorth() => MoveY(true);
    RoverStatus MoveSouth() => MoveY(false);
    RoverStatus MoveY(bool increase) => Status with
    {
        PositionY = Status.PositionY + (increase? 1 : -1),
    };

    RoverStatus TurnRight() => Turn(true);
    RoverStatus TurnLeft() => Turn(false);
    RoverStatus Turn(bool clockwise) => Status with {
        Direction = (Direction)(((int)Status.Direction + (clockwise ? - 1 : 1) + 4) % 4)
    };
}
