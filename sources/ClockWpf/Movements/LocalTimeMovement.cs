namespace DustInTheWind.ClockWpf.Movements;

/// <summary>
/// Provides the system's local time.
/// </summary>
[Movement("Local Time", "Provides the system's local time.")]
public class LocalTimeMovement : MovementBase
{
    /// <summary>
    /// Returns the system's local time from the moment of the request.
    /// </summary>
    /// <returns>A <see cref="TimeOnly"/> object containing the time value.</returns>
    protected override TimeOnly GenerateNewTime()
    {
        return TimeOnly.FromDateTime(DateTime.Now);
    }
}
