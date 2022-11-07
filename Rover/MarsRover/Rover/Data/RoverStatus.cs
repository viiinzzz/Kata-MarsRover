using MarsRover.Rover.Parser;
using MarsRover.Models;

namespace MarsRover.Rover.Data;

public record class RoverStatus(Location Location, DirectionEnum Direction)
    : BaseRoverStatus(Location.PositionX, Location.PositionY, Direction)
{
    public static implicit operator RoverStatus((int PositionX, int PositionY, DirectionEnum Direction) status)
        => new(new(status.PositionX, status.PositionY), status.Direction);

    public static implicit operator RoverStatus((Location Location, DirectionEnum Direction) status)
        => new(status.Location, status.Direction);

    public static implicit operator RoverStatus(string status)
        => Parser.Parse(status);

    private static readonly IRoverStatusParser Parser
        = new RoverStatusParser();

    public override string ToString()
        => $"{PositionX} {PositionY} {Direction}";
    

    public RoverStatus DryRun(MoveEnum move, IPositionMaster PositionMaster) => move switch
    {
        MoveEnum.R => TurnRight(),
        MoveEnum.L => TurnLeft(),
        MoveEnum.M => Direction switch
        {
            DirectionEnum.N => MoveNorth(PositionMaster),
            DirectionEnum.S => MoveSouth(PositionMaster),
            DirectionEnum.E => MoveEast(),
            DirectionEnum.W => MoveWest(),
            _ => throw new NotImplementedException()
        },
        _ => throw new NotImplementedException()
    };

    RoverStatus TurnRight() => Turn(true);
    RoverStatus TurnLeft() => Turn(false);
    RoverStatus MoveEast() => MoveX(true);
    RoverStatus MoveWest() => MoveX(false);
    RoverStatus MoveNorth(IPositionMaster PositionMaster) => MoveY(true, PositionMaster);
    RoverStatus MoveSouth(IPositionMaster PositionMaster) => MoveY(false, PositionMaster);

    RoverStatus Turn(bool clockwise) => this with
    {
        Direction = (DirectionEnum)(((int)Direction + (clockwise ? -1 : 1) + 4) % 4)
    };

    RoverStatus MoveX(bool increase) => this with
    {
        PositionX = PositionX + (increase ? 1 : -1)
    };

    RoverStatus MoveY(bool increase, IPositionMaster PositionMaster)

        => Direction == DirectionEnum.N && PositionMaster.IsNorthPole(this) && increase
            ? MoveY_PastPole(true, PositionMaster)

            : Direction == DirectionEnum.S && PositionMaster.IsSouthPole(this) && !increase
                ? MoveY_PastPole(false, PositionMaster)

                : MoveY_NotPastPole(increase);

    RoverStatus MoveY_NotPastPole(bool increase) => this with
    {
        PositionY = PositionY + (increase ? 1 : -1)
    };

    RoverStatus MoveY_PastPole(bool north, IPositionMaster PositionMaster)
    {
        var Width = PositionMaster.Width();

        return this with
        {
            Direction = north ? DirectionEnum.S : DirectionEnum.N,
            PositionX = (PositionX + (Width / 2)) % Width,
        };
    }

}