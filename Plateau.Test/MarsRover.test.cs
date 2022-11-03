using System.Security.Cryptography.X509Certificates;
using Xunit;
using NFluent;
using FluentAssertions;
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
        Check.That(plateau.Width).IsEqualTo(11);
        Check.That(plateau.Height).IsEqualTo(11);
    }

    [Fact]
    public void check_Plateau_size_when_initialized_with_string()
    {
        var plateau = new Plateau("10 10");
        Check.That(plateau.Width).IsEqualTo(11);
        Check.That(plateau.Height).IsEqualTo(11);
    }

    [Fact]
    public void create_Rover()
    {
        var plateau = new Plateau(10, 10);
        var rover = new Rover(5, 5, Direction.E, plateau.ValidatePosition);
        Check.That(rover).IsNotEqualTo(null);
    }

    [Fact]
    public void check_Rover_initial_status()
    {
        var plateau = new Plateau(10, 10);
        var rover = new Rover(5, 5, Direction.E, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("5 5 E");
    }
    [Fact]
    public void check_Rover_initial_status_when_initialized_with_string()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 E", plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("5 5 E");
    }

    [Fact]
    public void check_Rover_turn_left_360()
    {
        var plateau = new Plateau("1 1");
        var rover = new Rover("0 0 N", plateau.ValidatePosition);
        rover.Run(Move.L, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("0 0 W");
        rover.Run(Move.L, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("0 0 S");
        rover.Run(Move.L, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("0 0 E");
        rover.Run(Move.L, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("0 0 N");
    }

    [Fact]
    public void check_Rover_turn_right_360()
    {
        var plateau = new Plateau("1 1");
        var rover = new Rover("0 0 N", plateau.ValidatePosition);
        rover.Run(Move.R, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("0 0 E");
        rover.Run(Move.R, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("0 0 S");
        rover.Run(Move.R, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("0 0 W");
        rover.Run(Move.R, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("0 0 N");
    }

    [Fact]
    public void check_Rover_move_forward_when_heading_north()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 N", plateau.ValidatePosition);
        rover.Run(Move.M, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("5 6 N");
    }

    [Fact]
    public void check_Rover_move_forward_when_heading_west()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 W", plateau.ValidatePosition);
        rover.Run(Move.M, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("4 5 W");
    }

    [Fact]
    public void check_Rover_move_forward_when_heading_south()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 S", plateau.ValidatePosition);
        rover.Run(Move.M, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("5 4 S");
    }

    [Fact]
    public void check_Rover_move_forward_when_heading_east()
    {
        var plateau = new Plateau("10 10");
        var rover = new Rover("5 5 E", plateau.ValidatePosition);
        rover.Run(Move.M, plateau.ValidatePosition);
        Check.That(rover.Status).IsEqualTo("6 5 E");
    }

    [Fact]
    public void check_Rover_multiple_moves()
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
        Check.That(rover.Status).IsEqualTo("6 3 E");
    }

    private string trimLines(string lines)
        => joinLines(lines.Split(new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0));

    private string joinLines(IEnumerable<string> lines)
        => lines.Count() == 0 ? ""
            : lines.Aggregate((x, y) => x + '\n' + y);

    [Fact]
    public void check_Plateau_Fleet()
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
        Check.That(plateau.Result).IsEqualTo(trimLines(output));
    }

    [Fact]
    public void check_Plateau_invalid_plateau_width()
    {
        FluentActions.Invoking(() => new Plateau(-5, 5))
            .Should().Throw<Exception>()
            .WithMessage("plateau size invalid data -- width must be strictly positive");
    }

    [Fact]
    public void check_Plateau_invalid_plateau_height()
    {
        FluentActions.Invoking(() => new Plateau(5, -5))
            .Should().Throw<Exception>()
            .WithMessage("plateau size invalid data -- height must be strictly positive");
    }

    [Fact]
    public void check_Plateau_invalid_plateau_size1()
    {
        const string input = "-5 5";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: plateau size invalid data --*");
    }

    [Fact]
    public void check_Plateau_invalid_plateau_size2()
    {
        const string input = "5 -5";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: plateau size invalid data --*");
    }

    [Fact]
    public void check_Plateau_Fleet_invalid_moves()
    {
        const string input = @"
            5 5
            1 2 N
            LMLM_it_gonna_break_LMLMM
        ";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover moves invalid data --*");
    }

    [Fact]
    public void check_Plateau_Fleet_missing_moves()
    {
        const string input = @"
            5 5
            1 2 N
        ";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover configuration invalid data --*");
    }

    [Fact]
    public void check_Plateau_Fleet_invalid_status_direction()
    {
        const string input = @"
            5 5
            1 2 Z
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }

    [Fact]
    public void check_Plateau_Fleet_invalid_status_position1()
    {
        const string input = @"
            5 5
            -1 2 N
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }

    [Fact]
    public void check_Plateau_Fleet_invalid_status_position2()
    {
        const string input = @"
            5 5
            1 -2 N
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }

    [Fact]
    public void check_Plateau_Fleet_invalid_position3()
    {
        const string input = @"
            5 5
            1 N
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }

    [Fact]
    public void check_Plateau_Fleet_invalid_position4()
    {
        const string input = @"
            5 5
            1 2 it_gonna_break
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }

}
