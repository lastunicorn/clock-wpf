namespace DustInTheWind.ClockWpf.Movements;

internal interface ITimeTransition : IDisposable
{
    bool IsRunning { get; }

    TimeOnly CurrentTime { get; }

    void Start(TimeOnly startTime, TimeOnly endTime, int tickInterval);

    void Stop();
}
