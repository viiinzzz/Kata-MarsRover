using NFluent;

namespace MarsRover.Test;

public class status_of_Rover_that_ran_basic_move
{
    [Fact]
    public void single_move_forward_heading_north()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 N", plateau.ValidatePosition);
        rover.Run(Move.M, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("5 6 N");
    }

    [Fact]
    public void single_move_forward_heading_west()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 W", plateau.ValidatePosition);
        rover.Run(Move.M, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("4 5 W");
    }

    [Fact]
    public void single_move_forward_heading_south()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 S", plateau.ValidatePosition);
        rover.Run(Move.M, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("5 4 S");
    }

    [Fact]
    public void single_move_forward_heading_east()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 E", plateau.ValidatePosition);
        rover.Run(Move.M, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("6 5 E");
    }
}