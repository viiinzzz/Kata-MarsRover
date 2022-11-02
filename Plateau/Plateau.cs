namespace MarsRover;

public record class Plateau(int width, int height)
{
    public int StartX { get; set; } = 0;
    public int StartY { get; set; } = 0;
}
