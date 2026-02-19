namespace DustInTheWind.ClockWpf.Movements;

internal interface ITimeTransition : IDisposable
{
    bool IsRunning { get; }

    TimeSpan CurrentTime { get; }

    void Start(TimeSpan startTime, TimeSpan endTime, int tickInterval);

    void Stop();
}
