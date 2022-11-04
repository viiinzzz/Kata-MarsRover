namespace MarsRover.Models;

public interface IDiscardable
{
    void Try(Action<string> action);
}