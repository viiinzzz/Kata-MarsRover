using MarsRover.Rover.Data;

namespace MarsRover.Rover.Parser;

public interface IRoverStatusParser
{
    RoverStatus Parse(string status);
}