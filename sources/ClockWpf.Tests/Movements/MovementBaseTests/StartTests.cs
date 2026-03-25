namespace DustInTheWind.ClockWpf.Tests.Movements.MovementBaseTests;

public class StartTests
{
    [Fact]
    public void HavingPositiveTickInterval_WhenStarting_ThenIsRunningIsTrue()
    {
        using TestableMovementBase movement = new();
        movement.TickInterval = 100;

        movement.Start();

        Assert.True(movement.IsRunning);
    }

    [Fact]
    public void HavingNonPositiveTickInterval_WhenStarting_ThenIsRunningIsTrue()
    {
        using TestableMovementBase movement = new();
        movement.TickInterval = 0;

        movement.Start();

        Assert.True(movement.IsRunning);
    }

    [Fact]
    public void HavingNonPositiveTickInterval_WhenStarting_ThenTickEventIsRaised()
    {
        using TestableMovementBase movement = new();
        movement.TickInterval = 0;
        bool tickEventRaised = false;
        movement.Tick += (s, e) => tickEventRaised = true;

        movement.Start();

        Assert.True(tickEventRaised);
    }

    [Fact]
    public void HavingNonPositiveTickInterval_WhenStarting_ThenTickEventArgsContainsCorrectTime()
    {
        TimeOnly expectedTime = new(10, 30, 0);
        using TestableMovementBase movement = new();
        movement.TimeToReturn = expectedTime;
        movement.TickInterval = 0;
        TimeOnly? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        movement.Start();

        Assert.Equal(expectedTime, receivedTime);
    }

    [Fact]
    public void HavingNonPositiveTickInterval_WhenStarting_ThenLastTickIsUpdated()
    {
        TimeOnly expectedTime = new(10, 30, 0);
        using TestableMovementBase movement = new();
        movement.TimeToReturn = expectedTime;
        movement.TickInterval = 0;

        movement.Start();

        Assert.Equal(expectedTime, movement.LastTick);
    }

    [Fact]
    public void HavingPositiveTickInterval_WhenStarting_ThenTickEventIsEventuallyRaised()
    {
        using TestableMovementBase movement = new();
        movement.TickInterval = 50;
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        movement.Start();

        bool wasSignaled = tickReceived.Wait(TimeSpan.FromSeconds(1));
        Assert.True(wasSignaled);
    }

    [Fact]
    public void HavingPositiveTickInterval_WhenStarting_ThenLastTickIsEventuallyUpdated()
    {
        TimeOnly expectedTime = new(10, 30, 0);
        using TestableMovementBase movement = new();
        movement.TimeToReturn = expectedTime;
        movement.TickInterval = 50;
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        movement.Start();
        tickReceived.Wait(TimeSpan.FromSeconds(1));

        Assert.Equal(expectedTime, movement.LastTick);
    }
}
