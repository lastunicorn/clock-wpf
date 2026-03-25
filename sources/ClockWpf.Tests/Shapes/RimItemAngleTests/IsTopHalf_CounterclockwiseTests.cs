using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class IsTopHalf_CounterclockwiseTests
{
    [Fact]
    public void HavingCounterclockwiseDirectionAndAngleBetweenMinus90AndMinus270_WhenCheckingIsTopHalf_ThenReturnsTrue()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 1,
            OffsetAngle = 180
        };

        bool isTopHalf = rimItemAngle.IsTopHalf;

        Assert.True(isTopHalf);
    }

    [Fact]
    public void HavingCounterclockwiseDirectionAndAngleOfMinus90_WhenCheckingIsTopHalf_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 1,
            OffsetAngle = 90
        };

        bool isTopHalf = rimItemAngle.IsTopHalf;

        Assert.False(isTopHalf);
    }

    [Fact]
    public void HavingCounterclockwiseDirectionAndAngleOfMinus270_WhenCheckingIsTopHalf_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 1,
            OffsetAngle = 270
        };

        bool isTopHalf = rimItemAngle.IsTopHalf;

        Assert.False(isTopHalf);
    }

    [Fact]
    public void HavingCounterclockwiseDirectionAndAngleOfZero_WhenCheckingIsTopHalf_ThenReturnsFalse()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 1,
            OffsetAngle = 0
        };

        bool isTopHalf = rimItemAngle.IsTopHalf;

        Assert.False(isTopHalf);
    }
}
