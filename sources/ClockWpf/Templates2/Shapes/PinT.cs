using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class PinT : ShapeT
{
    [Category("Appearance")]
    [DefaultValue(4.0)]
    [Description("The diameter of the pin, calculated as percentage from the clock's radius.")]
    public double Diameter { get; set; }
}
