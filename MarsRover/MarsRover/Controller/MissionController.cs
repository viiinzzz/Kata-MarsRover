using MarsRover.Controller.Data;
using MarsRover.Controller.Parser;
using MarsRover.Models;

namespace MarsRover.Controller;

public record class MissionController(int UnitCount, int PositionX, int PositionY)
{
    public IPositionMaster PositionMaster { get; }// = new Plateau(1, 1);

    private IDispatcher dispatcher;

    public string PrintDispatch => $"{dispatcher.PrintDispatch()}";

    public IDispatchable AddRover(string status)
        => dispatcher.AddRover(status);

    public MissionController(int UnitCount, int PositionX, int PositionY, int MaximumX, int MaximumY)
        : this(UnitCount, PositionX, PositionY)
    {
        PositionMaster = new Plateau(MaximumX, MaximumY);

        dispatcher = new Fleet(UnitCount, PositionMaster);
    }

    public MissionController(int MaximumX, int MaximumY)
        : this(100, 0, 0, MaximumX, MaximumY) { }

    public MissionController(string config)
        : this(100, 0, 0)
    {
        var Mission = MissionParser.Parse(config);

        PositionMaster = (Plateau)Mission.cornerDefinition;
        
        dispatcher = new Fleet(UnitCount, PositionMaster);

        Mission.RoverDefinitions.ForEach(roverDefinition
            => dispatcher.TryAddRover(roverDefinition.status, roverDefinition.moves));
    }

}