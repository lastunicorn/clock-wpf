using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.RandomTimeMovementTests;

public class ConstructorTests
{
    [Fact]
    public void WhenInspectingType_ThenMovementAttributeExists()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(RandomTimeMovement), typeof(MovementAttribute));

        Assert.NotNull(attribute);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeNameIsRandom()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(RandomTimeMovement), typeof(MovementAttribute));

        Assert.Equal("Random", attribute.Name);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeDescriptionIsCorrect()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(RandomTimeMovement), typeof(MovementAttribute));

        Assert.NotEmpty(attribute.Description);
    }
}
