namespace MarsRover.Models;

public interface IMissionController
{
    int GetRoverId();
    RoverStatus ValidatePosition(RoverStatus status);
}