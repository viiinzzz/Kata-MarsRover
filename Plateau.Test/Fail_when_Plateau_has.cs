using FluentAssertions;

namespace MarsRover.Test;

public class Fail_when_Plateau_has
{
    [Fact]
    public void invalid_width__by_value()
    {
        FluentActions.Invoking(() => new Plateau(-5, 5))
            .Should().Throw<Exception>()
            .WithMessage("plateau size invalid data -- width must be strictly positive");
    }

    [Fact]
    public void invalid_height__by_value()
    {
        FluentActions.Invoking(() => new Plateau(5, -5))
            .Should().Throw<Exception>()
            .WithMessage("plateau size invalid data -- height must be strictly positive");
    }

    [Fact]
    public void invalid_width_size__by_string()
    {
        const string input = "-5 5";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: plateau size invalid data --*");
    }

    [Fact]
    public void invalid_height_size__by_string()
    {
        const string input = "5 -5";
        FluentActions.Invoking(() => new Plateau(input))
            .Should().Throw<Exception>()
            .WithMessage("parse error: plateau size invalid data --*");
    }
}