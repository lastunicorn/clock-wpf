namespace DustInTheWind.ClockWpf.Movements;

internal delegate void TimeTransitionCallback(TimeOnly time);

internal class TransitionByDuration : ITimeTransition
{
    private readonly Timer transitionTimer;
    private readonly TimeTransitionCallback callback;
    private TimeOnly startTime;
    private TimeOnly endTime;
    private DateTime realStartTime;
    private bool isDisposed;

    public TimeSpan TransitionDuration { get; set; }

    public bool IsRunning { get; private set; }

    public TimeOnly CurrentTime { get; private set; }

    public TransitionByDuration(TimeTransitionCallback callback)
    {
        this.callback = callback ?? throw new ArgumentNullException(nameof(callback));

        transitionTimer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    }

    private void TimerCallback(object state)
    {
        Move();
        callback(CurrentTime);
    }

    public void Start(TimeOnly startTime, TimeOnly endTime, int tickInterval)
    {
        this.startTime = startTime;
        this.endTime = endTime;

        if (TransitionDuration <= TimeSpan.Zero)
        {
            Stop();
            return;
        }

        realStartTime = DateTime.Now;
        IsRunning = true;

        _ = transitionTimer.Change(0, tickInterval);

    }

    private void Move()
    {
        if (!IsRunning)
            return;

        TimeSpan realElapsedTime = DateTime.Now - realStartTime;

        if (realElapsedTime >= TransitionDuration)
        {
            Stop();
        }
        else
        {
            double progress = realElapsedTime.TotalMilliseconds / TransitionDuration.TotalMilliseconds;
            double startTicks = startTime.Ticks;
            double endTicks = endTime.Ticks;
            double totalTicks = endTicks - startTicks;
            double currentTicks = startTicks + (totalTicks) * progress;

            CurrentTime = new TimeOnly((long)currentTicks);
        }
    }

    public void Stop()
    {
        CurrentTime = endTime;
        _ = transitionTimer.Change(Timeout.Infinite, Timeout.Infinite);

        IsRunning = false;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                _ = transitionTimer.Change(Timeout.Infinite, Timeout.Infinite);
                transitionTimer.Dispose();
            }

            isDisposed = true;
        }
    }
}
