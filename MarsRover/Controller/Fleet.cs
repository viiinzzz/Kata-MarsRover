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

    public IDispatchable AddRover(int PositionX, int PositionY, DirectionEnum Direction)
        => Add(new Rover.RoverUnit(PositionX, PositionY, Direction, PositionMaster, GetRoverId()));

    public IDispatchable AddRover(string status)
        => Add(new Rover.RoverUnit(status, PositionMaster, GetRoverId()));

    public IDispatchable TryAddRover(IDiscardable status, IDiscardable moves)
    {
        Rover.RoverUnit rover = null;

        status.Try(status =>
        {
            rover = new Rover.RoverUnit(status, PositionMaster, GetRoverId());
            if (rover == null) throw new Exception();
        });

        moves.Try(moves =>
        {
            Add(rover.Run(moves));
        });

        return rover;
    }
}