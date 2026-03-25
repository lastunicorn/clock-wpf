using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.SpeedyMovementTests;

public class TimeSpeedTests
{
    [Fact]
    public void HavingNewValue_WhenSettingTimeSpeed_ThenTimeSpeedIsUpdated()
    {
        using SpeedyMovement movement = new();

        movement.TimeSpeed = 2;

        Assert.Equal(2f, movement.TimeSpeed);
    }

    [Fact]
    public void HavingNewValue_WhenSettingTimeSpeed_ThenModifiedEventIsRaised()
    {
        using SpeedyMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TimeSpeed = 2;

        Assert.True(modifiedRaised);
    }

    [Fact]
    public void HavingSameValue_WhenSettingTimeSpeed_ThenModifiedEventIsNotRaised()
    {
        using SpeedyMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.TimeSpeed = SpeedyMovement.DefaultTimeSpeed;

        Assert.False(modifiedRaised);
    }
}
