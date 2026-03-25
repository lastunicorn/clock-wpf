using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class Value_ClockwiseTests
{
    [Fact]
    public void HavingClockwiseDirectionAndIndexZero_WhenGettingValue_ThenReturnsOffsetAngle()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 30
        };

        double value = rimItemAngle.Value;

        Assert.Equal(30, value);
    }

    [Fact]
    public void HavingClockwiseDirectionAndPositiveIndex_WhenGettingValue_ThenReturnsOffsetPlusIndexTimesAngleBetweenItems()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 5,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 0
        };

        double value = rimItemAngle.Value;

        Assert.Equal(30, value);
    }

    [Fact]
    public void HavingClockwiseDirectionWithOffsetAndPositiveIndex_WhenGettingValue_ThenReturnsSumOfOffsetAndIndexContribution()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Clockwise,
            AngleBetweenItems = 12,
            OffsetAngle = 10
        };

        double value = rimItemAngle.Value;

        Assert.Equal(46, value);
    }
}
