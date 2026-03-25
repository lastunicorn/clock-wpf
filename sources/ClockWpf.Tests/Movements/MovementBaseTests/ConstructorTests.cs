namespace DustInTheWind.ClockWpf.Tests.Movements.MovementBaseTests;

public class ConstructorTests
{
    [Fact]
    public void WhenCreatingInstance_ThenIsRunningIsFalse()
    {
        using TestableMovementBase movement = new();

        Assert.False(movement.IsRunning);
    }

    [Fact]
    public void WhenCreatingInstance_ThenLastTickIsZero()
    {
        using TestableMovementBase movement = new();

        Assert.Equal(TimeSpan.Zero, movement.LastTick);
    }

    [Fact]
    public void WhenCreatingInstance_ThenTickIntervalIs100()
    {
        using TestableMovementBase movement = new();

        Assert.Equal(100, movement.TickInterval);
    }
}
