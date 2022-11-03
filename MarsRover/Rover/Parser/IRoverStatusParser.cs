using MarsRover.Models;

namespace MarsRover.Rover.Parser;

public interface IRoverStatusParser
{
    RoverStatus Parse(string status);
}