using MarsRover.Models;

namespace MarsRover.Test.MarsRover.Test;

public class status_of_Rover_that_ran_past_pole
{
    [Theory]
    [InlineData(0, 2)]
    [InlineData(1, 3)]
    [InlineData(2, 0)]
    [InlineData(3, 1)]
    public void north(int PositionX, int nextPositionX)
    {
        var controller = new MissionController("3 3");
        var rover = controller.AddRover($"{PositionX} 3 N");
        rover.Run(MoveEnum.M);
        Check.That(rover.PrintDispatch()).IsEqualTo($"{nextPositionX} 3 S");
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(1, 3)]
    [InlineData(2, 0)]
    [InlineData(3, 1)]
    public void south(int PositionX, int nextPositionX)
    {
        var controller = new MissionController("3 3");
        var rover = controller.AddRover($"{PositionX} 0 S");
        rover.Run(MoveEnum.M);
        Check.That(rover.PrintDispatch()).IsEqualTo($"{nextPositionX} 0 N");
    }
}