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
            .Where(line => line.Length > 0);
        var size = lines.Take(1).First();
        (Width, Height) = (
            int.Parse(SizeRx.Match(size).Groups["Width"].Value),
            int.Parse(SizeRx.Match(size).Groups["Height"].Value)
        );
        //TODO: parse fleet
        while (lines.Count() > 0)
        {
            var roverConfig = lines.Take(2);
            var status = roverConfig.Take(1).First();
            var moves = roverConfig.Take(1).First();
            var rover = new Rover(status, this);
            Rovers.Add(rover);
            rover.Do(moves);
        }
    }

    public string Result => Rovers.Count == 0 ? ""
        : Rovers.Select(rover => rover.Status).Aggregate((x, y) => x + '\n' + y);
}
