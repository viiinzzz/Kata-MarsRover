using MarsRover.Rover.Data;
using MarsRover.Models;

namespace MarsRover.Rover;

public record class RoverUnit(IPositionMaster PositionMaster, int RoverId) : IDispatchable
{
    public RoverUnit(int positionX, int positionY, DirectionEnum direction,
        IPositionMaster PositionMaster, int RoverId) : this(PositionMaster, RoverId)
        => doNext((positionX, positionY, direction));

    public RoverUnit(string status,
        IPositionMaster PositionMaster, int RoverId) : this(PositionMaster, RoverId)
        => doNext(status);

    public RoverStatus Status { get; private set; } = new(0, 0, DirectionEnum.N);

    public string StatusString => Status.ToString();

    private List<RoverStatus> History = new();

    public override string ToString() => @$"Rover#{RoverId}:
{(History.Count == 0 ? "" : History.Select((step, index)
        => $"STEP:{index + 1}:{step.ToString()}")
    .Aggregate((x, y) => x + "\n" + y))}";

    private void doNext(RoverStatus next)
        => History.Add(Status = (RoverStatus)PositionMaster.ValidatePosition(next));


    public RoverUnit Run(string moves)
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

    public RoverUnit Run(MoveEnum move, string debugString = "single move")
    {
        try
        {
            doNext(DryRun(move));
            return this;
        }
        catch (Exception e)
        {
            throw new BumpException($"{e.Message} -- {debugString}");
        }
    }

    public RoverStatus DryRun(MoveEnum move) => move switch
    {
        MoveEnum.R => TurnRight(),
        MoveEnum.L => TurnLeft(),
        MoveEnum.M => Status.Direction switch
        {
            DirectionEnum.N => MoveNorth(),
            DirectionEnum.S => MoveSouth(),
            DirectionEnum.E => MoveEast(),
            DirectionEnum.W => MoveWest(),
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
        Direction = (DirectionEnum)(((int)Status.Direction + (clockwise ? -1 : 1) + 4) % 4)
    };
}