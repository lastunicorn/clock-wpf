namespace DustInTheWind.ClockWpf.TimeProviders;

/// <summary>
/// Provides time values from a time coordinate that is n times faster than the real one.
/// </summary>
public class BrokenTimeProvider : TimeProviderBase
{
    /// <summary>
    /// The default value of the time multiplier.
    /// </summary>
    public const float DefaultTimeMultiplier = 10;

    private TimeSpan initialTime = DateTime.Now.TimeOfDay;
    private DateTime initialRealTime = DateTime.UtcNow;
    private float timeMultiplier = DefaultTimeMultiplier;

    /// <summary>
    /// Gets or sets the initial time value used as a reference for calculating subsequent time values.
    /// When this value is set, it resets the time calculation and restarts the provider.
    /// </summary>
    public TimeSpan InitialTime
    {
        get => initialTime;
        set
        {
            Stop();
            initialTime = value;
            initialRealTime = DateTime.UtcNow;
            Start();
        }
    }

    /// <summary>
    /// Gets or sets the time multiplier that specifies how much faster the provided time
    /// is compared to the real one.
    /// </summary>
    public float TimeMultiplier
    {
        get => timeMultiplier;
        set
        {
            Stop();
            initialTime = GetTime();
            initialRealTime = DateTime.UtcNow;
            timeMultiplier = value;
            Start();
        }
    }

    /// <summary>
    /// Returns a new time value calculated based on the time multiplier.
    /// </summary>
    /// <returns>A <see cref="TimeSpan"/> object containing the time value.</returns>
    protected override TimeSpan GetTime()
    {
        DateTime currentRealTime = DateTime.UtcNow;
        long realDeltaTicks = currentRealTime.Ticks - initialRealTime.Ticks;
        double fakeDeltaTicks = realDeltaTicks * TimeMultiplier;
        TimeSpan fakeDelta = TimeSpan.FromTicks((long)fakeDeltaTicks);
        TimeSpan fakeTime = initialTime + fakeDelta;

        return fakeTime.Days > 0
            ? fakeTime.Subtract(TimeSpan.FromDays(fakeTime.Days))
            : fakeTime;
    }
}
