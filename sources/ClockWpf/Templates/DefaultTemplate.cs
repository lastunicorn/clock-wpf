using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

public class DefaultTemplate : ClockTemplate
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
        };

        yield return new Ticks
        {
            Name = "Hour Ticks",
            Angle = 30,
            OffsetAngle = 30,
            StrokeThickness = 2
        };

        yield return new Hours
        {
            Name = "Hours"
        };

        yield return new DiamondHand
        {
            Name = "Hour Hand",
            ComponentToDisplay = TimeComponent.Hour,
            Length = 48,
            Width = 10,
            TailLength = 12,
            StrokeThickness = 0,
            FillBrush = Brushes.RoyalBlue
        };

        yield return new DiamondHand
        {
            Name = "Minute Hand",
            ComponentToDisplay = TimeComponent.Minute,
            Length = 74,
            Width = 8,
            TailLength = 8,
            StrokeThickness = 0,
            FillBrush = Brushes.LimeGreen
        };

        yield return new SimpleHand
        {
            Name = "Second Hand",
            ComponentToDisplay = TimeComponent.Second,
            Length = 85,
            TailLength = 14,
            StrokeBrush = Brushes.Red,
            StrokeThickness = 1
        };
    }
}
