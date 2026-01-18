namespace DustInTheWind.ClockWpf.TimeProviders;

/// <summary>
/// Provides random time values.
/// </summary>
public class RandomTimeProvider : TimeProviderBase
{
    private readonly Random rand = new();

    /// <summary>
    /// Returns a random time value.
    /// </summary>
    /// <returns>A <see cref="TimeSpan"/> object containing a random time value.</returns>
    protected override TimeSpan GetTime()
    {
        int hours = rand.Next(23);
        int minutes = rand.Next(59);
        int seconds = rand.Next(59);

        return new TimeSpan(hours, minutes, seconds);
    }
}
