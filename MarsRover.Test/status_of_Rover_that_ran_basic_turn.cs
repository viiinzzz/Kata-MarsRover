using NFluent;

namespace MarsRover.Test;

public class status_of_Rover_that_ran_basic_turn
{
    [Fact]
    public void left_360()
    {
        var plateau = new Plateau("1 1");
        var rover = new Rover("0 0 N", plateau.ValidatePosition);
        rover.Run(Move.L, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("0 0 W");
        rover.Run(Move.L, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("0 0 S");
        rover.Run(Move.L, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("0 0 E");
        rover.Run(Move.L, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("0 0 N");
    }

    [Fact]
    public void right_360()
    {
        var plateau = new Plateau("1 1");
        var rover = new Rover("0 0 N", plateau.ValidatePosition);
        rover.Run(Move.R, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("0 0 E");
        rover.Run(Move.R, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("0 0 S");
        rover.Run(Move.R, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("0 0 W");
        rover.Run(Move.R, plateau.ValidatePosition);
        Check.That(rover.Status.ToString()).IsEqualTo("0 0 N");
    }
}