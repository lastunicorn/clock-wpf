using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.RandomTimeMovementTests;

public class GenerateNewTimeTests
{
    
    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenLastTickIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        movement.TickInterval = 0;

        movement.Start();

        Assert.InRange(movement.LastTick, TimeOnly.MinValue, TimeOnly.MaxValue);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenTickEventTimeIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        movement.TickInterval = 0;
        TimeOnly? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        movement.Start();

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, TimeOnly.MinValue, TimeOnly.MaxValue);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStartingTwice_ThenEachLastTickIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        movement.TickInterval = 0;

        movement.Start();
        TimeOnly firstLastTick = movement.LastTick;

        movement.Start();
        TimeOnly secondLastTick = movement.LastTick;

        Assert.InRange(firstLastTick, TimeOnly.MinValue, TimeOnly.MaxValue);
        Assert.InRange(secondLastTick, TimeOnly.MinValue, TimeOnly.MaxValue);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenLastTickIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));

        Assert.InRange(movement.LastTick, TimeOnly.MinValue, TimeOnly.MaxValue);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenTickEventTimeIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        TimeOnly? receivedTime = null;
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) =>
        {
            receivedTime = e.Time;
            tickReceived.Set();
        };

        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, TimeOnly.MinValue, TimeOnly.MaxValue);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStartingTwice_ThenEachLastTickIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeOnly firstLastTick = movement.LastTick;

        tickReceived.Reset();
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeOnly secondLastTick = movement.LastTick;

        Assert.InRange(firstLastTick, TimeOnly.MinValue, TimeOnly.MaxValue);
        Assert.InRange(secondLastTick, TimeOnly.MinValue, TimeOnly.MaxValue);
    }
}
