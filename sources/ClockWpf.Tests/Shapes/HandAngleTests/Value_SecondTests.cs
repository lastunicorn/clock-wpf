using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.HandAngleTests;

public class Value_SecondTests
{
    [Fact]
    public void HavingSecondComponentAndIntegralValue_WhenGettingValue_ThenReturns6DegreesPerSecond()
    {
        HandAngle handAngle = new()
        {
            Time = TimeSpan.FromSeconds(15),
            TimeComponent = TimeComponent.Second,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = true
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }

    [Fact]
    public void HavingSecondComponentAndFractionalValue_WhenGettingValue_ThenReturnsProportionalAngle()
    {
        HandAngle handAngle = new()
        {
            Time = TimeSpan.FromSeconds(15),
            TimeComponent = TimeComponent.Second,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = false
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }
}
