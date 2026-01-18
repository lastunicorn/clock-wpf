namespace DustInTheWind.ClockWpf.TimeProviders;

/// <summary>
/// Contains the event data for the <see cref="ITimeProvider.TimeChanged"/> event.
/// </summary>
public class TimeChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the time value.
    /// </summary>
    public TimeSpan Time { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeChangedEventArgs"/> class.
    /// </summary>
    /// <param name="time">The time value.</param>
    public TimeChangedEventArgs(TimeSpan time)
    {
        Time = time;
    }
}
