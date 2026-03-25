using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class IsBiggerOrEqualThan_CounterclockwiseTests
{
    [Fact]
    public void HavingCounterclockwiseDirectionAndIndexContributionBiggerThanThreshold_WhenCheckingIsBiggerOrEqualThan_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsBiggerOrEqualThan(20);

        Assert.True(result);
    }

    [Fact]
    public void HavingCounterclockwiseDirectionAndIndexContributionEqualToThreshold_WhenCheckingIsBiggerOrEqualThan_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsBiggerOrEqualThan(30);

        Assert.True(result);
    }

    [Fact]
    public void HavingCounterclockwiseDirectionAndIndexContributionSmallerThanThreshold_WhenCheckingIsBiggerOrEqualThan_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsBiggerOrEqualThan(40);

        Assert.False(result);
    }
}
