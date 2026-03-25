using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.HandAngleTests;

public class Value_MinuteTests
{
    [Fact]
    public void HavingMinuteComponentAndIntegralValue_WhenGettingValue_ThenReturns6DegreesPerMinute()
    {
        HandAngle handAngle = new()
        {
            Time = TimeSpan.FromMinutes(15),
            TimeComponent = TimeComponent.Minute,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = true
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }

    [Fact]
    public void HavingMinuteComponentWithSecondsAndIntegralValue_WhenGettingValue_ThenIgnoresSubMinuteProgress()
    {
        HandAngle handAngle = new()
        {
            Time = new TimeSpan(0, 15, 45),
            TimeComponent = TimeComponent.Minute,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = true
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }

    [Fact]
    public void HavingMinuteComponentAndFractionalValue_WhenGettingValue_ThenReturnsProportionalAngle()
    {
        HandAngle handAngle = new()
        {
            Time = TimeSpan.FromMinutes(15),
            TimeComponent = TimeComponent.Minute,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = false
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }

    [Fact]
    public void HavingMinuteComponentWithSecondsAndFractionalValue_WhenGettingValue_ThenIncludesSubMinuteProgress()
    {
        HandAngle handAngle = new()
        {
            Time = new TimeSpan(0, 15, 30),
            TimeComponent = TimeComponent.Minute,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = false
        };

        double value = handAngle.Value;

        Assert.Equal(93.0, value, precision: 10);
    }
}
