namespace MarsRover;

public interface IMissionController
{
    int GetRoverId();
    RoverStatus ValidatePosition(RoverStatus status);
}