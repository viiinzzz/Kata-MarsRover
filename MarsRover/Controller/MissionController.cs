using MarsRover.Controller.Parser;
using MarsRover.Models;

namespace MarsRover.Controller;

public class MissionController : IMissionController
{
    public Plateau Plateau { get; }= new(1, 1);

    public RoverStatus ValidatePosition(RoverStatus status)
        => Plateau.ValidatePosition(status);


    private readonly List<Rover.RoverUnit> Rovers = new();
    private int RoverId = 0;
    public int GetRoverId() => RoverId++;

    public string Result => Rovers.Count == 0
        ? ""
        : Rovers.Select(rover => $"{rover.Status}")
            .Aggregate((x, y) => x + '\n' + y);

    public Rover.RoverUnit AddRover(int PositionX, int PositionY, Direction Direction)
        => AddRover(new Rover.RoverUnit(PositionX, PositionY, Direction, this));

    public Rover.RoverUnit AddRover(string status)
        => AddRover(new Rover.RoverUnit(status, this));

    private Rover.RoverUnit AddRover(Rover.RoverUnit rover)
    {
        Rovers.Add(rover);
        return rover;
    }

    public MissionController(int MaximumX, int MaximumY)
        => Plateau = new(MaximumX, MaximumY);

    public MissionController(string config)
    {
        var Mission = MissionParser.Parse(config);

        Plateau = Mission.cornerDefinition;

        Mission.RoverDefinitions.ForEach(roverDefinition =>
        {
            Rover.RoverUnit rover = null;
            roverDefinition.status.Try(status =>
            {
                rover = new Rover.RoverUnit(status, this);
                if (rover == null) throw new Exception();
            });
            roverDefinition.moves.Try(moves =>
            {
                Rovers.Add(rover.Run(moves));
            });
        });
    }

}