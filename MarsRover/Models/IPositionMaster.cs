namespace MarsRover.Models;

public interface IPositionMaster
{
    BaseRoverStatus ValidatePosition(BaseRoverStatus status);
    public int Width();
    public int Height();
}