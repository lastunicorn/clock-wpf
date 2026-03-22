using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Templates.Shapes;

public class TextShapeT : ShapeT
{
    [Category("Appearance")]
    [DefaultValue("Dust in the Wind")]
    [Description("The text that is drawn.")]
    public string Text { get; set; }

    [Category("Appearance")]
    [Description("The font family used to draw the texts.")]
    public FontFamily FontFamily { get; set; }

    [Category("Appearance")]
    [DefaultValue(12.0)]
    [Description("The font size used to draw the texts.")]
    public double FontSize { get; set; }

    [Category("Appearance")]
    [Description("The font weight used to draw the texts.")]
    public FontWeight FontWeight { get; set; }

    [Category("Layout")]
    [DefaultValue(100f)]
    [Description("The maximum width of the rectangle where the text should be drawn.")]
    public float MaxWidth { get; set; }

    [Category("Layout")]
    [DefaultValue(50.0)]
    [Description("The vertical position where the text is drawn, expressed as percentage from the clock's radius.")]
    public double Y { get; set; }
}
