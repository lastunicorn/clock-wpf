using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.MovementBaseTests;

internal class TestableMovementBase : MovementBase
{
    public TimeOnly TimeToReturn { get; set; } = TimeOnly.MinValue;

    protected override TimeOnly GenerateNewTime()
    {
        return TimeToReturn;
    }
}
