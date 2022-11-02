
namespace MarsRover;

public enum Heading
{
    N,
    W,
    S,
    E
}

public enum Action
{
    L,
    R,
    M
}

public record class Rover(int PositionX, int PositionY, Heading Heading, Plateau Plateau)
{
    public string Status => $"{PositionX} {PositionY} {Heading}";
    void doAction(Action Action) => throw new NotImplementedException();
}
