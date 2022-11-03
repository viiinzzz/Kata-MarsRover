using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace MarsRover;

public class Plateau
{
    private static readonly Regex SizeRx = new Regex(@"(?<Width>\d+) (?<Height>\d+)");

    public int Width { get; private set; } = 0;
    public int Height { get; private set; } = 0;
    private List<Rover> Rovers = new();

    public Plateau(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Plateau(string config)
    {
        var lines = config.Split(new []{ '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .Select((line, index) => (line, index + 1));

        // parsing helpers

        (string line, int index)[] took = new []{("", 0)};

        const int debugLines_Last = int.MaxValue;
        const int debugLines_All = -1;

        var Try = (
            string purpose, Action action,
            Func<Exception, string> errorMessage, int index
            ) => {
            try { action(); } catch (Exception exception)
            {
                string debugLines = "";
                if (index == debugLines_Last)
                {
                    var last = took.Last();
                    debugLines = $"LINE:{last.index} {last.line}";
                }
                else if (index == debugLines_All)
                {
                    debugLines = took.Select(line => $"LINE:{line.index} {line.line}")
                        .Aggregate((x, y) => x + " -- " + y);
                }
                else
                {
                    var line = took[index];
                    debugLines = $"LINE:{line.index} {line.line}";
                }
                throw new Exception(@$"parse error: {purpose} {errorMessage(exception)} -- {debugLines}.");
            }
        };

        var TryTakeLines = (
            string purpose,
            int count,
            Action<string[]> parseLines
            ) =>
        {
            if (count <= 0) return;

            Try(purpose, () => {
                took = lines.Take(count).ToArray();
                lines = lines.Skip(count);
            }, exception => $"expected {count} more line{(count > 1 ? "s" : "")} after", debugLines_Last);

            Try(purpose, () => {
                var data = took.Select(line => line.line).ToArray();
                parseLines(data);
            }, exception => "invalid data", debugLines_All);
        };

        var TryTakeOneLine = (string purpose, Action<string> parseLine)
            => TryTakeLines(purpose, 1, data => parseLine(data[0]));

        var CanTake = () => lines.Any();

        // effectively parse

        TryTakeOneLine("plateau size", size =>
        {
            var rx = SizeRx.Match(size);
            var parseInt = (string name) => int.Parse(rx.Groups[name].Value);
            (Width, Height) = (parseInt("Width"), parseInt("Height"));
        });

        while (CanTake())
        {
            string status = "", moves = "";
            TryTakeLines("rover configuration", 2, roverConfig => {
                status = roverConfig[0];
                moves = roverConfig[1];
            });

            Rover rover = null;
            Try("rover status", () => {
                rover = new Rover(status, this);
            }, exception => "invalid data", 0);

            Try("rover moves", () => {
                rover.Do(moves);
                Rovers.Add(rover);
            }, exception => "invalid data", 1);
        }

        if (CanTake())
        {
            TryTakeOneLine("rover configuration", line => { throw new Exception(); });
        }
    }

    public string Result => Rovers.Count == 0 ? ""
        : Rovers.Select(rover => rover.Status).Aggregate((x, y) => x + '\n' + y);
}
