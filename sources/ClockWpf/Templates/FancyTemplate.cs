using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class FancyTemplate : ClockTemplate
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        GradientStopCollection gradientStops = new()
        {
            new GradientStop(Colors.WhiteSmoke, 0),
            new GradientStop(Colors.WhiteSmoke, 0.5),
            new GradientStop(Colors.LightGray, 1)
        };

        LinearGradientBrush linearGradientBrush = new(gradientStops, new Point(0.25, 0), new Point(0.75, 1));

        yield return new FlatBackgroundT
        {
            Name = "Background",
            FillBrush = linearGradientBrush
        };

        yield return new TicksT
        {
            Name = "Minute Ticks",
            StrokeBrush = Brushes.Black,
            Length = 6,
            StrokeThickness = 0.3,
            DistanceFromEdge = 8,
            Angle = 6,
            OffsetAngle = 6
        };

        yield return new TicksT
        {
            Name = "Hour Ticks",
            StrokeBrush = Brushes.Black,
            Length = 6,
            StrokeThickness = 1,
            DistanceFromEdge = 8,
            Angle = 30,
            OffsetAngle = 30
        };

        yield return new HourNumeralsT
        {
            Name = "Hour Numerals",
            DistanceFromEdge = 28,
            FillBrush = Brushes.Black,
            FontSize = 20
        };

        yield return new NibHandT
        {
            Name = "Hour Hand",
            FillBrush = Brushes.Black,
            Length = 60,
            TimeComponent = TimeComponent.Hour,
            Width = 10,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60)),
            StrokeThickness = 1.5
        };

        yield return new NibHandT
        {
            Name = "Minute Hand",
            FillBrush = Brushes.Black,
            TimeComponent = TimeComponent.Minute,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60)),
            StrokeThickness = 1.5,
            Length = 86
        };

        yield return new FancySweepHandT
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 86,
            StrokeBrush = Brushes.Red,
            StrokeThickness = 0.5
        };

        yield return new PinT
        {
            Name = "Pin",
            FillBrush = new SolidColorBrush(Color.FromRgb(0x64, 0x64, 0x64)),
            StrokeThickness = 0,
            Diameter = 4
        };
    }
}
