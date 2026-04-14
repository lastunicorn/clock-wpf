using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates.Shapes;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Templates;

[ClockTemplate("Sharp", "A modern clock template with sharp, angular design.")]
public class SharpTemplate : ClockTemplate
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        yield return new FancyBackgroundT
        {
            Name = "Background",
            OuterRimBrush = CreateOuterRimBrush(Colors.Black),
            InnerRimBrush = CreateInnerRimBrush(Colors.Black),
            FillBrush = CreateFaceBrush(Colors.Black),
            ColorSource = ColorSource.Manual
        };

        yield return new TicksT
        {
            Name = "Minute Ticks",
            DistanceFromEdge = 16.5,
            Angle = 6,
            OffsetAngle = 6,
            StrokeThickness = 0.3
        };

        yield return new TicksT
        {
            Name = "Hour Ticks",
            DistanceFromEdge = 16.5,
            Angle = 30,
            OffsetAngle = 30,
            StrokeBrush = Brushes.White,
            StrokeThickness = 1
        };

        yield return new HourNumeralsT
        {
            Name = "Hour Numerals",
            DistanceFromEdge = 32,
            FillBrush = Brushes.LightGray,
            FontFamily = new FontFamily("Vivaldi"),
            FontSize = 16,
            FontWeight = FontWeights.Normal
        };

        yield return new TextRimT
        {
            Name = "Minute Numerals",
            Angle = 30,
            OffsetAngle = 30,
            DistanceFromEdge = 5.5,
            FillBrush = Brushes.DarkGray,
            FontFamily = new FontFamily("Arial"),
            FontSize = 5.5,
            Texts = Enumerable.Range(1, 12)
                .Select(x => (x * 5).ToString())
                .ToArray()
        };

        yield return new DiamondHandT
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            FillBrush = Brushes.RoyalBlue,
            Length = 50,
            Width = 10,
            TailLength = 8,
            StrokeThickness = 0
        };

        yield return new DiamondHandT
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            FillBrush = Brushes.LimeGreen,
            Length = 76,
            Width = 8,
            TailLength = 8,
            StrokeThickness = 0
        };

        yield return new LineHandT
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 86,
            TailLength = 14,
            StrokeBrush = Brushes.Red,
            StrokeThickness = 0.3
        };

        yield return new PinT()
        {
            Name = "Pin",
            Diameter = 2,
            FillBrush = Brushes.Red
        };
    }

    private static Brush CreateOuterRimBrush(Color color)
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(40f), 0));
        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(-40f), 1));

        brush.Freeze();
        return brush;
    }

    private static Brush CreateInnerRimBrush(Color color)
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(-40f), 0));
        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(40f), 1));

        return brush;
    }

    private static Brush CreateFaceBrush(Color color)
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(40f), 0));
        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(-40f), 1));

        if (brush.CanFreeze)
            brush.Freeze();

        return brush;
    }
}
