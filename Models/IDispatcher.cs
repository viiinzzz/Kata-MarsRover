namespace MarsRover.Models;

public interface IDispatcher
{
    public string PrintDispatch();
    public IDispatchable AddRover(int PositionX, int PositionY, DirectionEnum Direction);
    public IDispatchable AddRover(string status);
    public IDispatchable TryAddRover(IDiscardable status, IDiscardable moves);
}