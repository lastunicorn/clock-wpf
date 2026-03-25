using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class IsSmallerOrEqualThan_ClockwiseTests
{
    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionSmallerThanThreshold_WhenCheckingIsSmallerOrEqualThan_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsSmallerOrEqualThan(20);

        Assert.True(result);
    }

    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionEqualToThreshold_WhenCheckingIsSmallerOrEqualThan_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsSmallerOrEqualThan(18);

        Assert.True(result);
    }

    [Fact]
    public void HavingClockwiseDirectionAndIndexContributionBiggerThanThreshold_WhenCheckingIsSmallerOrEqualThan_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 10
        };

        bool result = rimItemAngle.IsSmallerOrEqualThan(10);

        Assert.False(result);
    }
}
