using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class Blade2HandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(20.0)]
    [Description("The width of the hand.")]
    public double Width { get; set; }

    [Category("Appearance")]
    [DefaultValue(45.0)]
    [Description("The distance from the origin to the most wide part of the hand (the hip).")]
    public double HipDistance { get; set; }

    [Category("Appearance")]
    [DefaultValue(15.0)]
    public double TipLength { get; set; }
}
