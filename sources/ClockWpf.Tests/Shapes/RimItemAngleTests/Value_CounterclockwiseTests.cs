using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Tests.Shapes.RimItemAngleTests;

public class Value_CounterclockwiseTests
{
    [Fact]
    public void HavingCounterclockwiseDirectionAndIndexZero_WhenGettingValue_ThenReturnsNegatedOffsetAngle()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 0,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 6,
            OffsetAngle = 30
        };

        double value = rimItemAngle.Value;

        Assert.Equal(-30, value);
    }

    [Fact]
    public void HavingCounterclockwiseDirectionWithOffsetAndPositiveIndex_WhenGettingValue_ThenReturnsNegatedSumOfOffsetAndIndexContribution()
    {
        RimItemAngle rimItemAngle = new()
        {
            Index = 3,
            ClockDirection = RotationDirection.Counterclockwise,
            AngleBetweenItems = 12,
            OffsetAngle = 10
        };

        double value = rimItemAngle.Value;

        Assert.Equal(-46, value);
    }
}
