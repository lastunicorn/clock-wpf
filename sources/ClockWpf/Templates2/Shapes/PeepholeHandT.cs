using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class PeepholeHandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(10.0)]
    [Description("The width of the slot carved inside the disk, calculated as percentage from the clock's radius.")]
    public double Width { get; set; }

    [Category("Appearance")]
    [DefaultValue(100.0)]
    [Description("The radius of the opaque disk, calculated as percentage from the clock's radius.")]
    public double Radius { get; set; }

    [Category("Appearance")]
    [DefaultValue(12.0)]
    [Description("The length of the hand's tail, calculated as percentage from the clock's radius.")]
    public double TailLength { get; set; }
}
