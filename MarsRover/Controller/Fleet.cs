using MarsRover.Models;

namespace MarsRover.Controller;

public record class Fleet(IPositionMaster PositionMaster)
    : IRoverIdentifier, IDispatcher
{
    private readonly List<IDispatchable> Rovers = new();

    private IDispatchable Add(IDispatchable rover)
    {
        Rovers.Add(rover);
        return rover;
    }

    private int RoverId = 0;

    public int GetRoverId() => RoverId++;

    public string PrintDispatch() => Rovers.Count == 0 ? ""
        : Rovers.Select(rover => $"{rover.PrintDispatch()}")
            .Aggregate((x, y) => x + '\n' + y);

    public override string ToString() => PrintDispatch();

    [Obsolete(@"AddRover(int PositionX, int PositionY, DirectionEnum Direction) is deprecated,
please use AddRover(string status) instead.")]
    public IDispatchable AddRover(int PositionX, int PositionY, DirectionEnum Direction)
        => Add(new Rover.RoverUnit(PositionX, PositionY, Direction, PositionMaster, GetRoverId()));


    IDispatchable taskNewRoverWithStatus(string status)
        => new Rover.RoverUnit(status, PositionMaster, GetRoverId());

    public IDispatchable AddRover(string status)
        => Add(taskNewRoverWithStatus(status));

    public IDispatchable TryAddRover(IDiscardable status, IDiscardable moves)
    {
        var rover = status.Try(status =>
        {
            var r = taskNewRoverWithStatus(status);
            if (r == null) throw new Exception();
            return r;
        });

        moves.Try(moves =>
        {
            Add(rover.Run(moves));
        });

        return rover;
    }
}