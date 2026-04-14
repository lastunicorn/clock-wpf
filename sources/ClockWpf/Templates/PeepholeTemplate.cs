using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

[ClockTemplate("Peephole", "A clock template with peephole-style hands.")]
public class PeepholeTemplate : ClockTemplate
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        yield return new FlatBackgroundT
        {
            Name = "Background",
            FillBrush = Brushes.Black
        };

        yield return new HourNumeralsT
        {
            Name = "Hour Numerals",
            FillBrush = Brushes.White,
            FontFamily = new FontFamily("Arial"),
            FontSize = 26,
            FontWeight = FontWeights.Bold,
            DistanceFromEdge = 46
        };

        yield return new PeepholeHandT
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            FillBrush = Brushes.White,
            Radius = 98,
            Length = 72,
            Width = 23,
            TailLength = 11.5
        };

        SolidColorBrush strokeBrush = new(Color.FromRgb(0x64, 0x64, 0x64));

        if (strokeBrush.CanFreeze)
            strokeBrush.Freeze();

        yield return new BarHandT
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            FillBrush = Brushes.Black,
            Length = 90,
            Width = 8,
            TailLength = 0,
            StrokeThickness = 0.1,
            StrokeBrush = strokeBrush,
            RoundEnds = false
        };

        yield return new BarHandT
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            FillBrush = Brushes.Black,
            Length = 90,
            Width = 1,
            RoundEnds = false,
            TailLength = 30,
            StrokeThickness = 0.1,
            StrokeBrush = strokeBrush
        };

        yield return new PinT
        {
            Name = "Pin",
            FillBrush = Brushes.Black,
            Diameter = 20,
            StrokeBrush = strokeBrush,
            StrokeThickness = 0.5
        };
    }
}
