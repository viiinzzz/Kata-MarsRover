namespace MarsRover.Models;

public interface IDispatchable
{
    public string Print();
    public string PrintDispatch();
    public IDispatchable Run(MoveEnum move);
    public IDispatchable Run(string moves);
}