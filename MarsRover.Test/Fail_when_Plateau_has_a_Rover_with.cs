namespace MarsRover.Helpers.Test;

public class Fail_when_Plateau_has_a_Rover_with
{
    [Fact]
    public void invalid_moves()
    {
        const string input = @"
            5 5
            1 2 N
            LMLM_it_gonna_break_LMLMM
        ";
        FluentActions.Invoking(() => new MissionController(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover moves invalid data --*");
    }

    [Fact]
    public void missing_moves()
    {
        const string input = @"
            5 5
            1 2 N
        ";
        FluentActions.Invoking(() => new MissionController(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover configuration invalid data --*");
    }

    [Fact]
    public void invalid_status_direction()
    {
        const string input = @"
            5 5
            1 2 Z
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new MissionController(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }

    [Fact]
    public void invalid_status_position_x()
    {
        const string input = @"
            5 5
            -1 2 N
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new MissionController(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }

    [Fact]
    public void invalid_status_position_y()
    {
        const string input = @"
            5 5
            1 -2 N
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new MissionController(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }

    [Fact]
    public void incomplete_status()
    {
        const string input = @"
            5 5
            1 N
            LMLMLMLMM
        ";
        FluentActions.Invoking(() => new MissionController(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: rover status invalid data --*");
    }
}