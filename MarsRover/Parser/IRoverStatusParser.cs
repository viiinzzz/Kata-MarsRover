namespace MarsRover.Parser;

public interface IRoverStatusParser
{
    RoverStatus Parse(string status);
}