using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.HandAngleTests;

public class Value_HourTests
{
    [Fact]
    public void HavingHourComponentAndIntegralValue_WhenGettingValue_ThenReturns30DegreesPerHour()
    {
        HandAngle handAngle = new()
        {
            Time = TimeSpan.FromHours(3),
            TimeComponent = TimeComponent.Hour,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = true
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }

    [Fact]
    public void HavingHourOver12AndIntegralValue_WhenGettingValue_ThenAppliesModulo12ToHours()
    {
        HandAngle handAngle = new()
        {
            Time = TimeSpan.FromHours(15),
            TimeComponent = TimeComponent.Hour,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = true
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }

    [Fact]
    public void HavingHourComponentWithMinutesAndIntegralValue_WhenGettingValue_ThenIgnoresSubHourProgress()
    {
        HandAngle handAngle = new()
        {
            Time = new TimeSpan(3, 45, 0),
            TimeComponent = TimeComponent.Hour,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = true
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }

    [Fact]
    public void HavingHourComponentAndFractionalValue_WhenGettingValue_ThenReturnsProportionalAngle()
    {
        HandAngle handAngle = new()
        {
            Time = TimeSpan.FromHours(3),
            TimeComponent = TimeComponent.Hour,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = false
        };

        double value = handAngle.Value;

        Assert.Equal(90.0, value);
    }

    [Fact]
    public void HavingHourComponentWithMinutesAndFractionalValue_WhenGettingValue_ThenIncludesSubHourProgress()
    {
        HandAngle handAngle = new()
        {
            Time = new TimeSpan(3, 30, 0),
            TimeComponent = TimeComponent.Hour,
            ClockDirection = RotationDirection.Clockwise,
            IntegralValue = false
        };

        double value = handAngle.Value;

        Assert.Equal(105.0, value);
    }

    [Fact]
    public void HavingCounterclockwiseDirection_WhenGettingValue_ThenReturnsNegativeAngle()
    {
        HandAngle handAngle = new()
        {
            Time = TimeSpan.FromHours(3),
            TimeComponent = TimeComponent.Hour,
            ClockDirection = RotationDirection.Counterclockwise,
            IntegralValue = true
        };

        double value = handAngle.Value;

        Assert.Equal(-90.0, value);
    }
}
