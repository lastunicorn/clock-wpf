namespace DustInTheWind.ClockWpf.TimeProviders;

/// <summary>
/// Implements base functionality for a time provider class.
/// </summary>
public abstract class TimeProviderBase : ITimeProvider
{
    private readonly Timer timer;
    private int interval = 100;

    /// <summary>
    /// Gets or sets the interval in milliseconds at which the time provider generates time values.
    /// </summary>
    public int Interval
    {
        get => interval;
        set
        {
            interval = value;

            if (IsRunning)
                timer.Change(interval, interval);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the time provider is currently running.
    /// </summary>
    public bool IsRunning { get; private set; }

    public TimeSpan LastValue { get; private set; }

    /// <summary>
    /// Event raised when the time provider produces a new time value.
    /// </summary>
    public event EventHandler<TimeChangedEventArgs> TimeChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeProviderBase"/> class.
    /// </summary>
    protected TimeProviderBase()
    {
        timer = new Timer(HandleTimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    }

    private void HandleTimerCallback(object state)
    {
        LastValue = GetTime();
        OnTimeChanged(new TimeChangedEventArgs(LastValue));
    }

    /// <summary>
    /// Returns the current time value. This method is called internally by the timer.
    /// </summary>
    /// <returns>A <see cref="TimeSpan"/> object containing the time value.</returns>
    protected abstract TimeSpan GetTime();

    /// <summary>
    /// Starts the time provider. The time provider will begin generating time values.
    /// </summary>
    public void Start()
    {
        LastValue = GetTime();
        OnTimeChanged(new TimeChangedEventArgs(LastValue));

        timer.Change(interval, interval);
        IsRunning = true;
    }

    /// <summary>
    /// Stops the time provider. The time provider will stop generating time values.
    /// </summary>
    public void Stop()
    {
        timer.Change(Timeout.Infinite, Timeout.Infinite);
        IsRunning = false;
    }

    /// <summary>
    /// Raises the <see cref="TimeChanged"/> event.
    /// </summary>
    /// <param name="e">A <see cref="TimeChangedEventArgs"/> object that contains the event data.</param>
    protected virtual void OnTimeChanged(TimeChangedEventArgs e)
    {
        TimeChanged?.Invoke(this, e);
    }

    #region IDisposable Members

    private bool disposed;

    /// <summary>
    /// Releases all resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the current instance and optionally releases the managed resources.
    /// </summary>
    /// <remarks>
    /// <para>Dispose(bool disposing) executes in two distinct scenarios.</para>
    /// <para>If the method has been called directly or indirectly by a user's code managed and unmanaged resources can be disposed.</para>
    /// <para>If the method has been called by the runtime from inside the finalizer you should not reference other objects. Only unmanaged resources can be disposed.</para>
    /// </remarks>
    /// <param name="disposing">Specifies if the method has been called by a user's code (true) or by the runtime from inside the finalizer (false).</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
                IsRunning = false;
            }

            disposed = true;
        }
    }

    /// <summary>
    /// Finalizer.
    /// </summary>
    ~TimeProviderBase()
    {
        Dispose(false);
    }

    #endregion
}
