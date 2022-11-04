using MarsRover.Controller.Data;
using MarsRover.Controller.Parser;
using MarsRover.Models;

namespace MarsRover.Controller;

public class MissionController
{
    public IPositionMaster PositionMaster { get; }// = new Plateau(1, 1);

    private IDispatcher dispatcher;

    public string Result => $"{dispatcher.PrintRovers()}";

    public Rover.RoverUnit AddRover(int PositionX, int PositionY, DirectionEnum Direction)
        => dispatcher.AddRover(PositionX, PositionY, Direction);

    public Rover.RoverUnit AddRover(string status)
        => dispatcher.AddRover(status);

    public MissionController(int MaximumX, int MaximumY)
    {
        PositionMaster = new Plateau(MaximumX, MaximumY);

        dispatcher = new Fleet(PositionMaster);
    }

    public MissionController(string config)
    {
        var Mission = MissionParser.Parse(config);

        PositionMaster = (Plateau)Mission.cornerDefinition;
        
        dispatcher = new Fleet(PositionMaster);

        Mission.RoverDefinitions.ForEach(roverDefinition
            => dispatcher.TryAddRover(roverDefinition.status, roverDefinition.moves));
    }

}