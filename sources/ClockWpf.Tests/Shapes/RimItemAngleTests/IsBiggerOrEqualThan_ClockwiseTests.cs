using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class IsBiggerOrEqualThan_ClockwiseTests
{
    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionBiggerThanThreshold_WhenCheckingIsBiggerOrEqualThan_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsBiggerOrEqualThan(20);

        Assert.True(result);
    }

    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionEqualToThreshold_WhenCheckingIsBiggerOrEqualThan_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsBiggerOrEqualThan(30);

        Assert.True(result);
    }

    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionSmallerThanThreshold_WhenCheckingIsBiggerOrEqualThan_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsBiggerOrEqualThan(40);

        Assert.False(result);
    }
}
