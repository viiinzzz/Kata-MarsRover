using Xunit;
using NFluent;
using MarsRover;

namespace MarsRover.Test;

public class PlateauTest
{
    [Fact]
    public void create_Plateau()
    {
        var plateau = new Plateau(10, 10);
        Check.That(plateau).IsNotEqualTo(null);
    }

    [Fact]
    public void check_Plateau_size()
    {
        var plateau = new Plateau(10, 10);
        Check.That(plateau.width).IsEqualTo(10);
        Check.That(plateau.height).IsEqualTo(10);
    }

    [Fact]
    public void check_Plateau_size_when_initialized_with_string()
    {
        var plateau = new Plateau("10 10");
        Check.That(plateau.width).IsEqualTo(10);
        Check.That(plateau.height).IsEqualTo(10);
    }

    [Fact]
    public void create_Rover()
    {
        var plateau = new Plateau(10, 10);
        var rover = new Rover(5, 5, Heading.E, plateau);
        Check.That(rover).IsNotEqualTo(null);
    }

    [Fact]
    public void check_Rover_initial_status()
    {
        var plateau = new Plateau(10, 10);
        var rover = new Rover(5, 5, Heading.E, plateau);
        Check.That(rover.Status).IsEqualTo("5 5 E");
    }
}
