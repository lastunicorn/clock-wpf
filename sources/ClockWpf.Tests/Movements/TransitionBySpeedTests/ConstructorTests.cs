using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionBySpeedTests;

public class ConstructorTests
{
    [Fact]
    public void HavingNullCallback_WhenCreatingInstance_ThenArgumentNullExceptionIsThrown()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new TransitionBySpeed(null);
        });
    }

    [Fact]
    public void WhenCreatingInstance_ThenIsRunningIsFalse()
    {
        using TransitionBySpeed transition = new(_ => { });

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void WhenCreatingInstance_ThenCurrentTimeIsZero()
    {
        using TransitionBySpeed transition = new(_ => { });

        Assert.Equal(TimeSpan.Zero, transition.CurrentTime);
    }

    [Fact]
    public void WhenCreatingInstance_ThenTransitionSpeedIsZero()
    {
        using TransitionBySpeed transition = new(_ => { });

        Assert.Equal(0, transition.TransitionSpeed);
    }
}
