using DustInTheWind.ClockWpf.Serialization;

namespace DustInTheWind.ClockWpf.Shapes;

public class ImportedEventArgs : EventArgs
{
    public ClockShape ClockShape { get; }

    public ImportedEventArgs(ClockShape clockShape)
    {
        ClockShape = clockShape ?? throw new ArgumentNullException(nameof(clockShape));
    }
}