using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class ConstructorTests
{
    [Fact]
    public void HavingNullCallback_WhenCreatingInstance_ThenArgumentNullExceptionIsThrown()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new TransitionByDuration(null);
        });
    }

    [Fact]
    public void WhenCreatingInstance_ThenIsRunningIsFalse()
    {
        using TransitionByDuration transition = new(_ => { });

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void WhenCreatingInstance_ThenCurrentTimeIsZero()
    {
        using TransitionByDuration transition = new(_ => { });

        Assert.Equal(TimeOnly.MinValue, transition.CurrentTime);
    }

    [Fact]
    public void WhenCreatingInstance_ThenTransitionDurationIsZero()
    {
        using TransitionByDuration transition = new(_ => { });

        Assert.Equal(TimeSpan.Zero, transition.TransitionDuration);
    }
}
