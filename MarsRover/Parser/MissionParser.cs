using MarsRover;
using MarsRover.Parser;

namespace Kata_MarsRover.Parser;

public static class MissionParser
{
    public static Mission Parse(string config)
    {
        Mission Mission = new();
        LinesParser Parser = new(config);

        Parser.TryTakeOneLine("plateau corner",
            corner => Mission.cornerDefinition = corner);

        while (Parser.CanTake())
        {
            var RoverDefinition = Parser.TryTakeLinesAsDiscardables("rover configuration",
                new (string purpose, Func<Exception, string> exceptionMessage, int index)[] {
                    (
                        purpose: "rover status",
                        exceptionMessage: exception => "invalid data",
                        index: 0),
                    (
                        purpose: "rover moves",
                        exceptionMessage: exception => exception is BumpException ? exception.Message : "invalid data",
                        index: 1)
                });
            var status = RoverDefinition[0];
            var moves = RoverDefinition[1];
            Mission.RoverDefinitions.Add((status, moves));
        }

        if (Parser.CanTake())
            Parser.TryTakeOneLine("incomplete rover configuration",
                line => throw new Exception());

        return Mission;
    }
}