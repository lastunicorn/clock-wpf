using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionBySpeedTests;

public class StartTests
{
    [Fact]
    public void HavingZeroTransitionSpeed_WhenStarting_ThenIsRunningIsFalse()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 0;

        transition.Start(TimeOnly.MinValue, new TimeOnly(1, 0, 0), 30);

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingZeroTransitionSpeed_WhenStarting_ThenCurrentTimeIsEndTime()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 0;
        TimeOnly endTime = new TimeOnly(12, 0, 0);

        transition.Start(TimeOnly.MinValue, endTime, 30);

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingNegativeTransitionSpeed_WhenStarting_ThenIsRunningIsFalse()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = -1;

        transition.Start(TimeOnly.MinValue, new TimeOnly(1, 0, 0), 30);

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingNegativeTransitionSpeed_WhenStarting_ThenCurrentTimeIsEndTime()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = -1;
        TimeOnly endTime = new TimeOnly(12, 0, 0);

        transition.Start(TimeOnly.MinValue, endTime, 30);

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingPositiveTransitionSpeed_WhenStarting_ThenIsRunningIsTrue()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 1;

        transition.Start(TimeOnly.MinValue, new TimeOnly(1, 0, 0), 30);

        Assert.True(transition.IsRunning);
    }
}
