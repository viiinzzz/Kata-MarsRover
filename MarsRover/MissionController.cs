using MarsRover.Parser;

namespace MarsRover;

public interface IMissionController
{
    int GetRoverId();
    RoverStatus ValidatePosition(RoverStatus status);
}

public class MissionController : IMissionController
{
    private Plateau Plateau = new(1, 1);

    public int Width => Plateau.Width;
    public int Height => Plateau.Height;

    public RoverStatus ValidatePosition(RoverStatus status)
        => Plateau.ValidatePosition(status);


    private readonly List<Rover> Rovers = new();
    private int RoverId = 0;
    public int GetRoverId() => RoverId++;

    public string Result => Rovers.Count == 0 ? ""
        : Rovers.Select(rover => $"{rover.Status}")
            .Aggregate((x, y) => x + '\n' + y);

    public Rover AddRover(int PositionX, int PositionY, Direction Direction)
    {
        var rover = new Rover(PositionX, PositionY, Direction, this);
        Rovers.Add(rover);
        return rover;
    }

    public Rover AddRover(string status)
    {
        var rover = new Rover(status, this);
        Rovers.Add(rover);
        return rover;
    }


    public MissionController(int MaximumX, int MaximumY)
        => Plateau = new(MaximumX, MaximumY);

    public MissionController(string config)
    {
        ParseConfig(config);
    }

    private void ParseConfig(string config)
    {

        // parsing helpers
        var Parser = new MissionParser(config);

        // effectively parse

        Parser.TryTakeOneLine("plateau corner", corner => Plateau = corner);

        while (Parser.CanTake())
        {
            string status = "", moves = "";
            Parser.TryTakeLines("rover configuration", 2, roverConfig =>
            {
                status = roverConfig[0];
                moves = roverConfig[1];
            });

            Rover rover = null;
            Parser.Try("rover status", () => { rover = new Rover(status, this); }, exception => "invalid data", 0);

            Parser.Try("rover moves", () =>
            {
                rover.Run(moves);
                Rovers.Add(rover);
            }, exception => exception is BumpException ? exception.Message : "invalid data", 1);
        }

        if (Parser.CanTake())
        {
            Parser.TryTakeOneLine("rover configuration", line => { throw new Exception(); });
        }
    }
}
