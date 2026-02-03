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
        TextsProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(GenerateHourNumbers(), HandleTextsChanged));
        AngleProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(30.0));
        OffsetAngleProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(30.0));
        OrientationProperty.OverrideMetadata(typeof(HourNumerals), new FrameworkPropertyMetadata(RimItemOrientation.Normal));
    }

    private static string[] GenerateHourNumbers()
    {
        return Enumerable.Range(1, 12)
            .Select(x => x.ToString())
            .ToArray();
    }

    private static void HandleTextsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is HourNumerals hourNumerals)
        {
            hourNumerals.InvalidateCache();
            hourNumerals.OnChanged(EventArgs.Empty);
        }
    }

    public HourNumerals()
    {
        Name = "Hour Numerals";
    }
}
