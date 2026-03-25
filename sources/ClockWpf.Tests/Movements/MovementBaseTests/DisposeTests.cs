namespace DustInTheWind.ClockWpf.Tests.Movements.MovementBaseTests;

public class DisposeTests
{
    [Fact]
    public void HavingRunningMovement_WhenDisposing_ThenIsRunningIsFalse()
    {
        TestableMovementBase movement = new();
        movement.Start();

        movement.Dispose();

        Assert.False(movement.IsRunning);
    }

    [Fact]
    public void WhenDisposingTwice_ThenNoExceptionIsThrown()
    {
        TestableMovementBase movement = new();
        movement.Dispose();

        movement.Dispose();
    }
}
