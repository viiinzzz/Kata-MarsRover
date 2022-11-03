using NFluent;

namespace MarsRover.Test;

public class status_of_Rovers_that_ran_instructions
{
    [Fact]
    public void single_rover()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 S", plateau.ValidatePosition);
        rover.Run("LMRMMRRR", plateau.ValidatePosition);
        /*
        5 5 S
        L -> 5 5 E
        M -> 6 5 E
        R -> 6 5 S
        M -> 6 4 S
        M -> 6 3 S
        R -> 6 3 W
        R -> 6 3 N
        R -> 6 3 E
         */
        Check.That(rover.Status.ToString()).IsEqualTo("6 3 E");
    }

    [Fact]
    public void many_rovers()
    {
        const string input = @"
            5 5
            1 2 N
            LMLMLMLMM
            3 3 E
            MMRMMRMRRM
        ";
        const string output = @"
            1 3 N
            5 1 E
        ";
        var plateau = new Plateau(input);
        Check.That(plateau.Result).IsEqualTo(output?.trimLines());
    }

}