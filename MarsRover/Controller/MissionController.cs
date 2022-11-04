using MarsRover.Controller.Data;
using MarsRover.Controller.Parser;
using MarsRover.Models;

namespace MarsRover.Controller;

public class MissionController
{
    public IPositionMaster PositionMaster { get; }// = new Plateau(1, 1);

    private Fleet Fleet;

    public string Result => $"{Fleet}";

    public Rover.RoverUnit AddRover(int PositionX, int PositionY, DirectionEnum Direction)
        => Fleet.AddRover(PositionX, PositionY, Direction);

    public Rover.RoverUnit AddRover(string status)
        => Fleet.AddRover(status);

    public MissionController(int MaximumX, int MaximumY)
    {
        PositionMaster = new Plateau(MaximumX, MaximumY);

        Fleet = new(PositionMaster);
    }

    public MissionController(string config)
    {
        var Mission = MissionParser.Parse(config);

        PositionMaster = (Plateau)Mission.cornerDefinition;
        
        Fleet = new(PositionMaster);

        Mission.RoverDefinitions.ForEach(roverDefinition
            => Fleet.TryAddRover(roverDefinition.status, roverDefinition.moves));
    }

}