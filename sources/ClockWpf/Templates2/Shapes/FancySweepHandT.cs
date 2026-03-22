using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class FancySweepHandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(7.0)]
    [Description("The radius of the circle from the middle (or not so middle) of the hand.")]
    public double CircleRadius { get; set; }

    [Category("Appearance")]
    [DefaultValue(24.0)]
    [Description("The offset position of the center of the circle from the top of the hand.")]
    public double CircleOffset { get; set; }

    [Category("Appearance")]
    [DefaultValue(14.0)]
    [Description("The length of the tail of the hand.")]
    public double TailLength { get; set; }
}
