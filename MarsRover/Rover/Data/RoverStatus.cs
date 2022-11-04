using MarsRover.Models;
using MarsRover.Rover.Parser;

namespace MarsRover.Rover.Data;

public record class RoverStatus(int PositionX, int PositionY, DirectionEnum Direction)
    : BaseRoverStatus(PositionX, PositionY, Direction)
{
    public static implicit operator RoverStatus((int PositionX, int PositionY, DirectionEnum Direction) status)
        => new(status.PositionX, status.PositionY, status.Direction);

    public static implicit operator RoverStatus(string status)
        => Parser.Parse(status);

    private static readonly IRoverStatusParser Parser
        = new RoverStatusParser();

    public override string ToString()
        => $"{PositionX} {PositionY} {Direction}";
}