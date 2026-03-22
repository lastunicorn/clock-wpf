using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class DotHandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The radius of the dot.")]
    public double Radius { get; set; }
}