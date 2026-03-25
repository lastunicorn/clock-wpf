using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class ImplicitOperatorTests
{
    [Fact]
    public void HavingRimItemAngle_WhenImplicitlyConvertingToDouble_ThenReturnsValueProperty()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 12,
            OffsetAngle = 10
        };

        double value = rimItemAngle;

        Assert.Equal(rimItemAngle.Value, value);
    }
}
