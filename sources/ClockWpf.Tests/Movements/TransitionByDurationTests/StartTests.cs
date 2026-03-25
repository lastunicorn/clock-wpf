using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class StartTests
{
    [Fact]
    public void HavingZeroTransitionDuration_WhenStarting_ThenIsRunningIsFalse()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.Zero;

        transition.Start(TimeSpan.Zero, TimeSpan.FromHours(1), 30);

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingZeroTransitionDuration_WhenStarting_ThenCurrentTimeIsEndTime()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.Zero;
        TimeSpan endTime = TimeSpan.FromHours(12);

        transition.Start(TimeSpan.Zero, endTime, 30);

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingNegativeTransitionDuration_WhenStarting_ThenIsRunningIsFalse()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(-1);

        transition.Start(TimeSpan.Zero, TimeSpan.FromHours(1), 30);

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingNegativeTransitionDuration_WhenStarting_ThenCurrentTimeIsEndTime()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(-1);
        TimeSpan endTime = TimeSpan.FromHours(12);

        transition.Start(TimeSpan.Zero, endTime, 30);

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingPositiveTransitionDuration_WhenStarting_ThenIsRunningIsTrue()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);

        transition.Start(TimeSpan.Zero, TimeSpan.FromHours(1), 30);

        Assert.True(transition.IsRunning);
    }
}
