using System.Text.RegularExpressions;

namespace MarsRover;

public record class Plateau(int Width, int Height)
{
    private static readonly Regex SizeRx = new Regex(@"(?<Width>\d+) (?<Height>\d+)");

    public Plateau(string size)
        : this(0, 0)
        => (Width, Height) = (
            int.Parse(SizeRx.Match(size).Groups["Width"].Value),
            int.Parse(SizeRx.Match(size).Groups["Height"].Value)
        );

    public string Result => throw new NotImplementedException();
}
