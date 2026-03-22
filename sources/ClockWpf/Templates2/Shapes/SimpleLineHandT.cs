using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class SimpleLineHandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the ends of the clock's hands are rendered with rounded caps.")]
    public bool RoundEnds { get; set; }

    [Category("Appearance")]
    [DefaultValue(0.0)]
    [Description("The hand's length of the tail, calculated as percentage from the clock's radius.")]
    public double TailLength { get; set; }
}
