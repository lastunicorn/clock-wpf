using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.StaticMovementTests;

public class TickIntervalTests
{
    [Fact]
    public void WhenSettingTickIntervalToNonZero_ThenTickIntervalRemainsZero()
    {
        using StaticMovement movement = new();

        movement.TickInterval = 100;

        Assert.Equal(0, movement.TickInterval);
    }
}
