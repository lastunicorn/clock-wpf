using System.Windows;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.TemplateEditor.Behaviors;

internal class DragDropState
{
    public Point StartPoint { get; set; }

    public Shape DraggedItem { get; set; }
}
