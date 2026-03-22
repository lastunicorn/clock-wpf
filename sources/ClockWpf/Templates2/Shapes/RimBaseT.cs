using System.ComponentModel;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public abstract class RimBaseT : ShapeT
{
    [Category("Layout")]
    [DefaultValue(6.0)]
    [Description("The distance from the edge of the clock to the items being displayed, calculated as percentage from the clock's radius.")]
    public double DistanceFromEdge { get; set; }

    [Category("Layout")]
    [DefaultValue(30)]
    [Description("The angle, in degrees, between two consecutive instances of the shape.")]
    public double Angle { get; set; }

    [Category("Layout")]
    [DefaultValue(0.0)]
    [Description("The angle, in degrees, between north and the first item that is displayed.")]
    public double OffsetAngle { get; set; }

    [Category("Layout")]
    [DefaultValue(typeof(RimItemOrientation), "Normal")]
    [Description("The orientation of each item being displayed.")]
    public RimItemOrientation Orientation { get; set; }
}
