using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.MovementBaseTests;

internal class TestableMovementBase : MovementBase
{
    public TimeSpan TimeToReturn { get; set; } = TimeSpan.Zero;

    protected override TimeSpan GenerateNewTime()
    {
        return TimeToReturn;
    }
}
