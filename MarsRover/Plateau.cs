using MarsRover.Parser;
using MarsRover.Helpers;

namespace MarsRover;

public record class Plateau(int MaximumX, int MaximumY) : RecordWithValidation
{
    protected override void Validate()
    {
        if (MaximumX <= 0) throw new Exception("plateau corner invalid data -- MaximumX must be strictly positive");
        if (MaximumY <= 0) throw new Exception("plateau corner invalid data -- MaximumY must be strictly positive");
    }

    public int Width => MaximumX + 1;
    public int Height => MaximumY + 1;

    public static implicit operator Plateau((int MaximumX, int MaximumY) corner)
        => new (corner.MaximumX, corner.MaximumY);

    public static implicit operator Plateau(string corner)
        => Parser.Parse(corner);

    private static readonly ICornerParser Parser = new CornerParser();

    public RoverStatus ValidatePosition(RoverStatus status)
    {
        if (status.PositionX < 0) throw new Exception("rover invalid move -- bumped into left border");
        if (status.PositionY < 0) throw new Exception("rover invalid move -- bumped into bottom border");
        if (status.PositionX > MaximumX) throw new Exception("rover invalid move -- bumped into right border");
        if (status.PositionY > MaximumY) throw new Exception("rover invalid move -- bumped into top border");
        return status;
    }
}