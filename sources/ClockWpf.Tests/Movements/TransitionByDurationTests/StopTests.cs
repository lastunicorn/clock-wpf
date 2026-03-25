using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class StopTests
{
    [Fact]
    public void HavingRunningTransition_WhenStopping_ThenIsRunningIsFalse()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);
        transition.Start(TimeSpan.Zero, TimeSpan.FromHours(1), 30);

        transition.Stop();

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingRunningTransition_WhenStopping_ThenCurrentTimeIsEndTime()
    {
        TimeSpan endTime = TimeSpan.FromHours(12);
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);
        transition.Start(TimeSpan.Zero, endTime, 30);

        transition.Stop();

        Assert.Equal(endTime, transition.CurrentTime);
    }

    [Fact]
    public void HavingStoppedTransition_WhenStopping_ThenIsRunningRemainsIsFalse()
    {
        using TransitionByDuration transition = new(_ => { });

        transition.Stop();

        Assert.False(transition.IsRunning);
    }
}
