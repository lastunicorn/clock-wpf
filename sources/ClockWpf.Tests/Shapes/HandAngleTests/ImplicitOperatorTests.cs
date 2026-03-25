using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.HandAngleTests;

public class ImplicitOperatorTests
{
    [Fact]
    public void HavingHandAngle_WhenImplicitlyConvertingToDouble_ThenReturnsValueProperty()
    {
        HandAngle handAngle = new()
        {
            Time = new TimeOnly(3, 0, 0),
            TimeComponent = TimeComponent.Hour,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = true
        };

        double value = handAngle;

        Assert.Equal(handAngle.Value, value);
    }
}
