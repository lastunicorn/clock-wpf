namespace DustInTheWind.ClockWpf.Tests.Movements.MovementBaseTests;

public class TickIntervalTests
{
    [Fact]
    public void HavingDefaultTickInterval_WhenSettingNewValue_ThenTickIntervalReturnsNewValue()
    {
        using TestableMovementBase movement = new();

        movement.TickInterval = 200;

        Assert.Equal(200, movement.TickInterval);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenSettingNewValue_ThenModifiedEventIsRaised()
    {
        using TestableMovementBase movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TickInterval = 200;

        Assert.True(modifiedRaised);
    }

    [Fact]
    public void HavingDefaultTickInterval_WhenSettingSameValue_ThenModifiedEventIsNotRaised()
    {
        using TestableMovementBase movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TickInterval = 100;

        Assert.False(modifiedRaised);
    }

    [Fact]
    public void HavingRunningMovement_WhenSettingNewPositiveValue_ThenModifiedEventIsRaised()
    {
        using TestableMovementBase movement = new();
        movement.Start();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TickInterval = 200;

        Assert.True(modifiedRaised);
    }

    [Fact]
    public void HavingRunningMovementWithNonPositiveTickInterval_WhenSettingPositiveTickInterval_ThenTickEventIsEventuallyRaised()
    {
        using TestableMovementBase movement = new();
        movement.TickInterval = 0;
        movement.Start();
        using ManualResetEventSlim tickReceived = new(false);
        movement.Tick += (s, e) => tickReceived.Set();

        movement.TickInterval = 50;

        bool wasSignaled = tickReceived.Wait(TimeSpan.FromSeconds(1));
        Assert.True(wasSignaled);
    }
}
