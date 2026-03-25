using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.UtcTimeMovementTests;

public class GenerateNewTimeTests
{
    // --- No offset, TickInterval = 0 ---

    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenLastTickIsCloseToUtcTimeOfDay()
    {
        using UtcTimeMovement movement = new();
        movement.TickInterval = 0;

        TimeSpan timeBefore = DateTime.UtcNow.TimeOfDay;
        movement.Start();
        TimeSpan timeAfter = DateTime.UtcNow.TimeOfDay;

        Assert.InRange(movement.LastTick, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenTickEventTimeIsCloseToUtcTimeOfDay()
    {
        using UtcTimeMovement movement = new();
        movement.TickInterval = 0;
        TimeSpan? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        TimeSpan timeBefore = DateTime.UtcNow.TimeOfDay;
        movement.Start();
        TimeSpan timeAfter = DateTime.UtcNow.TimeOfDay;

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStartingTwice_ThenSecondLastTickIsGreaterOrEqualToFirst()
    {
        using UtcTimeMovement movement = new();
        movement.TickInterval = 0;

        movement.Start();
        TimeSpan firstLastTick = movement.LastTick;

        Thread.Sleep(10);

        movement.Start();
        TimeSpan secondLastTick = movement.LastTick;

        Assert.True(secondLastTick >= firstLastTick);
    }

    // --- No offset, default tick interval ---

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenLastTickIsCloseToUtcTimeOfDay()
    {
        using UtcTimeMovement movement = new();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        TimeSpan timeBefore = DateTime.UtcNow.TimeOfDay;
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeSpan timeAfter = DateTime.UtcNow.TimeOfDay;

        Assert.InRange(movement.LastTick, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenTickEventTimeIsCloseToUtcTimeOfDay()
    {
        using UtcTimeMovement movement = new();
        TimeSpan? receivedTime = null;
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) =>
        {
            receivedTime = e.Time;
            tickReceived.Set();
        };

        TimeSpan timeBefore = DateTime.UtcNow.TimeOfDay;
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeSpan timeAfter = DateTime.UtcNow.TimeOfDay;

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, timeBefore, timeAfter);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStartingTwice_ThenSecondLastTickIsGreaterOrEqualToFirst()
    {
        using UtcTimeMovement movement = new();
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

    // --- With offset ---

    [Fact]
    public void HavingPositiveUtcOffset_HavingTickIntervalZero_WhenStarting_ThenLastTickIsCloseToOffsetUtcTime()
    {
        using UtcTimeMovement movement = new();
        movement.TickInterval = 0;
        movement.UtcOffset = TimeSpan.FromHours(2);

        TimeSpan expectedBefore = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(2);
        movement.Start();
        TimeSpan expectedAfter = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(2);

        Assert.InRange(movement.LastTick, expectedBefore, expectedAfter);
    }

    [Fact]
    public void HavingPositiveUtcOffset_HavingTickIntervalZero_WhenStarting_ThenTickEventTimeIsCloseToOffsetUtcTime()
    {
        using UtcTimeMovement movement = new();
        movement.TickInterval = 0;
        movement.UtcOffset = TimeSpan.FromHours(2);
        TimeSpan? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        TimeSpan expectedBefore = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(2);
        movement.Start();
        TimeSpan expectedAfter = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(2);

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, expectedBefore, expectedAfter);
    }

    [Fact]
    public void HavingNegativeUtcOffset_HavingTickIntervalZero_WhenStarting_ThenLastTickIsCloseToOffsetUtcTime()
    {
        using UtcTimeMovement movement = new();
        movement.TickInterval = 0;
        movement.UtcOffset = TimeSpan.FromHours(-2);

        TimeSpan expectedBefore = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(-2);
        movement.Start();
        TimeSpan expectedAfter = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(-2);

        Assert.InRange(movement.LastTick, expectedBefore, expectedAfter);
    }
}
