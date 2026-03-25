using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.StaticMovementTests;

public class GenerateNewTimeTests
{
    [Fact]
    public void HavingSpecificTimeSet_WhenStarting_ThenLastTickEqualsSetTime()
    {
        using StaticMovement movement = new();
        movement.Time = new TimeOnly(10, 30, 0);

        movement.Start();

        Assert.Equal(new TimeOnly(10, 30, 0), movement.LastTick);
    }

    [Fact]
    public void HavingSpecificTimeSet_WhenStarting_ThenTickEventTimeEqualsSetTime()
    {
        using StaticMovement movement = new();
        movement.Time = new TimeOnly(10, 30, 0);
        TimeOnly? receivedTime = null;
        movement.Tick += (s, e) => receivedTime = e.Time;

        movement.Start();

        Assert.NotNull(receivedTime);
        Assert.Equal(new TimeOnly(10, 30, 0), receivedTime.Value);
    }

    [Fact]
    public void HavingTimeChangedBetweenStarts_WhenStartingAgain_ThenLastTickEqualsNewTime()
    {
        using StaticMovement movement = new();
        movement.Time = new TimeOnly(10, 30, 0);
        movement.Start();

        movement.Time = new TimeOnly(15, 45, 0);
        movement.Start();

        Assert.Equal(new TimeOnly(15, 45, 0), movement.LastTick);
    }
}
