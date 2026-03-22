using System.ComponentModel;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class ShapeT
{
    /// <summary>
    /// A user friendly name. Used only to be displayed to the user. Does not influence the
    /// way the shape is rendered.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    [Description("A user friendly name. Used only to be displayed to the user. Does not influence the way the shape is rendered.")]
    public string Name { get; set; }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("A value specifying if the shape should be rendered or not.")]
    public bool IsVisible { get; set; }

    [Category("Appearance")]
    [DefaultValue(typeof(SolidColorBrush), "CornflowerBlue")]
    [Description("Gets or sets the brush used to draw the filling of the shape.")]
    public Brush FillBrush { get; set; }

    [Category("Appearance")]
    [DefaultValue(typeof(SolidColorBrush), "Black")]
    [Description("Gets or sets the brush used to draw the stroke of the shape.")]
    public Brush StrokeBrush { get; set; }

    [Category("Appearance")]
    [DefaultValue(1.0)]
    [Description("The width of the outline.")]
    public double StrokeThickness { get; set; }
}
