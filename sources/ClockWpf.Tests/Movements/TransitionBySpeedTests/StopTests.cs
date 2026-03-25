using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionBySpeedTests;

public class StopTests
{
    [Fact]
    public void HavingRunningTransition_WhenStopping_ThenIsRunningIsFalse()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 1;
        transition.Start(TimeSpan.Zero, TimeSpan.FromHours(1), 30);

        transition.Stop();

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingRunningTransition_WhenStopping_ThenCurrentTimeIsEndTime()
    {
        TimeSpan endTime = TimeSpan.FromHours(12);
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 1;
        transition.Start(TimeSpan.Zero, endTime, 30);

        transition.Stop();

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingStoppedTransition_WhenStopping_ThenIsRunningRemainsIsFalse()
    {
        using TransitionBySpeed transition = new(_ => { });

        transition.Stop();

        Assert.False(transition.IsRunning);
    }
}
