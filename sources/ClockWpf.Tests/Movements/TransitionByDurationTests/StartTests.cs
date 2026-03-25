using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class StartTests
{
    [Fact]
    public void HavingZeroTransitionDuration_WhenStarting_ThenIsRunningIsFalse()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.Zero;

        transition.Start(TimeOnly.MinValue, new TimeOnly(1, 0, 0), 30);

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingZeroTransitionDuration_WhenStarting_ThenCurrentTimeIsEndTime()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.Zero;
        TimeOnly endTime = new TimeOnly(12, 0, 0);

        transition.Start(TimeOnly.MinValue, endTime, 30);

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingNegativeTransitionDuration_WhenStarting_ThenIsRunningIsFalse()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(-1);

        transition.Start(TimeOnly.MinValue, new TimeOnly(1, 0, 0), 30);

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingNegativeTransitionDuration_WhenStarting_ThenCurrentTimeIsEndTime()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(-1);
        TimeOnly endTime = new TimeOnly(12, 0, 0);

        transition.Start(TimeOnly.MinValue, endTime, 30);

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingPositiveTransitionDuration_WhenStarting_ThenIsRunningIsTrue()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);

        transition.Start(TimeOnly.MinValue, new TimeOnly(1, 0, 0), 30);

        Assert.True(transition.IsRunning);
    }
}
