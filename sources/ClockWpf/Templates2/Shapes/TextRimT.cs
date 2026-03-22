using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Templates2.Shapes;

public class TextRimT : RimBaseT
{
    [Category("Appearance")]
    [Description("The array of texts that are rendered.")]
    public string[] Texts { get; set; }

    [Category("Appearance")]
    [Description("The font family used to draw the texts.")]
    public FontFamily FontFamily { get; set; }

    [Category("Appearance")]
    [Description("The font size used to draw the texts.")]
    public double FontSize { get; set; }

    [Category("Appearance")]
    [Description("The font weight used to draw the texts.")]
    public FontWeight FontWeight { get; set; }
}
