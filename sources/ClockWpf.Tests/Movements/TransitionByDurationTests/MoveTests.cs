using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class MoveTests
{
    [Fact]
    public void HavingForwardTransition_WhenCallbackFires_ThenTimeIsGreaterThanOrEqualToStartTime()
    {
        using ManualResetEventSlim callbackFired = new(false);
        TimeOnly startTime = TimeOnly.MinValue;
        TimeOnly endTime = new TimeOnly(12, 0, 0);
        TimeOnly capturedTime = default;
        using TransitionByDuration transition = new(time =>
        {
            capturedTime = time;
            callbackFired.Set();
        });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);
        transition.Start(startTime, endTime, 10);

        bool fired = callbackFired.Wait(TimeSpan.FromSeconds(1));
        transition.Stop();

        Assert.True(fired);
        Assert.True(capturedTime >= startTime);
    }

    [Fact]
    public void HavingBackwardTransition_WhenCallbackFires_ThenTimeIsLessThanOrEqualToStartTime()
    {
        using ManualResetEventSlim callbackFired = new(false);
        TimeOnly startTime = new TimeOnly(12, 0, 0);
        TimeOnly endTime = TimeOnly.MinValue;
        TimeOnly capturedTime = default;
        using TransitionByDuration transition = new(time =>
        {
            capturedTime = time;
            callbackFired.Set();
        });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);
        transition.Start(startTime, endTime, 10);

        bool fired = callbackFired.Wait(TimeSpan.FromSeconds(1));
        transition.Stop();

        Assert.True(fired);
        Assert.True(capturedTime <= startTime);
    }

    [Fact]
    public void HavingEqualStartAndEndTime_WhenCallbackFires_ThenTimeEqualsEndTime()
    {
        using ManualResetEventSlim callbackFired = new(false);
        TimeOnly time = new TimeOnly(6, 0, 0);
        TimeOnly capturedTime = default;
        using TransitionByDuration transition = new(t =>
        {
            capturedTime = t;
            callbackFired.Set();
        });
        transition.TransitionDuration = TimeSpan.FromSeconds(5);
        transition.Start(time, time, 10);

        bool fired = callbackFired.Wait(TimeSpan.FromSeconds(1));
        transition.Stop();

        Assert.True(fired);
        Assert.Equal(time, capturedTime);
    }

    [Fact]
    public void HavingTransitionDurationElapsed_WhenMoving_ThenIsRunningIsFalse()
    {
        TimeOnly endTime = new TimeOnly(12, 0, 0);
        using ManualResetEventSlim completed = new(false);
        using TransitionByDuration transition = new(time =>
        {
            if (time == endTime)
                completed.Set();
        });
        transition.TransitionDuration = TimeSpan.FromMilliseconds(50);
        transition.Start(TimeOnly.MinValue, endTime, 10);

        completed.Wait(TimeSpan.FromSeconds(2));

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingTransitionDurationElapsed_WhenMoving_ThenCurrentTimeIsEndTime()
    {
        TimeOnly endTime = new TimeOnly(12, 0, 0);
        using ManualResetEventSlim completed = new(false);
        using TransitionByDuration transition = new(time =>
        {
            if (time == endTime)
                completed.Set();
        });
        transition.TransitionDuration = TimeSpan.FromMilliseconds(50);
        transition.Start(TimeOnly.MinValue, endTime, 10);

        completed.Wait(TimeSpan.FromSeconds(2));

        Assert.Equal(endTime, transition.CurrentTime);
    }
}
