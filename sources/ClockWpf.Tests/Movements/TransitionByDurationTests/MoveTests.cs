using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class MoveTests
{
    [Fact]
    public void HavingForwardTransition_WhenCallbackFires_ThenTimeIsGreaterThanOrEqualToStartTime()
    {
        using ManualResetEventSlim callbackFired = new(false);
        TimeSpan startTime = TimeSpan.FromHours(0);
        TimeSpan endTime = TimeSpan.FromHours(12);
        TimeSpan capturedTime = default;
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
        TimeSpan startTime = TimeSpan.FromHours(12);
        TimeSpan endTime = TimeSpan.FromHours(0);
        TimeSpan capturedTime = default;
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
        TimeSpan time = TimeSpan.FromHours(6);
        TimeSpan capturedTime = default;
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
        TimeSpan endTime = TimeSpan.FromHours(12);
        using ManualResetEventSlim completed = new(false);
        using TransitionByDuration transition = new(time =>
        {
            if (time == endTime)
                completed.Set();
        });
        transition.TransitionDuration = TimeSpan.FromMilliseconds(50);
        transition.Start(TimeSpan.FromHours(0), endTime, 10);

        completed.Wait(TimeSpan.FromSeconds(2));

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingTransitionDurationElapsed_WhenMoving_ThenCurrentTimeIsEndTime()
    {
        TimeSpan endTime = TimeSpan.FromHours(12);
        using ManualResetEventSlim completed = new(false);
        using TransitionByDuration transition = new(time =>
        {
            if (time == endTime)
                completed.Set();
        });
        transition.TransitionDuration = TimeSpan.FromMilliseconds(50);
        transition.Start(TimeSpan.FromHours(0), endTime, 10);

        completed.Wait(TimeSpan.FromSeconds(2));

        Assert.Equal(endTime, transition.CurrentTime);
    }
}
