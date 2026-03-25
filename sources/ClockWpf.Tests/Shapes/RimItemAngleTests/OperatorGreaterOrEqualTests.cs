using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class OperatorGreaterOrEqualTests
{
    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionBiggerThanThreshold_WhenUsingOperatorGreaterOrEqual_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle >= 20;

        Assert.True(result);
    }

    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionEqualToThreshold_WhenUsingOperatorGreaterOrEqual_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle >= 30;

        Assert.True(result);
    }

    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionSmallerThanThreshold_WhenUsingOperatorGreaterOrEqual_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle >= 40;

        Assert.False(result);
    }
}
