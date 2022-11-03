namespace MarsRover;

public enum Move { L, R, M }

public record class Rover(IMissionController Controller)
{
    public Rover(int positionX, int positionY, Direction direction, IMissionController Controller) : this(Controller)
        => doNext((positionX, positionY, direction));

    public Rover(string status, IMissionController Controller) : this(Controller)
        => doNext(status);

    private int RoverId = Controller.GetRoverId();

    public RoverStatus Status { get; private set; } = new(0, 0, Direction.N);

    public string StatusString => Status.ToString();

    private List<RoverStatus> History = new();

    public override string ToString() => @$"Rover#{RoverId}:
{(History.Count == 0 ? "" : History.Select((step, index)
        => $"STEP:{index + 1}:{step.ToString()}")
    .Aggregate((x,y) => x + "\n" + y))}";

    private void doNext(RoverStatus next)
        => History.Add(Status = Controller.ValidatePosition(next));


    public void Run(string moves)
        => moves.AsEnumerable()
            .Select((move, index) => (
                move,
                index: index + 1,
                before: moves.Substring(0, index),
                after: moves.Substring(index + 1)
            )).Select(move => (
                move.move,
                debugString: $"MOVE:{move.index}: {move.before}/{move.move}/{move.after}"
            ))
            .ToList()
            .ForEach(move => Run(
                Enum.Parse<Move>($"{move.move}"),
                move.debugString
            ));

    public void Run(Move move, string debugString = "single move")
    {
        try
        {
            doNext(DryRun(move));
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

    RoverStatus MoveNorth() => MoveY(true);
    RoverStatus MoveSouth() => MoveY(false);

    RoverStatus TurnRight() => Turn(true);
    RoverStatus TurnLeft() => Turn(false);

    RoverStatus MoveX(bool increase) => Status with
    {
        PositionX = Status.PositionX + (increase ? 1 : -1)
    };

    RoverStatus MoveY(bool increase) => Status with
    {
        PositionY = Status.PositionY + (increase ? 1 : -1)
    };

    RoverStatus Turn(bool clockwise) => Status with
    {
        Direction = (Direction)(((int)Status.Direction + (clockwise ? - 1 : 1) + 4) % 4)
    };
}
