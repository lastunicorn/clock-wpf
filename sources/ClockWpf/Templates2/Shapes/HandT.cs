using System.ComponentModel;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public abstract class HandT : ShapeT
{
    [Category("Appearance")]
    [DefaultValue(95.0)]
    [Description("The length of the hand from the pin to its top, calculated as percentage from the clock's radius.")]
    public double Length { get; set; }

    [DefaultValue(typeof(TimeComponent), "None")]
    [Category("Behavior")]
    [Description("Specifies the component that is displayed from the time value.")]
    public TimeComponent TimeComponent { get; set; }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Specifies if the hand will display only the integral part of the value.")]
    public bool IntegralValue { get; set; }
}
