using System.Windows;

namespace DustInTheWind.ClockWpf.TemplateEditor.Behaviors;

internal class ZoomPanState
{
    public bool IsDragging { get; set; }

    public Point LastMousePosition { get; set; }
}
