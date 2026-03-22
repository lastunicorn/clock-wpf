using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class BarHandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(10.0)]
    [Description("The width of the hand.")]
    public double Width { get; set; }

    [Category("Appearance")]
    [DefaultValue(2.0)]
    [Description("The hand's length of the tail as percentage from the clock's radius.")]
    public double TailLength { get; set; }

    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the ends of the clock hand are rendered with rounded caps.")]
    public bool RoundEnds { get; set; }
}
