using System.Windows;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class HourNumeralsT : TextRimT
{
    public HourNumeralsT()
    {
        Name = "Hour Numerals";
        DistanceFromEdge = 25;
        FontSize = 22;
        FontWeight = FontWeights.Normal;
        Texts = GenerateHourNumbers();
        Angle = 30;
        OffsetAngle = 30;
        Orientation = RimItemOrientation.Normal;
    }

    private static string[] GenerateHourNumbers()
    {
        return Enumerable.Range(1, 12)
            .Select(x => x.ToString())
            .ToArray();
    }
}
