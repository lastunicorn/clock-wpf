using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.LocalTimeMovementTests;

public class ConstructorTests
{
    [Fact]
    public void WhenInspectingType_ThenMovementAttributeExists()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(LocalTimeMovement), typeof(MovementAttribute));

        Assert.NotNull(attribute);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeNameIsLocalTime()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(LocalTimeMovement), typeof(MovementAttribute));

        Assert.Equal("Local Time", attribute.Name);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeDescriptionIsCorrect()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(LocalTimeMovement), typeof(MovementAttribute));

        Assert.NotEmpty(attribute.Description);
    }
}
