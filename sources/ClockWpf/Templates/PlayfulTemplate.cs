using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

public class PlayfulTemplate : ClockTemplate
{
    protected override IEnumerable<Shape> CreateShapes()
    {
        yield return new FlatBackground
        {
            Name = "Background"
        };

        yield return new Ticks
        {
            Name = "Minute Ticks",
            SkipIndex = 5,
            Length = 3,
            DistanceFromEdge = 6,
            RoundEnds = true,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60))
        };

        yield return new Ticks
        {
            Name = "Hour Ticks",
            Angle = 30,
            OffsetAngle = 30,
            StrokeThickness = 2,
            Length = 6,
            DistanceFromEdge = 7.5,
            RoundEnds = true,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0x40, 0x40, 0x40))
        };

        yield return new Hours
        {
            Name = "Hours",
            FontFamily = new FontFamily("Arial Rounded MT"),
            DistanceFromEdge = 28
        };

        yield return new CapsuleHand
        {
            Name = "Hour Hand",
            ComponentToDisplay = TimeComponent.Hour,
            Length = 48,
            Width = 10,
            TailLength = -18,
            StrokeThickness = 0,
            FillBrush = Brushes.RoyalBlue
        };

        yield return new CapsuleHand
        {
            Name = "Minute Hand",
            ComponentToDisplay = TimeComponent.Minute,
            Length = 80,
            Width = 8,
            TailLength = -18,
            StrokeThickness = 0,
            FillBrush = Brushes.LimeGreen
        };

        yield return new SimpleHand
        {
            Name = "Second Hand",
            ComponentToDisplay = TimeComponent.Second,
            Length = 85,
            TailLength = -19,
            StrokeBrush = Brushes.Red,
            StrokeThickness = 1,
            PinDiameter = 24
        };
    }
}
