using Xunit;
using NFluent;
using MarsRover;

namespace MarsRover.Test;

public class PlateauTest
{
    [Fact]
    public void Test1()
    {
        Check.That(new Plateau(10, 10)).IsNotEqualTo(null);
    }
}
