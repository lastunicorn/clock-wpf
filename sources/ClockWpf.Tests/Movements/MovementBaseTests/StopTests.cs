namespace DustInTheWind.ClockWpf.Tests.Movements.MovementBaseTests;

public class StopTests
{
    [Fact]
    public void HavingRunningMovement_WhenStopping_ThenIsRunningIsFalse()
    {
        using TestableMovementBase movement = new();
        movement.Start();

        movement.Stop();

        Assert.False(movement.IsRunning);
    }

    [Fact]
    public void HavingStoppedMovement_WhenStopping_ThenIsRunningRemainsIsFalse()
    {
        using TestableMovementBase movement = new();

        movement.Stop();

        Assert.False(movement.IsRunning);
    }
}
