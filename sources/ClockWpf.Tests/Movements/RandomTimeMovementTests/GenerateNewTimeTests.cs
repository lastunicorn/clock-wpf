using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.RandomTimeMovementTests;

public class GenerateNewTimeTests
{
    private static readonly TimeSpan OneDayMinusOneTick = TimeSpan.FromHours(24) - TimeSpan.FromTicks(1);

    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenLastTickIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        movement.TickInterval = 0;

        movement.Start();

        Assert.InRange(movement.LastTick, TimeSpan.Zero, OneDayMinusOneTick);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStarting_ThenTickEventTimeIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        movement.TickInterval = 0;
        TimeSpan? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        movement.Start();

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, TimeSpan.Zero, OneDayMinusOneTick);
    }

    [Fact]
    public void HavingTickIntervalZero_WhenStartingTwice_ThenEachLastTickIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        movement.TickInterval = 0;

        movement.Start();
        TimeSpan firstLastTick = movement.LastTick;

        movement.Start();
        TimeSpan secondLastTick = movement.LastTick;

        Assert.InRange(firstLastTick, TimeSpan.Zero, OneDayMinusOneTick);
        Assert.InRange(secondLastTick, TimeSpan.Zero, OneDayMinusOneTick);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenLastTickIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));

        Assert.InRange(movement.LastTick, TimeSpan.Zero, OneDayMinusOneTick);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStarting_ThenTickEventTimeIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        TimeSpan? receivedTime = null;
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) =>
        {
            receivedTime = e.Time;
            tickReceived.Set();
        };

        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, TimeSpan.Zero, OneDayMinusOneTick);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenStartingTwice_ThenEachLastTickIsWithinOneDay()
    {
        using RandomTimeMovement movement = new();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeSpan firstLastTick = movement.LastTick;

        tickReceived.Reset();
        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));
        TimeSpan secondLastTick = movement.LastTick;

        Assert.InRange(firstLastTick, TimeSpan.Zero, OneDayMinusOneTick);
        Assert.InRange(secondLastTick, TimeSpan.Zero, OneDayMinusOneTick);
    }
}
