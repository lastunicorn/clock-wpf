using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.StaticMovementTests;

public class TransitionDurationTests
{
    [Fact]
    public void WhenSettingPositiveValue_ThenTransitionDurationReturnsSetValue()
    {
        using StaticMovement movement = new();

        movement.TransitionDuration = TimeSpan.FromSeconds(2);

        Assert.Equal(TimeSpan.FromSeconds(2), movement.TransitionDuration);
    }

    [Fact]
    public void WhenSettingNegativeValue_ThenTransitionDurationIsResetToZero()
    {
        using StaticMovement movement = new();

        movement.TransitionDuration = TimeSpan.FromSeconds(-1);

        Assert.Equal(TimeSpan.Zero, movement.TransitionDuration);
    }

    [Fact]
    public void WhenSettingNewValue_ThenModifiedEventIsRaised()
    {
        using StaticMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TransitionDuration = TimeSpan.FromSeconds(2);

        Assert.True(modifiedRaised);
    }
}
