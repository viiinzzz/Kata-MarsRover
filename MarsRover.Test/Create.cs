namespace MarsRover.Helpers.Test;

public class Create
{
    [Fact]
    public void new_Plateau_by_values()
    {
        var controller = new MissionController(10, 10);
        Check.That(controller).IsNotEqualTo(null);
    }

    [Fact]
    public void size_of_new_Plateau__by_values()
    {
        var controller = new MissionController(10, 10);
        Check.That(controller.PositionMaster.Width()).IsEqualTo(11);
        Check.That(controller.PositionMaster.Height()).IsEqualTo(11);
    }

    [Fact]
    public void size_of_new_Plateau__by_string()
    {
        var controller = new MissionController("10 10");
        Check.That(controller.PositionMaster.Width()).IsEqualTo(11);
        Check.That(controller.PositionMaster.Height()).IsEqualTo(11);
    }

    [Fact]
    public void new_Rover__by_values()
    {
        var controller = new MissionController(10, 10);
        var rover = controller.AddRover(5, 5, DirectionEnum.E);
        Check.That(rover).IsNotEqualTo(null);
    }

    [Fact]
    public void status_of_new_Rover__by_values()
    {
        var controller = new MissionController(10, 10);
        var rover = controller.AddRover(5, 5, DirectionEnum.E);
        Check.That(rover.StatusString).IsEqualTo("5 5 E");
    }

    [Fact]
    public void status_of_new_Rover__by_string()
    {
        var controller = new MissionController("10 10");
        var rover = controller.AddRover("5 5 E");
        Check.That(rover.StatusString).IsEqualTo("5 5 E");
    }
}