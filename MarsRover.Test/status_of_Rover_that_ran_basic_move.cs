namespace MarsRover.Helpers.Test;

public class status_of_Rover_that_ran_basic_move
{
    [Fact]
    public void single_move_forward_heading_north()
    {
        var controller = new MissionController("10 10");
        var rover = controller.AddRover("5 5 N");
        rover.Run(MoveEnum.M);
        Check.That(rover.StatusString).IsEqualTo("5 6 N");
    }

    [Fact]
    public void single_move_forward_heading_west()
    {
        var controller = new MissionController("10 10");
        var rover = controller.AddRover("5 5 W");
        rover.Run(MoveEnum.M);
        Check.That(rover.StatusString).IsEqualTo("4 5 W");
    }

    [Fact]
    public void single_move_forward_heading_south()
    {
        var controller = new MissionController("10 10");
        var rover = controller.AddRover("5 5 S");
        rover.Run(MoveEnum.M);
        Check.That(rover.StatusString).IsEqualTo("5 4 S");
    }

    [Fact]
    public void single_move_forward_heading_east()
    {
        var controller = new MissionController("10 10");
        var rover = controller.AddRover("5 5 E");
        rover.Run(MoveEnum.M);
        Check.That(rover.StatusString).IsEqualTo("6 5 E");
    }
}