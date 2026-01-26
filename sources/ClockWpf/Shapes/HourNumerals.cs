using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

public class HourNumerals : TextRim
{
    static HourNumerals()
    {
        DistanceFromEdgeProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(25.0));
        FontFamilyProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(new FontFamily("Arial")));
        FontSizeProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(22.0));
        FontWeightProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(FontWeights.Normal));
        TextsProperty.OverrideMetadata(typeof(HourNumerals), GenerateHourNumbers());
        AngleProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(30.0));
        OffsetAngleProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(30.0));
        OrientationProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(RimItemOrientation.Normal));
    }

    private static FrameworkPropertyMetadata GenerateHourNumbers()
    {
        return new FrameworkPropertyMetadata(Enumerable.Range(1, 12)
            .Select(x => x.ToString())
            .ToArray());
    }
}
