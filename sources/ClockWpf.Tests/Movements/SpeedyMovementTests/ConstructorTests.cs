using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.SpeedyMovementTests;

public class ConstructorTests
{
    [Fact]
    public void WhenCreatingInstance_ThenTimeSpeedIsDefaultTimeSpeed()
    {
        using SpeedyMovement movement = new();

        Assert.Equal(SpeedyMovement.DefaultTimeSpeed, movement.TimeSpeed);
    }

    [Fact]
    public void WhenCreatingInstance_ThenInitialTimeIsCloseToLocalTimeOfDay()
    {
        TimeOnly timeBefore = TimeOnly.FromDateTime(DateTime.Now);
        using SpeedyMovement movement = new();
        TimeOnly timeAfter = TimeOnly.FromDateTime(DateTime.Now);

        Assert.InRange(movement.InitialTime, timeBefore, timeAfter);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeExists()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(SpeedyMovement), typeof(MovementAttribute));

        Assert.NotNull(attribute);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeNameIsSpeedy()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(SpeedyMovement), typeof(MovementAttribute));

        Assert.Equal("Speedy", attribute.Name);
    }

    [Fact]
    public void WhenInspectingType_ThenMovementAttributeDescriptionIsCorrect()
    {
        MovementAttribute attribute = (MovementAttribute)Attribute.GetCustomAttribute(typeof(SpeedyMovement), typeof(MovementAttribute));

        Assert.NotEmpty(attribute.Description);
    }
}
