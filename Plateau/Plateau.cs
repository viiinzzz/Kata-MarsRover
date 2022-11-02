using System.Text.RegularExpressions;

namespace MarsRover;

public record class Plateau(int Width, int Height)
{
    private static readonly Regex SizeRx = new Regex(@"(?<Width>\d+) (?<Height>\d+)");
    
    private List<Rover> Rovers = new();

    public Plateau(string config) : this(0, 0)
    {
        //TODO: parse config
        throw new NotImplementedException();
        var size = "";
            (Width, Height) = (
            int.Parse(SizeRx.Match(size).Groups["Width"].Value),
            int.Parse(SizeRx.Match(size).Groups["Height"].Value)
        );
    }

    public string Result => throw new NotImplementedException();
}
