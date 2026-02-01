using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

internal class BobTemplate : ClockTemplate
{
    protected override IEnumerable<Shape> CreateShapes()
    {
        FlatBackground background = new()
        {
            Name = "Background",
            FillBrush = Brushes.WhiteSmoke,
            StrokeThickness = 22
        };
        yield return background;

        CapsuleHand hourHand = new()
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 44,
            Width = 18,
            TailLength = 9,
            StrokeThickness = 0,
            FillBrush = Brushes.Black
        };
        yield return hourHand;

        CapsuleHand minuteHand = new()
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 69,
            Width = 18,
            TailLength = 9,
            StrokeThickness = 0,
            FillBrush = Brushes.Black
        };
        yield return minuteHand;
    }
}
