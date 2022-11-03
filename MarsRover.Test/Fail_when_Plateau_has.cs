using FluentAssertions;

namespace MarsRover.Helpers.Test;

public class Fail_when_Plateau_has
{
    [Fact]
    public void invalid_width__by_value()
    {
        FluentActions.Invoking(() => new MissionController(-5, 5))
            .Should().Throw<Exception>()
            .WithMessage("plateau corner invalid data -- MaximumX must be strictly positive");
    }

    [Fact]
    public void invalid_height__by_value()
    {
        FluentActions.Invoking(() => new MissionController(5, -5))
            .Should().Throw<Exception>()
            .WithMessage("plateau corner invalid data -- MaximumY must be strictly positive");
    }

    [Fact]
    public void invalid_width_size__by_string()
    {
        const string input = "-5 5";
        FluentActions.Invoking(() => new MissionController(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: plateau corner invalid data --*");
    }

    [Fact]
    public void invalid_height_size__by_string()
    {
        const string input = "5 -5";
        FluentActions.Invoking(() => new MissionController(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: plateau corner invalid data --*");
    }
}