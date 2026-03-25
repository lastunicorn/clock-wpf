using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Movements;

/// <summary>
/// Provides the current UTC time. Optionally, an offset may be provided to display time from a different timezone.
/// </summary>
[Movement("UTC", "Provides the current UTC time. Optionally, an offset may be provided to display time from a different timezone.")]
public class UtcTimeMovement : MovementBase
{
    /// <summary>
    /// Gets or sets the offset time used to adjust the system's UTC time value.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(typeof(TimeSpan), "00:00:00")]
    [Description("The offset time used to adjust the system's UTC time value.")]
    public TimeSpan UtcOffset
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnModified();
            ForceTick();
        }
    }

    /// <summary>
    /// Returns the system's UTC time added with the offset value.
    /// </summary>
    /// <returns>A <see cref="TimeOnly"/> object containing the time value.</returns>
    protected override TimeOnly GenerateNewTime()
    {
        return UtcOffset == TimeSpan.Zero
            ? TimeOnly.FromDateTime(DateTime.UtcNow)
            : TimeOnly.FromDateTime(DateTime.UtcNow).Add(UtcOffset);
    }
}
