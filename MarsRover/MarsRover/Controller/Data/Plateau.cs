using MarsRover.Controller.Parser;
using MarsRover.Models;

namespace MarsRover.Controller.Data;

public record class Plateau(int MaximumX, int MaximumY) : RecordWithValidation, IPositionMaster
{
    protected override void Validate()
    {
        if (MaximumX <= 0 || MaximumX % 2 == 0)
            throw new Exception("plateau has invalid corner -- MaximumX must be 1 or greater odd");
        if (MaximumY <= 1)
            throw new Exception("plateau has invalid corner -- MaximumY must be 1 or greater");
    }

    public int Width() => MaximumX + 1;
    public int Height() => MaximumY + 1;
    
    public bool IsNorthPole(BaseRoverStatus status) => status.PositionY == MaximumY;
    public bool IsSouthPole(BaseRoverStatus status) => status.PositionY == 0;


    public static implicit operator Plateau((int MaximumX, int MaximumY) corner)
        => new(corner.MaximumX, corner.MaximumY);

    public static implicit operator Plateau(string corner)
        => Parser.Parse(corner);

    private static readonly ICornerParser Parser = new CornerParser();

    public BaseRoverStatus ValidatePosition(BaseRoverStatus status)
    {
        if (status.PositionX < 0) throw new Exception("invalid rover move -- bumped into West border");
        if (status.PositionY < 0) throw new Exception("invalid rover move -- bumped into South border");
        if (status.PositionX > MaximumX) throw new Exception("invalid rover move -- bumped into East border");
        if (status.PositionY > MaximumY) throw new Exception("invalid rover move -- bumped into North border");
        return status;
    }

}