using MarsRover.Controller.Data;
using System.Text.RegularExpressions;

namespace MarsRover.Controller.Parser;

public class CornerParser : ICornerParser
{
    private static readonly Regex CornerRx = new Regex(@"(?<MaximumX>[+-]?\d+) +(?<MaximumY>[+-]?\d+)");

    private static Func<string, int> ParseInt(Match rx)
        => (string name) => int.Parse(rx.Groups[name].Value);

    public Plateau Parse(string corner)
    {
        var rx = CornerRx.Match(corner);
        var parseInt = ParseInt(rx);

        return (
            parseInt("MaximumX"),
            parseInt("MaximumY")
        );
    }
}