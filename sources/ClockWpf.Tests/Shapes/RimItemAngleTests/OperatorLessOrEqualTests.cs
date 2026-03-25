using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class OperatorLessOrEqualTests
{
    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionSmallerThanThreshold_WhenUsingOperatorLessOrEqual_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle <= 20;

        Assert.True(result);
    }

    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionEqualToThreshold_WhenUsingOperatorLessOrEqual_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle <= 18;

        Assert.True(result);
    }

    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionBiggerThanThreshold_WhenUsingOperatorLessOrEqual_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle <= 10;

        Assert.False(result);
    }
}
