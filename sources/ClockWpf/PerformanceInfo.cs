using System.Diagnostics;

namespace DustInTheWind.ClockWpf;

public class PerformanceInfo
{
    private readonly Stopwatch stopwatch = new();

    public long MeasurementCount { get; private set; }

    public TimeSpan TotalTime { get; private set; }

    public TimeSpan LastTime { get; set; }

    public TimeSpan AverageTime
    {
        get
        {
            long measurementCount = MeasurementCount;

            return measurementCount == 0
                ? TimeSpan.Zero
                : TotalTime / measurementCount;
        }
    }

    public event EventHandler Changed;

    public void Start()
    {
        stopwatch.Restart();
    }

    public void Stop()
    {
        stopwatch.Stop();

        MeasurementCount++;
        LastTime = stopwatch.Elapsed;
        TotalTime += LastTime;

        OnChanged();
    }

    public void Reset()
    {
        MeasurementCount = 0;
        TotalTime = TimeSpan.Zero;
        LastTime = TimeSpan.Zero;

        OnChanged();
    }

    protected virtual void OnChanged()
    {
        Changed?.Invoke(this, EventArgs.Empty);
    }
}
