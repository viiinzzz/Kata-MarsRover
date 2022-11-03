using MarsRover.Parser;

namespace MarsRover;

public class Mission
{
    public string cornerDefinition { get; set; }
    public List<(Discardable status, Discardable moves)> RoverDefinitions = new ();
}