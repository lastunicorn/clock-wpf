using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

public class ClockDrawingContext
{
    public DrawingContext DrawingContext { get; init; }

    public double ClockDiameter { get; init; }

    public double ClockRadius => ClockDiameter / 2;

    public TimeSpan Time { get; init; }
}
