using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.SpeedyMovementTests;

public class GenerateNewTimeTests
{
    [Fact]
    public void HavingTimeSpeedZeroAndElapsedRealTime_WhenStarting_ThenLastTickEqualsInitialTime()
    {
        using SpeedyMovement movement = new();
        movement.TickInterval = 0;
        movement.TimeSpeed = 0;
        movement.InitialTime = new TimeSpan(10, 30, 0);

        Thread.Sleep(10);
        movement.Start();

        Assert.Equal(new TimeSpan(10, 30, 0), movement.LastTick);
    }

    [Fact]
    public void HavingPositiveTimeSpeedAndElapsedRealTime_WhenStarting_ThenLastTickIsAfterInitialTime()
    {
        using SpeedyMovement movement = new();
        movement.TickInterval = 0;
        movement.TimeSpeed = 1;
        movement.InitialTime = new TimeSpan(10, 30, 0);

        Thread.Sleep(10);
        movement.Start();

        Assert.True(movement.LastTick > new TimeSpan(10, 30, 0));
    }

    [Fact]
    public void HavingNegativeTimeSpeedAndElapsedRealTime_WhenStarting_ThenLastTickIsBeforeInitialTime()
    {
        using SpeedyMovement movement = new();
        movement.TickInterval = 0;
        movement.TimeSpeed = -1;
        movement.InitialTime = new TimeSpan(10, 30, 0);

        Thread.Sleep(10);
        movement.Start();

        Assert.True(movement.LastTick < new TimeSpan(10, 30, 0));
    }

    [Fact]
    public void HavingInitialTimeExceedingOneDayAndTimeSpeedZero_WhenSettingInitialTime_ThenLastTickIsWrappedWithinOneDay()
    {
        using SpeedyMovement movement = new();
        movement.TimeSpeed = 0;

        movement.InitialTime = new TimeSpan(25, 0, 0);

        Assert.Equal(new TimeSpan(1, 0, 0), movement.LastTick);
    }
}
