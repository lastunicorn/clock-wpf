using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.UtcTimeMovementTests;

public class ConstructorTests
{
    [Fact]
    public void WhenCreatingInstance_ThenUtcOffsetIsZero()
    {
        using UtcTimeMovement movement = new();

        Assert.Equal(TimeSpan.Zero, movement.UtcOffset);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeExists()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(UtcTimeMovement), typeof(MovementAttribute));

        Assert.NotNull(attribute);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeNameIsUtc()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(UtcTimeMovement), typeof(MovementAttribute));

        Assert.Equal("UTC", attribute.Name);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeDescriptionIsCorrect()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(UtcTimeMovement), typeof(MovementAttribute));

        Assert.NotEmpty(attribute.Description);
    }
}
