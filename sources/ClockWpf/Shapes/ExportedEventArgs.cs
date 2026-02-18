using DustInTheWind.ClockWpf.Serialization;

namespace DustInTheWind.ClockWpf.Shapes;

public class ExportedEventArgs : EventArgs
{
    public ClockShape ClockShape { get; }

    public ExportedEventArgs(ClockShape clockShape)
    {
        ClockShape = clockShape ?? throw new ArgumentNullException(nameof(clockShape));
    }
}
