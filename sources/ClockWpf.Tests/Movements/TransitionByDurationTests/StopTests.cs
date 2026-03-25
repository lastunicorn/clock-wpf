using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class StopTests
{
    [Fact]
    public void HavingRunningTransition_WhenStopping_ThenIsRunningIsFalse()
    {
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);
        transition.Start(TimeOnly.MinValue, new TimeOnly(1, 0, 0), 30);

        transition.Stop();

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingRunningTransition_WhenStopping_ThenCurrentTimeIsEndTime()
    {
        TimeOnly endTime = new TimeOnly(12, 0, 0);
        using TransitionByDuration transition = new(_ => { });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);
        transition.Start(TimeOnly.MinValue, endTime, 30);

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
