using System.ComponentModel;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class FancyBackgroundT : ShapeT
{
    [Category("Appearance")]
    public double OuterRimWidth { get; set; }

    [Category("Appearance")]
    public double InnerRimWidth { get; set; }

    [Category("Appearance")]
    public Brush OuterRimBrush { get; set; }

    [Category("Appearance")]
    public Brush InnerRimBrush { get; set; }

    [Category("Appearance")]
    public Color FillColor { get; set; }
}
