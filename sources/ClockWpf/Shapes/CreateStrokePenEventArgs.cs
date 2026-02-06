using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

public class CreateStrokePenEventArgs : EventArgs
{
    public Pen StrokePen { get; }

    public bool Freeze { get; set; } = true;

    public CreateStrokePenEventArgs(Pen strokePen)
    {
        StrokePen = strokePen ?? throw new ArgumentNullException(nameof(strokePen));
    }
}