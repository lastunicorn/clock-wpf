using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.StaticMovementTests;

public class TransitionSpeedTests
{
    [Fact]
    public void WhenSettingNewValue_ThenTransitionSpeedReturnsSetValue()
    {
        using StaticMovement movement = new();

        movement.TransitionSpeed = 2.5;

        Assert.Equal(2.5, movement.TransitionSpeed);
    }

    [Fact]
    public void WhenSettingSameValue_ThenModifiedEventIsNotRaised()
    {
        using StaticMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TransitionSpeed = 0;

        Assert.False(modifiedRaised);
    }

    [Fact]
    public void WhenSettingNewValue_ThenModifiedEventIsRaised()
    {
        using StaticMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TransitionSpeed = 2.5;

        Assert.True(modifiedRaised);
    }
}
