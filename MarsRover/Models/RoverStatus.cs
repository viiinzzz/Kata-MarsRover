using MarsRover.Rover.Parser;
using MarsRover.Rover;

namespace MarsRover.Models;

public record class RoverStatus(int PositionX, int PositionY, Direction Direction)
{
    public static implicit operator RoverStatus((int PositionX, int PositionY, Direction Direction) status)
        => new (status.PositionX, status.PositionY, status.Direction);

    public static implicit operator RoverStatus(string status)
        => Parser.Parse(status);

    private static readonly IRoverStatusParser Parser
        = new RoverStatusParser();

    public override string ToString()
        => $"{PositionX} {PositionY} {Direction}";
}