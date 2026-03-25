using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.HandAngleTests;

public class Value_NoneTests
{
    [Fact]
    public void HavingNoneTimeComponent_WhenGettingValue_ThenReturnsZero()
    {
        HandAngle handAngle = new()
        {
            Time = new TimeOnly(3, 0, 0),
            TimeComponent = TimeComponent.None,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = true
        };

        double value = handAngle.Value;

        Assert.Equal(0.0, value);
    }
}
