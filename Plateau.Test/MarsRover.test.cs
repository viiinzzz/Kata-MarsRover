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
        Check.That(plateau.Width).IsEqualTo(10);
        Check.That(plateau.Height).IsEqualTo(10);
    }

    [Fact]
    public void check_Plateau_size_when_initialized_with_string()
    {
        var plateau = new Plateau("10 10");
        Check.That(plateau.Width).IsEqualTo(10);
        Check.That(plateau.Height).IsEqualTo(10);
    }

    [Fact]
    public void create_Rover()
    {
        var plateau = new Plateau(10, 10);
        var rover = new Rover(5, 5, Direction.E, plateau);
        Check.That(rover).IsNotEqualTo(null);
    }

    [Fact]
    public void check_Rover_initial_status()
    {
        var plateau = new Plateau(10, 10);
        var rover = new Rover(5, 5, Direction.E, plateau);
        Check.That(rover.Status).IsEqualTo("5 5 E");
    }
    [Fact]
    public void check_Rover_initial_status_when_initialized_with_string()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 E", plateau);
        Check.That(rover.Status).IsEqualTo("5 5 E");
    }

    [Fact]
    public void check_Rover_turn_left_360()
    {
        var plateau = new Plateau("1 1");
        var rover = new Rover("0 0 N", plateau);
        rover.Do(Move.L);
        Check.That(rover.Status).IsEqualTo("0 0 W");
        rover.Do(Move.L);
        Check.That(rover.Status).IsEqualTo("0 0 S");
        rover.Do(Move.L);
        Check.That(rover.Status).IsEqualTo("0 0 E");
        rover.Do(Move.L);
        Check.That(rover.Status).IsEqualTo("0 0 N");
    }

    [Fact]
    public void check_Rover_turn_right_360()
    {
        var plateau = new Plateau("1 1");
        var rover = new Rover("0 0 N", plateau);
        rover.Do(Move.R);
        Check.That(rover.Status).IsEqualTo("0 0 E");
        rover.Do(Move.R);
        Check.That(rover.Status).IsEqualTo("0 0 S");
        rover.Do(Move.R);
        Check.That(rover.Status).IsEqualTo("0 0 W");
        rover.Do(Move.R);
        Check.That(rover.Status).IsEqualTo("0 0 N");
    }

    [Fact]
    public void check_Rover_move_forward_when_heading_north()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 N", plateau);
        rover.Do(Move.M);
        Check.That(rover.Status).IsEqualTo("5 6 N");
    }

    [Fact]
    public void check_Rover_move_forward_when_heading_west()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 W", plateau);
        rover.Do(Move.M);
        Check.That(rover.Status).IsEqualTo("4 5 W");
    }

    [Fact]
    public void check_Rover_move_forward_when_heading_south()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 S", plateau);
        rover.Do(Move.M);
        Check.That(rover.Status).IsEqualTo("5 4 S");
    }

    [Fact]
    public void check_Rover_move_forward_when_heading_east()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 E", plateau);
        rover.Do(Move.M);
        Check.That(rover.Status).IsEqualTo("6 5 E");
    }
}
