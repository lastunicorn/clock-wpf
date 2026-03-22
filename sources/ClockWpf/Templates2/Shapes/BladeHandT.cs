using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class BladeHandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(20.0)]
    [Description("The width of the hand.")]
    public double Width { get; set; }

    [Category("Appearance")]
    [DefaultValue(20.0)]
    [Description("The distance from the origin to the most wide part of the hand (the hip).")]
    public double HipDistance { get; set; }

    [Category("Appearance")]
    [DefaultValue(2.0)]
    [Description("The space betwwen the margin of the hand and the inner shadow.")]
    public double ShadowMargin { get; set; }
}
