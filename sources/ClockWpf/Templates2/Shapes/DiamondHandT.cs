using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class DiamondHandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The width of the hand.")]
    public double Width { get; set; }

    [Category("Appearance")]
    [DefaultValue(6.0)]
    [Description("The hand's length of the tail as percentage from the clock's radius.")]
    public double TailLength { get; set; }
}
