namespace DustInTheWind.ClockWpf.Movements;

internal class TransitionBySpeed : ITimeTransition
{
    private readonly Timer transitionTimer;
    private readonly TimeTransitionCallback callback;
    private TimeSpan startTime;
    private TimeSpan endTime;
    private DateTime realStartTime;
    private bool isDisposed;

    public double TransitionSpeed { get; set; }

    public bool IsRunning { get; private set; }

    public TimeSpan CurrentTime { get; private set; }

    public TransitionBySpeed(TimeTransitionCallback callback)
    {
        this.callback = callback ?? throw new ArgumentNullException(nameof(callback));

        transitionTimer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    }

    private void TimerCallback(object state)
    {
        Move();
        callback(CurrentTime);
    }

    public void Start(TimeSpan startTime, TimeSpan endTime, int tickInterval)
    {
        this.startTime = startTime;
        this.endTime = endTime;

        if (TransitionSpeed <= 0)
        {
            Stop();
            return;
        }

        realStartTime = DateTime.Now;
        IsRunning = true;

        _ = transitionTimer.Change(0, tickInterval);
    }

    public void Move()
    {
        TimeSpan realElapsedTime = DateTime.Now - realStartTime;

        TimeSpan totalTime = endTime - startTime;
        bool isForward = totalTime >= TimeSpan.Zero;
        double clockTimeAdvanced = realElapsedTime.TotalSeconds * TransitionSpeed;

        if (isForward)
        {
            CurrentTime = startTime + TimeSpan.FromSeconds(clockTimeAdvanced);

            if (CurrentTime >= endTime)
                Stop();
        }
        else
        {
            CurrentTime = startTime - TimeSpan.FromSeconds(clockTimeAdvanced);

            if (CurrentTime <= endTime)
                Stop();
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