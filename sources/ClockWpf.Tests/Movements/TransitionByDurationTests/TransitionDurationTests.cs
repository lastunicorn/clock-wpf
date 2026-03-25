using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class TransitionDurationTests
{
    [Fact]
    public void WhenSettingPositiveValue_ThenTransitionDurationReturnsSetValue()
    {
        using TransitionByDuration transition = new(_ => { });

        transition.TransitionDuration = TimeSpan.FromSeconds(2);

        Assert.Equal(TimeSpan.FromSeconds(2), transition.TransitionDuration);
    }

    [Fact]
    public void WhenSettingZeroValue_ThenTransitionDurationIsZero()
    {
        using TransitionByDuration transition = new(_ => { });

        transition.TransitionDuration = TimeSpan.Zero;

        Assert.Equal(TimeSpan.Zero, transition.TransitionDuration);
    }
}
