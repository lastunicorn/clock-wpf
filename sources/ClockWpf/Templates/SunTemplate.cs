using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

[Template("Sun", "A bright clock template with sun-inspired design.")]
public class SunTemplate : ClockTemplate
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        yield return new FancyBackgroundT
        {
            Name = "Background",
            OuterRimWidth = 14,
            InnerRimWidth = 46,
            OuterRimBrush = CreateOuterRimBrush(),
            InnerRimBrush = CreateInnerRimBrush(),
            FillBrush = CreateFaceBrush(),
            ColorSource = ColorSource.Manual
        };

        yield return new HourNumeralsT
        {
            Name = "Hour Numerals",
            DistanceFromEdge = 37,
            FontSize = 17,
            FillBrush = Brushes.Black
        };

        yield return new TextRimT
        {
            Name = "Minute Numerals",
            Texts = Enumerable.Range(1, 60)
                .Select(x => x.ToString())
                .ToArray(),
            Angle = 6,
            OffsetAngle = 6,
            DistanceFromEdge = 7,
            FontFamily = new FontFamily("Arial"),
            FontSize = 4.4,
            FillBrush = Brushes.Black
        };

        yield return new DotHandT
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 63,
            FillBrush = null,
            StrokeBrush = Brushes.Black,
            StrokeThickness = 1,
            Radius = 14,
            IntegralValue = true
        };

        yield return new DotHandT
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 93,
            FillBrush = null,
            StrokeBrush = Brushes.Black,
            StrokeThickness = 1,
            Radius = 6,
            IntegralValue = true
        };

        yield return new DotHandT
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 93,
            FillBrush = null,
            StrokeBrush = Brushes.Black,
            StrokeThickness = 0.5,
            Radius = 6
        };
    }

    private static Brush CreateOuterRimBrush()
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(Color.FromRgb(155, 219, 255), 0));
        brush.GradientStops.Add(new GradientStop(Color.FromRgb(0, 64, 128), 1));

        brush.Freeze();

        return brush;
    }

    private static Brush CreateInnerRimBrush()
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(Color.FromRgb(0, 64, 128), 0));
        brush.GradientStops.Add(new GradientStop(Color.FromRgb(155, 219, 255), 1));

        brush.Freeze();

        return brush;
    }

    private static Brush CreateFaceBrush()
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(Color.FromRgb(200, 230, 255), 0));
        brush.GradientStops.Add(new GradientStop(Color.FromRgb(50, 100, 150), 1));

        brush.Freeze();

        return brush;
    }
}
