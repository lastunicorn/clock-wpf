using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class IsTopHalf_ClockwiseTests
{
    [Fact]
    public void HavingClockwiseDirectionAndAngleBetween90And270_WhenCheckingIsTopHalf_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 1,
            OffsetAngle = 180
        };

        bool isTopHalf = rimItemAngle.IsTopHalf;

        Assert.True(isTopHalf);
    }

    [Fact]
    public void HavingClockwiseDirectionAndAngleOf90_WhenCheckingIsTopHalf_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 1,
            OffsetAngle = 90
        };

        bool isTopHalf = rimItemAngle.IsTopHalf;

        Assert.False(isTopHalf);
    }

    [Fact]
    public void HavingClockwiseDirectionAndAngleOf270_WhenCheckingIsTopHalf_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 1,
            OffsetAngle = 270
        };

        bool isTopHalf = rimItemAngle.IsTopHalf;

        Assert.False(isTopHalf);
    }

    [Fact]
    public void HavingClockwiseDirectionAndAngleBelow90_WhenCheckingIsTopHalf_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 1,
            OffsetAngle = 0
        };

        bool isTopHalf = rimItemAngle.IsTopHalf;

        Assert.False(isTopHalf);
    }
}
