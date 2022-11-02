using System.Text.RegularExpressions;

namespace MarsRover;

public record class Plateau(int width, int height)
{
    static Regex _sizeRx = new Regex(@"(?<width>\d+) (?<height>\d+)");

    public Plateau(string size)
        : this(0, 0)
        => (width, height) = (
            int.Parse(_sizeRx.Match(size).Groups["width"].Value),
            int.Parse(_sizeRx.Match(size).Groups["height"].Value)
        );
}
