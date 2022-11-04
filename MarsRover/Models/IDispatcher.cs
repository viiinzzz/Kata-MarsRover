namespace MarsRover.Models;

public interface IDispatcher
{
    public string PrintRovers();
    public Rover.RoverUnit AddRover(int PositionX, int PositionY, DirectionEnum Direction);
    public Rover.RoverUnit AddRover(string status);
    public Rover.RoverUnit TryAddRover(IDiscardable status, IDiscardable moves);
}