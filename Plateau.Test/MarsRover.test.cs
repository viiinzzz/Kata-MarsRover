using Xunit;
using NFluent;
using MarsRover;

namespace MarsRover.Test;

public class PlateauTest
{
    [Fact]
    public void create_Plateau()
    {
        Check.That(new Plateau(10, 10)).IsNotEqualTo(null);
    }

    [Fact]
    public void create_Rover()
    {
        var rover = new Rover(5, 5, Heading.E);
        Check.That(rover).IsNotEqualTo(null);
    }

    [Fact]
    public void check_Rover_initial_status()
    {
        var rover = new Rover(5, 5, Heading.E);
        Check.That(rover.Status).IsEqualTo("5 5 E");
    }
}
