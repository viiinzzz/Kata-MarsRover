using MarsRover.Models;
using MarsRover.Rover.Parser;

namespace MarsRover.Rover.Data;

public record class Location(int PositionX, int PositionY);

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
}