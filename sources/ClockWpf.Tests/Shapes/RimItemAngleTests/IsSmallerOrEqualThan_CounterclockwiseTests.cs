using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class IsSmallerOrEqualThan_CounterclockwiseTests
{
    [Fact]
    public void HavingCounterclockwiseDirectionAndIndexContributionSmallerThanThreshold_WhenCheckingIsSmallerOrEqualThan_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsSmallerOrEqualThan(20);

        Assert.True(result);
    }

    [Fact]
    public void HavingCounterclockwiseDirectionAndIndexContributionEqualToThreshold_WhenCheckingIsSmallerOrEqualThan_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsSmallerOrEqualThan(18);

        Assert.True(result);
    }

    [Fact]
    public void HavingCounterclockwiseDirectionAndIndexContributionBiggerThanThreshold_WhenCheckingIsSmallerOrEqualThan_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsSmallerOrEqualThan(10);

        Assert.False(result);
    }
}
