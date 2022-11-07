namespace MarsRover.Test.MarsRover.Test;

public class Fail_when_Plateau_has
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void width_zero_or_negative__by_value(int Width)
    {
        int MaximumX = Width - 1, MaximumY = 5;
        FluentActions.Invoking(() => new MissionController(MaximumX, MaximumY))
            .Should().Throw<Exception>()
            .WithMessage("plateau has invalid corner -- MaximumX must be 1 or greater odd");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void height_zero_or_negative_by_value(int Height)
    {
        int MaximumX = 5, MaximumY = Height - 1;
        FluentActions.Invoking(() => new MissionController(MaximumX, MaximumY))
            .Should().Throw<Exception>()
            .WithMessage("plateau has invalid corner -- MaximumY must be 1 or greater");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void width_zero_or_negative__by_string(int Width)
    {
        int MaximumX = Width - 1, MaximumY = 5;
        FluentActions.Invoking(() => new MissionController($"{MaximumX} {MaximumY}"))
            .Should().Throw<Exception>()
            .WithMessage("plateau has invalid corner --*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void height_zero_or_negative__by_string(int Height)
    {
        int MaximumX = 5, MaximumY = Height - 1;
        FluentActions.Invoking(() => new MissionController($"{MaximumX} {MaximumY}"))
            .Should().Throw<Exception>()
            .WithMessage("plateau has invalid corner --*");
    }

    //in the simple polar case:
    // - width must be odd and not zero, ie 2, 4, 6... 
    // - height be more than 2
    //hence provisionning a new plateau shall fail when:
    // - width is even, ie 0, 1, 3, 5...
    // - height is 0, 1
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void width_odd_or_zero(int Width)
    {
        int MaximumX = Width - 1, MaximumY = 5;
        FluentActions.Invoking(() => new MissionController($"{MaximumX} {MaximumY}"))
            .Should().Throw<Exception>()
            .WithMessage("plateau has invalid corner --*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void height_less_than_2(int Height)
    {
        int MaximumX = 5, MaximumY = Height - 1;
        FluentActions.Invoking(() => new MissionController($"{MaximumX} {MaximumY}"))
            .Should().Throw<Exception>()
            .WithMessage("plateau has invalid corner --*");
    }

}