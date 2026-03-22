using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public class BobTemplate : ClockTemplate
{
    protected override IEnumerable<ShapeT> CreateShapes()
    {
        FlatBackgroundT background = new()
        {
            Name = "Background",
            FillBrush = Brushes.WhiteSmoke,
            StrokeThickness = 22
        };
        yield return background;

        LineHandT hourHand = new()
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 44,
            TailLength = 9,
            StrokeThickness = 18,
            FillBrush = Brushes.Black,
            RoundEnds = true
        };
        yield return hourHand;

        LineHandT minuteHand = new()
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 69,
            TailLength = 9,
            StrokeThickness = 18,
            FillBrush = Brushes.Black,
            RoundEnds = true
        };
        yield return minuteHand;
    }
}
