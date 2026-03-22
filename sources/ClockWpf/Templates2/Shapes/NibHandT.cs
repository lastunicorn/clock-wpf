using System.ComponentModel;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class NibHandT : HandT
{
    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The width of the hand, calculated as percentage from the clock's radius.")]
    public double Width { get; set; }

    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Specifies if the hand should keep its proportions when its length is changed.")]
    public bool KeepProportions { get; set; }
}
