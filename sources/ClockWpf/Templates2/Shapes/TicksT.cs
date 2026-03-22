using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class TicksT : RimBaseT
{
    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The length of the ticks, calculated as percentage from the clock's radius.")]
    public double Length { get; set; }

    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the ends of the clock ticks are rendered with rounded caps.")]
    public bool RoundEnds { get; set; }
}
