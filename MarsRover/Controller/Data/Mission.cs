using MarsRover.Controller.Parser;

namespace MarsRover.Controller.Data;

public class Mission
{
    public string cornerDefinition { get; set; }
    public List<(Discardable status, Discardable moves)> RoverDefinitions = new();
}