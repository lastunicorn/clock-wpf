using System.ComponentModel;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class ShapeT
{
    /// <summary>
    /// A user friendly name. Used only to be displayed to the user. Does not influence the
    /// way the shape is rendered.
    /// </summary>
    public string Name { get; set; } = "Shape";

    public bool IsVisible { get; set; } = true;

    public Brush FillBrush { get; set; } = Brushes.CornflowerBlue;

    public Brush StrokeBrush { get; set; } = Brushes.Black;

    public double StrokeThickness { get; set; } = 1.0;
}
