using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// The <see cref="IShape"/> class used by default in <see cref="AnalogClock"/> to draw the text displayed on the background of the dial.
/// </summary>
public class TextShape : Shape
{
    /// <summary>
    /// The default name for the Shape.
    /// </summary>
    public const string DefaultName = "Text";

    #region Text DependencyProperty

    /// <summary>
    /// The default text drawn.
    /// </summary>
    public const string DefaultText = "Dust in the Wind";

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(TextShape),
        new FrameworkPropertyMetadata(DefaultText, FrameworkPropertyMetadataOptions.AffectsRender, HandleTextChanged));

    private static void HandleTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextShape textShape)
            textShape.InvalidateCache();
    }

    [Category("Appearance")]
    [DefaultValue(DefaultText)]
    [Description("The text that is drawn.")]
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #region FontFamily DependencyProperty

    public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
        nameof(FontFamily),
        typeof(FontFamily),
        typeof(TextShape),
        new FrameworkPropertyMetadata(new FontFamily("Arial"), HandleFontFamilyChanged));

    private static void HandleFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextShape textShape)
            textShape.InvalidateCache();
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
        typeof(TextShape),
        new FrameworkPropertyMetadata(12.0, HandleFontSizeChanged));

    private static void HandleFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextShape textShape)
            textShape.InvalidateCache();
    }

    [Category("Appearance")]
    [DefaultValue(12.0)]
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
        typeof(TextShape),
        new FrameworkPropertyMetadata(FontWeights.Normal, HandleFontWeightChanged));

    private static void HandleFontWeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextShape textShape)
            textShape.InvalidateCache();
    }

    [Category("Appearance")]
    [Description("The font weight used to draw the texts.")]
    public FontWeight FontWeight
    {
        get => (FontWeight)GetValue(FontWeightProperty);
        set => SetValue(FontWeightProperty, value);
    }

    #endregion

    #region MaxWidth DependencyProperty

    /// <summary>
    /// The maximum width of the rectangle where the text should be drawn.
    /// </summary>
    public const float DefaultMaxWidth = 100f;

    public static readonly DependencyProperty MaxWidthProperty = DependencyProperty.Register(
        nameof(MaxWidth),
        typeof(float),
        typeof(TextShape),
        new FrameworkPropertyMetadata(DefaultMaxWidth, FrameworkPropertyMetadataOptions.AffectsRender, HandleMaxWidthChanged));

    private static void HandleMaxWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextShape textShape)
            textShape.InvalidateCache();
    }

    /// <summary>
    /// Get s or sets the maximum width of the rectangle where the text should be drawn.
    /// </summary>
    [Category("Layout")]
    [DefaultValue(DefaultMaxWidth)]
    [Description("The maximum width of the rectangle where the text should be drawn.")]
    public float MaxWidth
    {
        get => (float)GetValue(MaxWidthProperty);
        set => SetValue(MaxWidthProperty, value);
    }

    #endregion

    #region Location DependencyProperty

    /// <summary>
    /// The default vertical location of the text.
    /// </summary>
    public static Point DefaultLocation = new(0f, 0f);

    public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
        nameof(Location),
        typeof(Point),
        typeof(TextShape),
        new FrameworkPropertyMetadata(DefaultLocation, FrameworkPropertyMetadataOptions.AffectsRender, HandleLocationChanged));

    private static void HandleLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextShape textShape)
            textShape.InvalidateCache();
    }

    /// <summary>
    /// Gets or sets the location where the text is drawn.
    /// The X and Y values are percentages relative to the clock's radius.
    /// </summary>
    [Category("Layout")]
    [DefaultValue(typeof(Point), "0;0")]
    [TypeConverter(typeof(PointConverter))]
    [Description("The location where the text is drawn.")]
    public Point Location
    {
        get => (Point)GetValue(LocationProperty);
        set => SetValue(LocationProperty, value);
    }

    #endregion

    static TextShape()
    {
        NameProperty.OverrideMetadata(typeof(TextShape), new FrameworkPropertyMetadata(DefaultName));
    }

    private FormattedText formattedText;
    private Point textPosition;

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        Typeface typeface = new(FontFamily, FontStyles.Normal, FontWeight, FontStretches.Normal);

        formattedText = new FormattedText(
            Text,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            typeface,
            FontSize,
            FillBrush,
            1.0);

        formattedText.MaxTextWidth = MaxWidth;

        Point location = Location;
        double textX = context.ClockRadius * (location.X / 100.0) - formattedText.Width / 2;
        double textY = context.ClockRadius * (location.Y / 100.0) - formattedText.Height / 2;

        textPosition = new Point(textX, textY);
    }

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (string.IsNullOrEmpty(Text))
            return false;

        return base.OnRendering(context);
    }

    public override void DoRender(ClockDrawingContext context)
    {
        context.DrawingContext.DrawText(formattedText, textPosition);
    }
}
