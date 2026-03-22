using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class FlatBackgroundT : ShapeT
{
    public FlatBackgroundT()
    {
        Name = "Flat Background";
        FillBrush = Brushes.WhiteSmoke;
        StrokeThickness = 0.0;
    }
}