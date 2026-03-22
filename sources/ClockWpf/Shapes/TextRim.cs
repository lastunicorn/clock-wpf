using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Templates.Shapes;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A rim shape that displays texts around the clock face.
/// </summary>
public class TextRim : RimBase
{
    #region Texts DependencyProperty

    public static readonly DependencyProperty TextsProperty = DependencyProperty.Register(
        nameof(Texts),
        typeof(string[]),
        typeof(TextRim),
        new FrameworkPropertyMetadata(null, HandleTextsChanged));

    private static void HandleTextsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextRim textRim)
        {
            textRim.InvalidateCache();
            textRim.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [Description("The array of texts that are rendered.")]
    public string[] Texts
    {
        get => (string[])GetValue(TextsProperty);
        set => SetValue(TextsProperty, value);
    }

    #endregion

    #region FontFamily DependencyProperty

    public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
        nameof(FontFamily),
        typeof(FontFamily),
        typeof(TextRim),
        new FrameworkPropertyMetadata(new FontFamily("Arial"), HandleFontFamilyChanged));

    private static void HandleFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextRim textRim)
        {
            textRim.InvalidateCache();
            textRim.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [Description("The font family used to draw the texts.")]
    public FontFamily FontFamily
    {
        get => (FontFamily)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    #endregion

    #region FontSize DependencyProperty

    public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
        nameof(FontSize),
        typeof(double),
        typeof(TextRim),
        new FrameworkPropertyMetadata(12.0, HandleFontSizeChanged));

    private static void HandleFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextRim textRim)
        {
            textRim.InvalidateCache();
            textRim.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [Description("The font size used to draw the texts.")]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    #endregion

    #region FontWeight DependencyProperty

    public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(
        nameof(FontWeight),
        typeof(FontWeight),
        typeof(TextRim),
        new FrameworkPropertyMetadata(FontWeights.Normal, HandleFontWeightChanged));

    private static void HandleFontWeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextRim textRim)
        {
            textRim.InvalidateCache();
            textRim.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [Description("The font weight used to draw the texts.")]
    public FontWeight FontWeight
    {
        get => (FontWeight)GetValue(FontWeightProperty);
        set => SetValue(FontWeightProperty, value);
    }

    #endregion

    private Typeface typeface;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        string[] texts = Texts;

        if (texts == null || texts.Length == 0)
            return false;

        typeface = new(FontFamily, FontStyles.Normal, FontWeight, FontStretches.Normal);

        return base.OnRendering(context);
    }

    protected override void RenderItem(ClockDrawingContext context, int index)
    {
        string[] texts = Texts;

        if (texts == null)
            return;

        if (index >= texts.Length)
            return;

        string text = texts[index];

        if (string.IsNullOrEmpty(text))
            return;

        double radius = context.ClockRadius;
        double calculateFontSize = FontSize.RelativeTo(radius);

        FormattedText formattedText = new(
            text,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            typeface,
            calculateFontSize,
            FillBrush,
            1.0);

        double textX = -formattedText.Width / 2;
        double textY = -formattedText.Height / 2;

        Point textPosition = new(textX, textY);
        context.DrawingContext.DrawText(formattedText, textPosition);
    }

    public override void Import(ShapeT shapeT)
    {
        base.Import(shapeT);

        if (shapeT is not TextRimT textRimT)
            return;

        Texts = textRimT.Texts;
        FontFamily = textRimT.FontFamily;
        FontSize = textRimT.FontSize;
        FontWeight = textRimT.FontWeight;
    }
}
