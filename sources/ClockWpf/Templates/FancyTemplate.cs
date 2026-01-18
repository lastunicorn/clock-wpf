using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

/// <summary>
/// Provides a fancy visual template for rendering a clock, featuring nib-style hands and a decorative sweep hand.
/// </summary>
public class FancyTemplate : ClockTemplate
{
    protected override IEnumerable<Shape> CreateShapes()
    {
        yield return new FlatBackground
        {
            Name = "Background",
            FillBrush = Brushes.White
        };

        yield return new Ticks
        {
            Name = "Minute Ticks",
            StrokeBrush = Brushes.Black,
            Length = 6,
            StrokeThickness = 0.3,
            DistanceFromEdge = 8,
            Angle = 6,
            OffsetAngle = 6,
            SkipIndex = 5
        };

        yield return new Ticks
        {
            Name = "Hour Ticks",
            StrokeBrush = Brushes.Black,
            Length = 6,
            StrokeThickness = 1,
            DistanceFromEdge = 8,
            Angle = 30,
            OffsetAngle = 30
        };

        yield return new Hours
        {
            Name = "Hours",
            DistanceFromEdge = 28,
            FillBrush = Brushes.Black,
            FontSize = 20
        };

        yield return new NibHand
        {
            Name = "Hour Hand",
            FillBrush = Brushes.Black,
            Length = 60,
            ComponentToDisplay = TimeComponent.Hour,
            Width = 10,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60)),
            StrokeThickness = 1.5
        };

        yield return new NibHand
        {
            Name = "Minute Hand",
            FillBrush = Brushes.Black,
            ComponentToDisplay = TimeComponent.Minute,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60)),
            StrokeThickness = 1.5,
            Length = 86
        };

        yield return new FancySweepHand
        {
            Name = "Second Hand",
            ComponentToDisplay = TimeComponent.Second,
            Length = 86,
            StrokeBrush = Brushes.Red,
            StrokeThickness = 0.5
        };

        yield return new Pin
        {
            Name = "Pin",
            FillBrush = new SolidColorBrush(Color.FromRgb(0x64, 0x64, 0x64)),
            StrokeThickness = 0,
            Diameter = 3
        };
    }
}
