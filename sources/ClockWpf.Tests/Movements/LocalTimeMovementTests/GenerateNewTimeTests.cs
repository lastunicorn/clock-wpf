using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.LocalTimeMovementTests;

public class GenerateNewTimeTests
{
    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenLastTickIsCloseToLocalTimeOfDay()
    {
        using LocalTimeMovement movement = new();
        movement.TickInterval = 0;

        TimeSpan timeBefore = DateTime.Now.TimeOfDay;
        movement.Start();
        TimeSpan timeAfter = DateTime.Now.TimeOfDay;

        Assert.InRange(movement.LastTick, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenTickEventTimeIsCloseToLocalTimeOfDay()
    {
        using LocalTimeMovement movement = new();
        movement.TickInterval = 0;
        TimeSpan? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        TimeSpan timeBefore = DateTime.Now.TimeOfDay;
        movement.Start();
        TimeSpan timeAfter = DateTime.Now.TimeOfDay;

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStartingTwice_ThenSecondLastTickIsGreaterOrEqualToFirst()
    {
        using LocalTimeMovement movement = new();
        movement.TickInterval = 0;

        movement.Start();
        TimeSpan firstLastTick = movement.LastTick;

        Thread.Sleep(10);

        movement.Start();
        TimeSpan secondLastTick = movement.LastTick;

        Assert.True(secondLastTick >= firstLastTick);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenLastTickIsCloseToLocalTimeOfDay()
    {
        using LocalTimeMovement movement = new();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        TimeSpan timeBefore = DateTime.Now.TimeOfDay;
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeSpan timeAfter = DateTime.Now.TimeOfDay;

        Assert.InRange(movement.LastTick, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenTickEventTimeIsCloseToLocalTimeOfDay()
    {
        using LocalTimeMovement movement = new();
        TimeSpan? receivedTime = null;
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) =>
        {
            receivedTime = e.Time;
            tickReceived.Set();
        };

        TimeSpan timeBefore = DateTime.Now.TimeOfDay;
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeSpan timeAfter = DateTime.Now.TimeOfDay;

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
        TimeSpan firstLastTick = movement.LastTick;

        tickReceived.Reset();
        Thread.Sleep(10);
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeSpan secondLastTick = movement.LastTick;

        Assert.True(secondLastTick >= firstLastTick);
    }
}
