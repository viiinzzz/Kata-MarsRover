namespace MarsRover.Models;

public interface IDiscardable
{
    void Try(Action<string> action);
    T Try<T>(Func<string, T> action);
}