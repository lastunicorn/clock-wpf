using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class FancyBackgroundT : ShapeT
{
    public double OuterRimWidth { get; set; } = 10.0;

    public double InnerRimWidth { get; set; } = 2.0;

    public Brush OuterRimBrush { get; set; }

    public Brush InnerRimBrush { get; set; }

    public Color FillColor { get; set; } = Colors.Black;

    public FancyBackgroundT()
    {
        Name = "Fancy Background";
        FillBrush = null;
        StrokeThickness = 0.0;
    }
}
