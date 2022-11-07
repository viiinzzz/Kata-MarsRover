using MarsRover.Rover.Data;
using MarsRover.Models;

namespace MarsRover.Rover;

public record class RoverUnit(IPositionMaster PositionMaster, int RoverId) : IDispatchable
{
    // [Obsolete("deprecated, please use RoverUnit(string status, IPositionMaster PositionMaster, int RoverId) instead.")]
    // protected RoverUnit(int positionX, int positionY, DirectionEnum direction,
    //     IPositionMaster PositionMaster, int RoverId) : this(PositionMaster, RoverId)
    //     => doNext((positionX, positionY, direction));

    public RoverUnit(string status,
        IPositionMaster PositionMaster, int RoverId) : this(PositionMaster, RoverId)
        => doNext(status);

    public RoverUnit SetStatus(string status)
    {
        doNext(status);
        return this;
    }

    private RoverStatus Status = new(new(0, 0), DirectionEnum.N);

    public string PrintDispatch() => Status.ToString();

    private List<RoverStatus> History = new();

    public override string ToString() => Print();
    public string Print() => @$"Rover#{RoverId}:
{(History.Count == 0 ? "" : History.Select((step, index)
        => $"STEP:{index + 1}:{step.ToString()}")
    .Aggregate((x, y) => x + "\n" + y))}";

    private void doNext(RoverStatus next)
        => History.Add(Status = (RoverStatus)PositionMaster.ValidatePosition(next));


    public IDispatchable Run(string moves)
    {
        moves.AsEnumerable()
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
                Enum.Parse<MoveEnum>($"{move.move}"),
                move.debugString
            ));
        return this;
    }

    public IDispatchable Run(MoveEnum move) => Run(move, "single move");
    public IDispatchable Run(MoveEnum move, string debugString)
    {
        try
        {
            doNext(Status.DryRun(move, PositionMaster));
            return this;
        }
        catch (Exception e)
        {
            throw new BumpException($"{e.Message} -- {debugString}");
        }
    }

}
