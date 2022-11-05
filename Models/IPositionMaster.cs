namespace MarsRover.Models;

public interface IPositionMaster
{
    BaseRoverStatus ValidatePosition(BaseRoverStatus status);
    public int Width();
    public int Height();

    public bool IsNorthPole(BaseRoverStatus status);
    public bool IsSouthPole(BaseRoverStatus status);

}