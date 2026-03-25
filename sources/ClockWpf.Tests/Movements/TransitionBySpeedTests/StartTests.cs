using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionBySpeedTests;

public class StartTests
{
    [Fact]
    public void HavingZeroTransitionSpeed_WhenStarting_ThenIsRunningIsFalse()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 0;

        transition.Start(TimeSpan.Zero, TimeSpan.FromHours(1), 30);

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingZeroTransitionSpeed_WhenStarting_ThenCurrentTimeIsEndTime()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 0;
        TimeSpan endTime = TimeSpan.FromHours(12);

        transition.Start(TimeSpan.Zero, endTime, 30);

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingNegativeTransitionSpeed_WhenStarting_ThenIsRunningIsFalse()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = -1;

        transition.Start(TimeSpan.Zero, TimeSpan.FromHours(1), 30);

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingNegativeTransitionSpeed_WhenStarting_ThenCurrentTimeIsEndTime()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = -1;
        TimeSpan endTime = TimeSpan.FromHours(12);

        transition.Start(TimeSpan.Zero, endTime, 30);

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingPositiveTransitionSpeed_WhenStarting_ThenIsRunningIsTrue()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 1;

        transition.Start(TimeSpan.Zero, TimeSpan.FromHours(1), 30);

        Assert.True(transition.IsRunning);
    }
}
