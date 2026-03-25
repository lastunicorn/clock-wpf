using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.StaticMovementTests;

public class TransitionTickIntervalTests
{
    [Fact]
    public void WhenSettingNewValue_ThenTransitionTickIntervalReturnsSetValue()
    {
        using StaticMovement movement = new();

        movement.TransitionTickInterval = 50;

        Assert.Equal(50, movement.TransitionTickInterval);
    }

    [Fact]
    public void WhenSettingSameValue_ThenModifiedEventIsNotRaised()
    {
        using StaticMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TransitionTickInterval = 30;

        Assert.False(modifiedRaised);
    }

    [Fact]
    public void WhenSettingNewValue_ThenModifiedEventIsRaised()
    {
        using StaticMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TransitionTickInterval = 50;

        Assert.True(modifiedRaised);
    }
}
