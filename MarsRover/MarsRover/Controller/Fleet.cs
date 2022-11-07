using MarsRover.Models;

namespace MarsRover.Controller;

public record class Fleet(int UnitCount, IPositionMaster PositionMaster)
    : IDispatcher//, IRoverIdentifier
{
    private readonly List<Rover.RoverUnit> RoversStock =
        Enumerable.Range(0, UnitCount)
            .Select(RoverId => new Rover.RoverUnit(PositionMaster, RoverId))
            .ToList();

    IDispatchable UnloadRover(string status)
        // => new Rover.RoverUnit(status, PositionMaster, GetRoverId());
    {
        if (RoversStock.Count == 0)
            throw new Exception("rover stock depleted");

        var rover = RoversStock[0];
        RoversStock.RemoveAt(0);

        rover.SetStatus(status);

        return rover;
    }


    private readonly List<IDispatchable> RoversActive = new();


    // private int RoverId = 0;

    // public int GetRoverId() => RoverId++;

    private IDispatchable Activate(IDispatchable rover)
    {
        RoversActive.Add(rover);
        return rover;
    }

    public override string ToString() => PrintDispatch();

    // [Obsolete("deprecated, please use AddRover(string status) instead.")]
    // protected IDispatchable AddRover(int PositionX, int PositionY, DirectionEnum Direction)
    //     => Activate(new Rover.RoverUnit(PositionX, PositionY, Direction, PositionMaster, GetRoverId()));



    public IDispatchable AddRover(string status)
        => Activate(UnloadRover(status));

    public IDispatchable TryAddRover(IDiscardable status, IDiscardable moves)
    {
        var rover = status.Try(status =>
        {
            var r = UnloadRover(status);
            if (r == null) throw new Exception();
            return r;
        });

        moves.Try(moves =>
        {
            Activate(rover.Run(moves));
        });

        return rover;
    }


    public string PrintDispatch() => RoversActive.Count == 0 ? ""
        : RoversActive.Select(rover => $"{rover.PrintDispatch()}")
            .Aggregate((x, y) => x + '\n' + y);

}