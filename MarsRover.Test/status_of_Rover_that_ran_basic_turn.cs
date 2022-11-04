namespace MarsRover.Helpers.Test;

public class status_of_Rover_that_ran_basic_turn
{
    [Fact]
    public void left_360()
    {
        var controller = new MissionController("1 1");
        var rover = controller.AddRover("0 0 N");
        rover.Run(MoveEnum.L);
        Check.That(rover.StatusString).IsEqualTo("0 0 W");
        rover.Run(MoveEnum.L);
        Check.That(rover.StatusString).IsEqualTo("0 0 S");
        rover.Run(MoveEnum.L);
        Check.That(rover.StatusString).IsEqualTo("0 0 E");
        rover.Run(MoveEnum.L);
        Check.That(rover.StatusString).IsEqualTo("0 0 N");
    }

    [Fact]
    public void right_360()
    {
        var controller = new MissionController("1 1");
        var rover = controller.AddRover("0 0 N");
        rover.Run(MoveEnum.R);
        Check.That(rover.StatusString).IsEqualTo("0 0 E");
        rover.Run(MoveEnum.R);
        Check.That(rover.StatusString).IsEqualTo("0 0 S");
        rover.Run(MoveEnum.R);
        Check.That(rover.StatusString).IsEqualTo("0 0 W");
        rover.Run(MoveEnum.R);
        Check.That(rover.StatusString).IsEqualTo("0 0 N");
    }
}