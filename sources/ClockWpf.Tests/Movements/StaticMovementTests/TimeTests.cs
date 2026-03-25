using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.StaticMovementTests;

public class TimeTests
{
    [Fact]
    public void WhenSettingTime_ThenTimeReturnsSetValue()
    {
        using StaticMovement movement = new();

        movement.Time = new TimeOnly(10, 30, 0);

        Assert.Equal(new TimeOnly(10, 30, 0), movement.Time);
    }

    [Fact]
    public void WhenSettingSameTime_ThenModifiedEventIsNotRaised()
    {
        using StaticMovement movement = new();
        TimeOnly currentTime = movement.Time;
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.Time = currentTime;

        Assert.False(modifiedRaised);
    }

    [Fact]
    public void WhenSettingNewTime_ThenModifiedEventIsRaised()
    {
        using StaticMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.Time = new TimeOnly(10, 30, 0);

        Assert.True(modifiedRaised);
    }

    [Fact]
    public void WhenSettingNewTime_ThenLastTickIsUpdatedImmediately()
    {
        using StaticMovement movement = new();

        movement.Time = new TimeOnly(10, 30, 0);

        Assert.Equal(new TimeOnly(10, 30, 0), movement.LastTick);
    }

    [Fact]
    public void WhenSettingNewTime_ThenTickEventIsRaisedWithNewTime()
    {
        using StaticMovement movement = new();
        TimeOnly? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        movement.Time = new TimeOnly(10, 30, 0);

        Assert.NotNull(receivedTime);
        Assert.Equal(new TimeOnly(10, 30, 0), receivedTime.Value);
    }
}
