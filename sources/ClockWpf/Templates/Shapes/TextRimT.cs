using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class TextRimT : RimBaseT
{
    public string[] Texts { get; set; }

    public FontFamily FontFamily { get; set; } = new FontFamily("Arial");

    public double FontSize { get; set; } = 12.0;

    public FontWeight FontWeight { get; set; } = FontWeights.Normal;
}
