using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Movements;

public class StaticMovement : MovementBase
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
            time = value;
            ForceTick();
        }
    }

    #endregion

    public StaticMovement()
    {
        TickInterval = 0;
    }

    protected override TimeSpan GenerateNewTime()
    {
        return time;
    }
}
