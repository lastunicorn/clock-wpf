using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionBySpeedTests;

public class TransitionSpeedTests
{
    [Fact]
    public void WhenSettingPositiveValue_ThenTransitionSpeedReturnsSetValue()
    {
        using TransitionBySpeed transition = new(_ => { });

        transition.TransitionSpeed = 2.5;

        Assert.Equal(2.5, transition.TransitionSpeed);
    }

    [Fact]
    public void WhenSettingZeroValue_ThenTransitionSpeedIsZero()
    {
        using TransitionBySpeed transition = new(_ => { });

        transition.TransitionSpeed = 0;

        Assert.Equal(0, transition.TransitionSpeed);
    }
}
