using System.ComponentModel;

namespace DustInTheWind.ClockWpf.TimeProviders;

public class StaticTimeProvider : TimeProviderBase
{
    #region Time Property

    private TimeSpan time = DateTime.Now.TimeOfDay;

    [Category("Behavior")]
    [Description("The desired time value to be returned.")]
    public TimeSpan Time
    {
        get => time;
        set
        {
            Stop();
            time = value;
            Start();
        }
    }

    #endregion

    protected override TimeSpan GetTime()
    {
        return time;
    }
}
