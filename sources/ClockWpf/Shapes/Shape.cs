using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes.Serialization;

namespace DustInTheWind.ClockWpf.Shapes;

public abstract class Shape : DependencyObject
{
    private bool isLayoutValid;

    #region Name DependencyProperty

    public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
        nameof(Name),
        typeof(string),
        typeof(Shape),
        new PropertyMetadata("Shape", OnNameChanged));

    private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Shape shape)
            shape.OnNameChanged(EventArgs.Empty);
    }

    /// <summary>
    /// A user friendly name. Used only to be displayed to the user. Does not influence the
    /// way the shape is rendered.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    [Description("A user friendly name. Used only to be displayed to the user. Does not influence the way the shape is rendered.")]
    public string Name
    {
        get => (string)GetValue(NameProperty);
        set
        {
            if (value == null)
                throw new ArgumentNullException("value");

            SetValue(NameProperty, value);
        }
    }

    #endregion

    #region IsVisilbe DependencyProperty

    public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(
        nameof(IsVisible),
        typeof(bool),
        typeof(Shape),
        new PropertyMetadata(true));

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("A value specifying if the shape should be rendered or not.")]
    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    #endregion

    #region FillBrush DependencyProperty

    public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register(
        nameof(FillBrush),
        typeof(Brush),
        typeof(Shape),
        new FrameworkPropertyMetadata(Brushes.CornflowerBlue));

    [Category("Appearance")]
    [DefaultValue(typeof(SolidColorBrush), "CornflowerBlue")]
    [Description("Gets or sets the brush used to draw the filling of the shape.")]
    public Brush FillBrush
    {
        get => (Brush)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }

    #endregion

    #region StrokeBrush DependencyProperty

    public static readonly DependencyProperty StrokeBrushProperty = DependencyProperty.Register(
        nameof(StrokeBrush),
        typeof(Brush),
        typeof(Shape),
        new FrameworkPropertyMetadata(Brushes.Black, HandleStrokeChanged));

    private static void HandleStrokeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Shape shape)
        {
            shape.strokePen = null;
            shape.isStrokePenCreated = false;
        }
    }

    [Category("Appearance")]
    [DefaultValue(typeof(SolidColorBrush), "Black")]
    [Description("Gets or sets the brush used to draw the stroke of the shape.")]
    public Brush StrokeBrush
    {
        get => (Brush)GetValue(StrokeBrushProperty);
        set => SetValue(StrokeBrushProperty, value);
    }

    #endregion

    #region StrokeThickness DependencyProperty

    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
        nameof(StrokeThickness),
        typeof(double),
        typeof(Shape),
        new FrameworkPropertyMetadata(1.0, HandleStrokeThicknessChanged));

    private static void HandleStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Shape shape)
        {
            shape.strokePen = null;
            shape.isStrokePenCreated = false;

            shape.InvalidateLayout();
        }
    }

    [Category("Appearance")]
    [DefaultValue(1.0)]
    [Description("The width of the outline.")]
    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    #endregion

    #region StrokePen Property

    private Pen strokePen;
    private bool isStrokePenCreated;

    protected Pen StrokePen
    {
        get
        {
            if (!isStrokePenCreated)
            {
                strokePen = CreateStrokePen();
                isStrokePenCreated = true;
            }

            return strokePen;
        }
    }

    protected virtual Pen CreateStrokePen()
    {
        return StrokeThickness > 0 && StrokeBrush != null
            ? new(StrokeBrush, StrokeThickness)
            : null;
    }

    #endregion

    #region Event NameChanged

    /// <summary>
    /// Event raised when the <see cref="Name"/> property is changed.
    /// </summary>
    public event EventHandler NameChanged;

    /// <summary>
    /// Raises the <see cref="NameChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
    protected virtual void OnNameChanged(EventArgs e)
    {
        NameChanged?.Invoke(this, e);
    }

    #endregion

    public void Render(ClockDrawingContext context)
    {
        if (!IsVisible)
            return;

        bool allowToRender = OnRendering(context);
        if (!allowToRender)
            return;

        if (!isLayoutValid)
        {
            CalculateLayout(context);
            isLayoutValid = true;
        }

        DoRender(context);

        OnRendered(context);
    }

    protected virtual bool OnRendering(ClockDrawingContext context)
    {
        if (FillBrush == null && StrokePen == null)
            return false;

        return true;
    }

    protected virtual void CalculateLayout(ClockDrawingContext context)
    {
    }

    public abstract void DoRender(ClockDrawingContext context);

    protected virtual void OnRendered(ClockDrawingContext context)
    {
    }

    protected void InvalidateLayout()
    {
        isLayoutValid = false;
    }

    protected virtual void InvalidateDrawingTools()
    {
        if (isStrokePenCreated)
        {
            strokePen = null;
            isStrokePenCreated = false;
        }
    }

    /// <summary>
    /// Serializes the properties of the shape into a dictionary of string key-value pairs.
    /// </summary>
    /// <returns>A dictionary containing the serialized property names and their values.</returns>
    public virtual Dictionary<string, string> Export()
    {
        return ShapeSerializer.Default.SerializeProperties(this);
    }

    /// <summary>
    /// Deserializes the shape properties from a dictionary of string key-value pairs.
    /// </summary>
    /// <param name="properties">A dictionary containing the property names and their serialized values.</param>
    public virtual void Import(Dictionary<string, string> properties)
    {
        ArgumentNullException.ThrowIfNull(properties);

        ShapeSerializer.Default.DeserializeProperties(this, properties);
    }
}
