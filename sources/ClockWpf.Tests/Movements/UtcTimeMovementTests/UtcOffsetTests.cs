using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.UtcTimeMovementTests;

public class UtcOffsetTests
{
    [Fact]
    public void WhenSettingNewValue_ThenUtcOffsetReturnsSetValue()
    {
        using UtcTimeMovement movement = new();

        movement.UtcOffset = TimeSpan.FromHours(2);

        Assert.Equal(TimeSpan.FromHours(2), movement.UtcOffset);
    }

    [Fact]
    public void WhenSettingSameValue_ThenModifiedEventIsNotRaised()
    {
        using UtcTimeMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.UtcOffset = TimeSpan.Zero;

        Assert.False(modifiedRaised);
    }

    [Fact]
    public void WhenSettingNewValue_ThenModifiedEventIsRaised()
    {
        using UtcTimeMovement movement = new();
        bool modifiedRaised = false;
        movement.Modified += (s, e) => modifiedRaised = true;

        movement.UtcOffset = TimeSpan.FromHours(2);

        Assert.True(modifiedRaised);
    }

    [Fact]
    public void WhenSettingSameValue_ThenTickEventIsNotRaised()
    {
        using UtcTimeMovement movement = new();
        bool tickRaised = false;
        movement.Tick += (s, e) => tickRaised = true;

        movement.UtcOffset = TimeSpan.Zero;

        Assert.False(tickRaised);
    }

    [Fact]
    public void WhenSettingNewOffset_ThenLastTickIsUpdatedImmediately()
    {
        using UtcTimeMovement movement = new();

        TimeSpan expectedBefore = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(2);
        movement.UtcOffset = TimeSpan.FromHours(2);
        TimeSpan expectedAfter = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(2);

        Assert.InRange(movement.LastTick, expectedBefore, expectedAfter);
    }

    [Fact]
    public void WhenSettingNewOffset_ThenTickEventTimeIsCloseToUtcTimeWithOffset()
    {
        using UtcTimeMovement movement = new();
        TimeSpan? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        TimeSpan expectedBefore = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(2);
        movement.UtcOffset = TimeSpan.FromHours(2);
        TimeSpan expectedAfter = DateTime.UtcNow.TimeOfDay + TimeSpan.FromHours(2);

        Assert.NotNull(receivedTime);
        Assert.InRange(receivedTime.Value, expectedBefore, expectedAfter);
    }
}
