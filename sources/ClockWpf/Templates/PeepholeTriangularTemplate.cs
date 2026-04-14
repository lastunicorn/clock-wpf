using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

[ClockTemplate("Peephole 2")]
public class PeepholeTriangularTemplate : ClockTemplate
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        yield return new FlatBackgroundT
        {
            Name = "Background",
            FillBrush = Brushes.White
        };

        yield return new HourNumeralsT
        {
            Name = "Hour Numerals",
            FillBrush = Brushes.Black,
            FontFamily = new FontFamily("Arial"),
            FontSize = 20,
            DistanceFromEdge = 32
        };

        GradientStopCollection gradientStops = new()
        {
            new GradientStop(Color.FromRgb(0x1a, 0x1a, 0x1a), 0),
            new GradientStop(Color.FromRgb(0x1a, 0x1a, 0x1a), 0.5),
            new GradientStop(Color.FromRgb(0x00, 0x00, 0x00), 1)
        };

        LinearGradientBrush linearGradientBrush = new(gradientStops, new Point(0.25, 0), new Point(0.75, 1));

        if (linearGradientBrush.CanFreeze)
            linearGradientBrush.Freeze();

        yield return new PeepholeTriangularHandT
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            FillBrush = linearGradientBrush,
            Radius = 100,
            Length = 88,
            SlotAngle = 22,
            TailLength = -15
        };

        SolidColorBrush strokeBrush = new(Color.FromRgb(0x64, 0x64, 0x64));

        if (strokeBrush.CanFreeze)
            strokeBrush.Freeze();

        yield return new BarHandT
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            FillBrush = Brushes.Red,
            Length = 80,
            Width = 2,
            TailLength = 12,
            RoundEnds = false,
            StrokeThickness = 0
        };

        yield return new PinT
        {
            Name = "Pin",
            FillBrush = Brushes.Red,
            Diameter = 6
        };

        yield return new PinT
        {
            Name = "Pin",
            FillBrush = Brushes.Black,
            Diameter = 4
        };
    }
}
