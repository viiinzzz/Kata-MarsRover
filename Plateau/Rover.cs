using System.Linq;
using System.Text.RegularExpressions;

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
    private static string _allHeadings = Enum.GetValues(typeof(Heading))
        .Cast<Heading>().Select(x => $"{x}")
        .Aggregate((x, y) => x + y);

    private static Regex _statusRx = new Regex(@$"(?<PositionX>\d+) (?<PositionY>\d+) (?<Heading>[{_allHeadings}])");

    public Rover(string status, Plateau Plateau)
        : this(0, 0, Heading.N, Plateau)
        => (PositionX, PositionY, Heading) = (
            int.Parse(_statusRx.Match(status).Groups["PositionX"].Value),
            int.Parse(_statusRx.Match(status).Groups["PositionY"].Value),
            Enum.Parse<Heading>(_statusRx.Match(status).Groups["Heading"].Value)
        );

    public string Status => $"{PositionX} {PositionY} {Heading}";
    void doAction(Action Action) => throw new NotImplementedException();
}
