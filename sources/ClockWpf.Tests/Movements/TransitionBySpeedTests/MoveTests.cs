using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionBySpeedTests;

public class MoveTests
{
    [Fact]
    public void HavingForwardTransition_WhenMoving_ThenCurrentTimeIsGreaterThanOrEqualToStartTime()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 1;
        TimeSpan startTime = TimeSpan.FromHours(0);
        TimeSpan endTime = TimeSpan.FromHours(12);
        transition.Start(startTime, endTime, 30);

        transition.Move();

        Assert.True(transition.CurrentTime >= startTime);
    }

    [Fact]
    public void HavingBackwardTransition_WhenMoving_ThenCurrentTimeIsLessThanOrEqualToStartTime()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 1;
        TimeSpan startTime = TimeSpan.FromHours(12);
        TimeSpan endTime = TimeSpan.FromHours(0);
        transition.Start(startTime, endTime, 30);

        transition.Move();

        Assert.True(transition.CurrentTime <= startTime);
    }

    [Fact]
    public void HavingEqualStartAndEndTime_WhenMoving_ThenIsRunningIsFalse()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 1;
        TimeSpan time = TimeSpan.FromHours(6);
        transition.Start(time, time, 30);

        transition.Move();

        Assert.False(transition.IsRunning);
    }

    [Fact]
    public void HavingEqualStartAndEndTime_WhenMoving_ThenCurrentTimeIsEndTime()
    {
        using TransitionBySpeed transition = new(_ => { });
        transition.TransitionSpeed = 1;
        TimeSpan time = TimeSpan.FromHours(6);
        transition.Start(time, time, 30);

        transition.Move();

        Assert.Equal(time, transition.CurrentTime);
    }
}
