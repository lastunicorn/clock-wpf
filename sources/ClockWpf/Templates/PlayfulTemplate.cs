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

        yield return new HourNumerals
        {
            Name = "Hour Numerals",
            FontFamily = new FontFamily("Arial Rounded MT"),
            DistanceFromEdge = 30
        };

        yield return new CapsuleHand
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 55,
            Width = 10,
            TailLength = -18,
            StrokeThickness = 0,
            FillBrush = Brushes.RoyalBlue
        };

        yield return new CapsuleHand
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 84,
            Width = 8,
            TailLength = -18,
            StrokeThickness = 0,
            FillBrush = Brushes.LimeGreen
        };

        yield return new SimpleHand
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 84,
            TailLength = -18,
            StrokeBrush = Brushes.Red,
            StrokeThickness = 1,
            PinDiameter = 24
        };
    }
}
