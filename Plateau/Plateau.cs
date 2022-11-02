using System.Text.RegularExpressions;

namespace MarsRover;

public record class Plateau(int width, int height)
{
    static Regex sizeRx = new Regex(@"(?<width>\d+) (?<height>\d+)");

    public Plateau(string size)
        : this(0, 0)
        => (width, height) = (
            width: int.Parse(sizeRx.Match(size).Groups["width"].Value),
            height: int.Parse(sizeRx.Match(size).Groups["height"].Value)
        );
}
