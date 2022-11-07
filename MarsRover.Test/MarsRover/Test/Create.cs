using MarsRover.Models;

namespace MarsRover.Test.MarsRover.Test;

public class Create
{
    /*
    [Fact]
    public void new_Plateau_by_values()
    {
        var controller = new MissionController(9, 9);
        Check.That(controller).IsNotEqualTo(null);
    }

    [Fact]
    public void size_of_new_Plateau__by_values()
    {
        var controller = new MissionController(9, 9);
        Check.That(controller.PositionMaster.Width()).IsEqualTo(10);
        Check.That(controller.PositionMaster.Height()).IsEqualTo(10);
    }*/

    [Fact]
    public void size_of_new_Plateau__by_string()
    {
        var controller = new MissionController("9 9");
        Check.That(controller.PositionMaster.Width()).IsEqualTo(10);
        Check.That(controller.PositionMaster.Height()).IsEqualTo(10);
    }

    /*
    [Fact]
    public void new_Rover__by_values()
    {
        var controller = new MissionController(9, 9);
        var rover = controller.AddRover(5, 5, DirectionEnum.E);
        Check.That(rover).IsNotEqualTo(null);
    }

    [Fact]
    public void status_of_new_Rover__by_values()
    {
        var controller = new MissionController(9, 9);
        var rover = controller.AddRover(5, 5, DirectionEnum.E);
        Check.That(rover.PrintDispatch()).IsEqualTo("5 5 E");
    }*/

    [Fact]
    public void status_of_new_Rover__by_string()
    {
        var controller = new MissionController("9 9");
        var rover = controller.AddRover("5 5 E");
        Check.That(rover.PrintDispatch()).IsEqualTo("5 5 E");
    }

}