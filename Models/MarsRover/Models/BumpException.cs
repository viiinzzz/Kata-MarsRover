namespace MarsRover.Models;

public class BumpException : Exception
{
    public BumpException(string message) : base(message) { }
}