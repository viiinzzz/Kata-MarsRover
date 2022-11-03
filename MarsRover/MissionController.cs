using Kata_MarsRover.Parser;

namespace MarsRover;

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

    public string Result => Rovers.Count == 0
        ? ""
        : Rovers.Select(rover => $"{rover.Status}")
            .Aggregate((x, y) => x + '\n' + y);

    public Rover AddRover(int PositionX, int PositionY, Direction Direction)
        => AddRover(new Rover(PositionX, PositionY, Direction, this));

    public Rover AddRover(string status)
        => AddRover(new Rover(status, this));

    private Rover AddRover(Rover rover)
    {
        Rovers.Add(rover);
        return rover;
    }

    public MissionController(int MaximumX, int MaximumY)
        => Plateau = new(MaximumX, MaximumY);

    /*
    private void ParseConfig(string config)
    {
        var Parser = new LinesParser(config);

        Parser.TryTakeOneLine("plateau corner",
            corner => Plateau = corner);

        while (Parser.CanTake())
        {
            string status = "", moves = "";
            Parser.TryTakeLines("rover configuration", 2,
                roverConfig =>
                {
                    status = roverConfig[0];
                    moves = roverConfig[1];
                });

            Rover rover = null;
            Parser.Try("rover status",
                () => { rover = new Rover(status, this); },
                exception => "invalid data", 0);

            Parser.Try("rover moves",
                () =>
                {
                    rover.Run(moves);
                    Rovers.Add(rover);
                },
                exception => exception is BumpException ? exception.Message : "invalid data", 1);
        }

        if (Parser.CanTake())
        {
            Parser.TryTakeOneLine("rover configuration",
                line => throw new Exception());
        }
    }
    */

    public MissionController(string config)
        //=> ParseConfig(config);
    {
        var Mission = MissionParser.Parse(config);

        this.Plateau = Mission.cornerDefinition;

        Mission.RoverDefinitions.ForEach(roverDefinition =>
        {
            Rover rover = null;
            roverDefinition.status.Try(status =>
            {
                rover = new Rover(status, this);
                if (rover == null) throw new Exception();
            });
            roverDefinition.moves.Try(moves =>
            {
                Rovers.Add(rover.Run(moves));
            });
        });
    }

}