using NFluent;

namespace MarsRover.Test;

public class Create
{
    [Fact]
    public void new_Plateau_by_values()
    {
        var plateau = new Plateau(10, 10);
        Check.That(plateau).IsNotEqualTo(null);
    }

    [Fact]
    public void size_of_new_Plateau__by_values()
    {
        var plateau = new Plateau(10, 10);
        Check.That(plateau.Width).IsEqualTo(11);
        Check.That(plateau.Height).IsEqualTo(11);
    }

    [Fact]
    public void size_of_new_Plateau__by_string()
    {
        var plateau = new Plateau("10 10");
        Check.That(plateau.Width).IsEqualTo(11);
        Check.That(plateau.Height).IsEqualTo(11);
    }

    [Fact]
    public void new_Rover__by_values()
    {
        var plateau = new Plateau(10, 10);
        var rover = new Rover(5, 5, Direction.E, plateau.ValidatePosition);
        Check.That(rover).IsNotEqualTo(null);
    }

    [Fact]
    public void status_of_new_Rover__by_values()
    {
        var plateau = new Plateau(10, 10);
        var rover = new Rover(5, 5, Direction.E, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("5 5 E");
    }

    [Fact]
    public void status_of_new_Rover__by_string()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 E", plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("5 5 E");
    }
}