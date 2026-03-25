using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.LocalTimeMovementTests;

public class GenerateNewTimeTests
{
    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenLastTickIsCloseToLocalTimeOfDay()
    {
        using LocalTimeMovement movement = new();
        movement.TickInterval = 0;

        TimeOnly timeBefore = TimeOnly.FromDateTime(DateTime.Now);
        movement.Start();
        TimeOnly timeAfter = TimeOnly.FromDateTime(DateTime.Now);

        Assert.InRange(movement.LastTick, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenTickEventTimeIsCloseToLocalTimeOfDay()
    {
        using LocalTimeMovement movement = new();
        movement.TickInterval = 0;
        TimeOnly? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        TimeOnly timeBefore = TimeOnly.FromDateTime(DateTime.Now);
        movement.Start();
        TimeOnly timeAfter = TimeOnly.FromDateTime(DateTime.Now);

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStartingTwice_ThenSecondLastTickIsGreaterOrEqualToFirst()
    {
        using LocalTimeMovement movement = new();
        movement.TickInterval = 0;

        movement.Start();
        TimeOnly firstLastTick = movement.LastTick;

        Thread.Sleep(10);

        movement.Start();
        TimeOnly secondLastTick = movement.LastTick;

        Assert.True(secondLastTick >= firstLastTick);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenLastTickIsCloseToLocalTimeOfDay()
    {
        using LocalTimeMovement movement = new();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        TimeOnly timeBefore = TimeOnly.FromDateTime(DateTime.Now);
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeOnly timeAfter = TimeOnly.FromDateTime(DateTime.Now);

        Assert.InRange(movement.LastTick, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenTickEventTimeIsCloseToLocalTimeOfDay()
    {
        using LocalTimeMovement movement = new();
        TimeOnly? receivedTime = null;
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) =>
        {
            receivedTime = e.Time;
            tickReceived.Set();
        };

        TimeOnly timeBefore = TimeOnly.FromDateTime(DateTime.Now);
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeOnly timeAfter = TimeOnly.FromDateTime(DateTime.Now);

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStartingTwice_ThenSecondLastTickIsGreaterOrEqualToFirst()
    {
        using LocalTimeMovement movement = new();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeOnly firstLastTick = movement.LastTick;

        tickReceived.Reset();
        Thread.Sleep(10);
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeOnly secondLastTick = movement.LastTick;

        Assert.True(secondLastTick >= firstLastTick);
    }
}
