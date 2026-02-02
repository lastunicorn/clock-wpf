using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Movements;

public class StaticMovement : MovementBase
{
    private readonly Timer transitionTimer;
    private TimeSpan targetTime = DateTime.Now.TimeOfDay;
    private TimeSpan currentTime = DateTime.Now.TimeOfDay;
    private TimeSpan startTime;
    private DateTime transitionStartTime;
    private bool isTransitioning;

    #region Time Property

    [Category("Behavior")]
    [Description("The desired time value to be returned.")]
    public TimeSpan Time
    {
        get => targetTime;
        set
        {
            if (targetTime == value)
                return;

            targetTime = value;
            StartTransition();
        }
    }

    #endregion

    #region TransitionInterval Property

    private TimeSpan transitionInterval = TimeSpan.Zero;

    [Category("Behavior")]
    [Description("The real-time duration over which the Time value transitions to the new value.")]
    public TimeSpan TransitionInterval
    {
        get => transitionInterval;
        set
        {
            if (value < TimeSpan.Zero)
                value = TimeSpan.Zero;

            transitionInterval = value;
        }
    }

    #endregion

    #region TransitionTickInterval Property

    private int transitionTickInterval = 30;

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
        }
    }

    #endregion

    public StaticMovement()
    {
        TickInterval = 0;
        transitionTimer = new Timer(HandleTransitionTimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    }

    private void StartTransition()
    {
        if (transitionInterval <= TimeSpan.Zero || transitionTickInterval <= 0)
        {
            currentTime = targetTime;
            ForceTick();
            return;
        }

        startTime = currentTime;
        transitionStartTime = DateTime.Now;
        isTransitioning = true;

        if (TickInterval == 0)
            _ = transitionTimer.Change(0, transitionTickInterval);

        ForceTick();
    }

    private void HandleTransitionTimerCallback(object state)
    {
        if (!isTransitioning)
            return;

        ForceTick();
    }

    protected override TimeSpan GenerateNewTime()
    {
        if (!isTransitioning)
            return currentTime;

        TimeSpan elapsed = DateTime.Now - transitionStartTime;

        if (elapsed >= transitionInterval)
        {
            currentTime = targetTime;
            isTransitioning = false;

            if (TickInterval == 0)
                _ = transitionTimer.Change(Timeout.Infinite, Timeout.Infinite);

            return currentTime;
        }

        double progress = elapsed.TotalMilliseconds / transitionInterval.TotalMilliseconds;
        double startTicks = startTime.Ticks;
        double targetTicks = targetTime.Ticks;
        double currentTicks = startTicks + (targetTicks - startTicks) * progress;

        currentTime = TimeSpan.FromTicks((long)currentTicks);
        return currentTime;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _ = transitionTimer.Change(Timeout.Infinite, Timeout.Infinite);
            transitionTimer.Dispose();
        }

        base.Dispose(disposing);
    }
}
