using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.StaticMovementTests;
public class ConstructorTests
{
    [Fact]
    public void WhenCreatingInstance_ThenTickIntervalIsZero()
    {
        using StaticMovement movement = new();

        Assert.Equal(0, movement.TickInterval);
    }

    [Fact]
    public void WhenCreatingInstance_ThenTimeIsCloseToLocalTimeOfDay()
    {
        TimeOnly timeBefore = TimeOnly.FromDateTime(DateTime.Now);
        using StaticMovement movement = new();
        TimeOnly timeAfter = TimeOnly.FromDateTime(DateTime.Now);

        Assert.InRange(movement.Time, timeBefore, timeAfter);
    }

    [Fact]
    public void WhenCreatingInstance_ThenTransitionDurationIsZero()
    {
        using StaticMovement movement = new();

        Assert.Equal(TimeSpan.Zero, movement.TransitionDuration);
    }

    [Fact]
    public void WhenCreatingInstance_ThenTransitionSpeedIsZero()
    {
        using StaticMovement movement = new();

        Assert.Equal(0, movement.TransitionSpeed);
    }

    [Fact]
    public void WhenCreatingInstance_ThenTransitionTickIntervalIs30()
    {
        using StaticMovement movement = new();

        Assert.Equal(30, movement.TransitionTickInterval);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeExists()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(StaticMovement), typeof(MovementAttribute));

        Assert.NotNull(attribute);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeNameIsStatic()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(StaticMovement), typeof(MovementAttribute));

        Assert.Equal("Static", attribute.Name);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeDescriptionIsCorrect()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(StaticMovement), typeof(MovementAttribute));

        Assert.NotEmpty(attribute.Description);
    }
}
