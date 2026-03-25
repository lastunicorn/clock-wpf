using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.SpeedyMovementTests;

public class InitialTimeTests
{
    [Fact]
    public void HavingNewValue_WhenSettingInitialTime_ThenInitialTimeIsUpdated()
    {
        using SpeedyMovement movement = new();

        movement.InitialTime = new TimeSpan(10, 30, 0);

        Assert.Equal(new TimeSpan(10, 30, 0), movement.InitialTime);
    }

    [Fact]
    public void HavingNewValue_WhenSettingInitialTime_ThenModifiedEventIsRaised()
    {
        using SpeedyMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.InitialTime = new TimeSpan(10, 30, 0);

        Assert.True(modifiedRaised);
    }

    [Fact]
    public void HavingTimeSpeedZeroAndNewValue_WhenSettingInitialTime_ThenLastTickEqualsNewInitialTime()
    {
        using SpeedyMovement movement = new();
        movement.TimeSpeed = 0;

        movement.InitialTime = new TimeSpan(10, 30, 0);

        Assert.Equal(new TimeSpan(10, 30, 0), movement.LastTick);
    }

    [Fact]
    public void HavingSameValue_WhenSettingInitialTime_ThenModifiedEventIsNotRaised()
    {
        using SpeedyMovement movement = new();
        movement.InitialTime = new TimeSpan(10, 30, 0);
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.InitialTime = new TimeSpan(10, 30, 0);

        Assert.False(modifiedRaised);
    }
}
