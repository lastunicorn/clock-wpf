namespace DustInTheWind.ClockWpf.TimeProviders;

/// <summary>
/// Provides the UTC time. Optionally, an offset may be provided.
/// </summary>
public class UtcTimeProvider : TimeProviderBase
{
    /// <summary>
    /// Gets or sets the offset time used to adjust the system's UTC time value.
    /// </summary>
    public TimeSpan UtcOffset { get; set; }

    /// <summary>
    /// Returns the system's UTC time added with the offset value.
    /// </summary>
    /// <returns>A <see cref="TimeSpan"/> object containing the time value.</returns>
    protected override TimeSpan GetTime()
    {
        return UtcOffset == TimeSpan.Zero
            ? DateTime.UtcNow.TimeOfDay
            : DateTime.UtcNow.TimeOfDay.Add(UtcOffset);
    }
}
