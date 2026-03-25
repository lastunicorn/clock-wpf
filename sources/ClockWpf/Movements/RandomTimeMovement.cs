namespace DustInTheWind.ClockWpf.Movements;

/// <summary>
/// Provides random time values.
/// </summary>
[Movement("Random", "Provides random time values.")]
public class RandomTimeMovement : MovementBase
{
    /// <summary>
    /// Returns a random time value.
    /// </summary>
    /// <returns>A <see cref="TimeOnly"/> object containing a random time value.</returns>
    protected override TimeOnly GenerateNewTime()
    {
        long ticks = Random.Shared.NextInt64(TimeSpan.TicksPerDay);
        return new TimeOnly(ticks);
    }
}
