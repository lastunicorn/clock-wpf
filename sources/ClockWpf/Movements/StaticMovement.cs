using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Movements;

/// <summary>
/// Provides a movement that returns a fixed time value, with optional smooth transitions when the value changes.
/// </summary>
/// <remarks>
/// Use StaticMovement to supply a constant time value that can be updated at runtime. When the time
/// value is changed, the transition to the new value can occur instantly or be smoothly interpolated over a specified
/// interval, depending on the TransitionInterval property. This class is useful for scenarios where a time value needs
/// to be controlled directly, with optional animation between changes.
/// </remarks>
[Movement("Static", "Provides a fixed time value with optional smooth transition when changed.")]
public class StaticMovement : MovementBase
{
    private TimeSpan desiredTime = DateTime.Now.TimeOfDay;
    private TimeSpan currentTime = DateTime.Now.TimeOfDay;
    private ITimeTransition timeTransition;

    #region Time Property

    /// <summary>
    /// Gets or sets the desired time value to be used.
    /// </summary>
    [Category("Behavior")]
    [Description("The desired time value to be used.")]
    public TimeSpan Time
    {
        get => desiredTime;
        set
        {
            if (desiredTime == value)
                return;

            desiredTime = value;
            OnModified();

            StartTransition();
        }
    }

    #endregion

    #region TransitionDuration Property

    private TimeSpan transitionDuration = TimeSpan.Zero;

    /// <summary>
    /// Gets or sets the duration over which the time value transitions to a new value in real time.
    /// </summary>
    /// <remarks>
    /// Setting this property to a negative value will automatically reset it to zero. Use this
    /// property to control the smoothness or speed of time-based transitions.
    /// </remarks>
    [Category("Behavior")]
    [Description("The real-time duration over which the Time value transitions to the new value.")]
    public TimeSpan TransitionDuration
    {
        get => transitionDuration;
        set
        {
            if (value < TimeSpan.Zero)
                value = TimeSpan.Zero;

            transitionDuration = value;
            OnModified();
        }
    }

    #endregion

    #region TransitionSpeed Property

    private double transitionSpeed;

    /// <summary>
    /// Gets or sets the speed at which the time transitions relative to real time.
    /// </summary>
    /// <remarks>
    /// A value of 1 means time passes at normal speed. A value of 2 means time passes twice as fast
    /// (2 seconds pass on the clock for every 1 real second). This property is only used when
    /// TransitionDuration is zero.
    /// </remarks>
    [Category("Behavior")]
    [Description("The speed at which the time transitions relative to real time. Used only when TransitionDuration is zero.")]
    public double TransitionSpeed
    {
        get => transitionSpeed;
        set
        {
            if (value == transitionSpeed)
                return;
            transitionSpeed = value;
            OnModified();
        }
    }

    #endregion

    #region TransitionTickInterval Property

    private int transitionTickInterval = 30;

    /// <summary>
    /// Gets or sets the real-time interval, in ticks, over which the Time value transitions to a new value.
    /// </summary>
    [Category("Behavior")]
    [Description("The real-time duration over which the Time value transitions to the new value.")]
    public int TransitionTickInterval
    {
        get => transitionTickInterval;
        set
        {
            if (value == transitionTickInterval)
                return;

            transitionTickInterval = value;
            OnModified();
        }
    }

    #endregion

    /// <summary>
    /// Initializes a new instance of the StaticMovement class with default settings.
    /// </summary>
    /// <remarks>
    /// This constructor sets the TickInterval property to 0 and prepares the internal timer for use.
    /// The timer does not start automatically upon construction.
    /// </remarks>
    public StaticMovement()
    {
        TickInterval = 0;
    }

    private void HandleTransitionCallback(TimeSpan time)
    {
        currentTime = time;
        ForceTick();
    }

    private void StartTransition()
    {
        TransitionType transitionType = CalculateTransitionType();

        switch (transitionType)
        {
            case TransitionType.Instant:
                currentTime = desiredTime;
                ForceTick();
                break;

            case TransitionType.AnimatedByDuration:
                StartAnimationByDuration();
                break;

            case TransitionType.AnimatedBySpeed:
                StartAnimationBySpeed();
                break;
        }
    }

    private TransitionType CalculateTransitionType()
    {
        if (transitionDuration > TimeSpan.Zero && transitionTickInterval > 0)
            return TransitionType.AnimatedByDuration;

        if (TransitionSpeed > 0 && transitionTickInterval > 0)
            return TransitionType.AnimatedBySpeed;

        return TransitionType.Instant;
    }

    private void StartAnimationByDuration()
    {
        if (timeTransition != null)
        {
            timeTransition.Stop();
            timeTransition.Dispose();
        }

        timeTransition = new TransitionByDuration(HandleTransitionCallback)
        {
            TransitionDuration = TransitionDuration
        };

        timeTransition.Start(currentTime, desiredTime, transitionTickInterval);
    }

    private void StartAnimationBySpeed()
    {
        if (timeTransition != null)
        {
            timeTransition.Stop();
            timeTransition.Dispose();
        }

        timeTransition = new TransitionBySpeed(HandleTransitionCallback)
        {
            TransitionSpeed = TransitionSpeed
        };

        timeTransition.Start(currentTime, desiredTime, transitionTickInterval);
    }

    /// <summary>
    /// Calculates and returns the current time value, progressing toward the target time if a transition is in
    /// progress.
    /// </summary>
    /// <remarks>
    /// This method is typically called to update or retrieve the current time during a transition.
    /// When the transition completes, the returned value equals the target time and the transition state is
    /// reset.
    /// </remarks>
    /// <returns>A TimeSpan representing the current time. If a transition is in progress, the value reflects the interpolated
    /// time between the start and target times; otherwise, it returns the current time without modification.</returns>
    protected override TimeSpan GenerateNewTime()
    {
        return currentTime;
    }

    protected override void OnModified()
    {
        base.OnModified();

        if (TickInterval != 0)
            TickInterval = 0;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            timeTransition?.Stop();
            timeTransition?.Dispose();
        }

        base.Dispose(disposing);
    }
}
