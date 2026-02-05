using System.Windows;

namespace DustInTheWind.WpfWorld.Behaviors;

internal class ZoomPanState
{
    public bool IsDragging { get; set; }

    public Point LastMousePosition { get; set; }
}
