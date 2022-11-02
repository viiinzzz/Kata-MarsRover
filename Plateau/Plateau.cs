using System.Text.RegularExpressions;

namespace MarsRover;

public record class Plateau(int Width, int Height)
{
    private static readonly Regex SizeRx = new Regex(@"(?<Width>\d+) (?<Height>\d+)");
    
    private List<Rover> Rovers = new();

    public Plateau(string config) : this(0, 0)
    {
        var lines = config.Split(new []{ '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .ToList();
        var size = lines.First();
        lines.RemoveAt(0);
        (Width, Height) = (
            int.Parse(SizeRx.Match(size).Groups["Width"].Value),
            int.Parse(SizeRx.Match(size).Groups["Height"].Value)
        );
        //TODO: parse fleet
        while (lines.Count > 0)
        {
            var status = lines.First();
            lines.RemoveAt(0);
            var move = lines.First();
            lines.RemoveAt(0);
            var rover = new Rover(status, this);
            Rovers.Add(rover);
        }
    }

    public string Result => throw new NotImplementedException();
}
