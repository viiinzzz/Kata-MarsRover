using NFluent;

namespace MarsRover.Helpers.Test;

public class status_of_Rover_that_ran_basic_turn
{
    [Fact]
    public void left_360()
    {
        var plateau = new MissionController("1 1");
        var rover = plateau.AddRover("0 0 N");
        rover.Run(Move.L);
        Check.That(rover.StatusString).IsEqualTo("0 0 W");
        rover.Run(Move.L);
        Check.That(rover.StatusString).IsEqualTo("0 0 S");
        rover.Run(Move.L);
        Check.That(rover.StatusString).IsEqualTo("0 0 E");
        rover.Run(Move.L);
        Check.That(rover.StatusString).IsEqualTo("0 0 N");
    }

    [Fact]
    public void right_360()
    {
        var plateau = new MissionController("1 1");
        var rover = plateau.AddRover("0 0 N");
        rover.Run(Move.R);
        Check.That(rover.StatusString).IsEqualTo("0 0 E");
        rover.Run(Move.R);
        Check.That(rover.StatusString).IsEqualTo("0 0 S");
        rover.Run(Move.R);
        Check.That(rover.StatusString).IsEqualTo("0 0 W");
        rover.Run(Move.R);
        Check.That(rover.StatusString).IsEqualTo("0 0 N");
    }
}