using System.Diagnostics.PerformanceData;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.ClearClock.Controls;

public class CornerButton : Button
{
    #region CornerType Dependency Property

    public static readonly DependencyProperty CornerTypeProperty = DependencyProperty.Register(
        nameof(Corner),
        typeof(CornerType),
        typeof(CornerButton),
        new PropertyMetadata(CornerType.TopLeft, HandlePropertyChanged));

    private static void HandlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CornerButton cornerButton)
            cornerButton.UpdateVisualElements();
    }

    private void UpdateVisualElements()
    {
        switch (Corner)
        {
            case CornerType.TopLeft:
                Data = Geometry.Parse("M 0,1 A 2,2 0 0 1 1,0 L 0,0 Z");
                ContentHorizontalAlignment = HorizontalAlignment.Left;
                ContentVerticalAlignment = VerticalAlignment.Top;
                break;

            case CornerType.TopRight:
                Data = Geometry.Parse("M 0,0 A 2,2 0 0 1 1,1 L 1,0 Z");
                ContentHorizontalAlignment = HorizontalAlignment.Right;
                ContentVerticalAlignment = VerticalAlignment.Top;
                break;

            case CornerType.BottomLeft:
                Data = Geometry.Parse("M 0,0 A 2,2 0 0 0 1,1 L 0,1 Z");
                ContentHorizontalAlignment = HorizontalAlignment.Left;
                ContentVerticalAlignment = VerticalAlignment.Bottom;
                break;

            case CornerType.BottomRight:
                Data = Geometry.Parse("M 0,1 A 2,2 0 0 0 1,0 L 1,1 Z");
                ContentHorizontalAlignment = HorizontalAlignment.Right;
                ContentVerticalAlignment = VerticalAlignment.Bottom;
                break;

            default:
                return;
        }
    }

    public CornerType Corner
    {
        get => (CornerType)GetValue(CornerTypeProperty);
        set => SetValue(CornerTypeProperty, value);
    }

    #endregion

    #region Data Dependency Property

    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
        nameof(Data),
        typeof(Geometry),
        typeof(CornerButton),
        new PropertyMetadata(null));

    public Geometry Data
    {
        get => (Geometry)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    #endregion

    #region ContentHorizontalAlignment Dependency Property

    public static readonly DependencyProperty ContentHorizontalAlignmentProperty = DependencyProperty.Register(
        nameof(ContentHorizontalAlignment),
        typeof(HorizontalAlignment),
        typeof(CornerButton),
        new PropertyMetadata(HorizontalAlignment.Left));

    public HorizontalAlignment ContentHorizontalAlignment
    {
        get => (HorizontalAlignment)GetValue(ContentHorizontalAlignmentProperty);
        set => SetValue(ContentHorizontalAlignmentProperty, value);
    }

    #endregion

    #region ContentVerticalAlignment Dependency Property

    public static readonly DependencyProperty ContentVerticalAlignmentProperty = DependencyProperty.Register(
        nameof(ContentVerticalAlignment),
        typeof(VerticalAlignment),
        typeof(CornerButton),
        new PropertyMetadata(VerticalAlignment.Top));

    public VerticalAlignment ContentVerticalAlignment
    {
        get => (VerticalAlignment)GetValue(ContentVerticalAlignmentProperty);
        set => SetValue(ContentVerticalAlignmentProperty, value);
    }

    #endregion

    #region HoverBackground Dependency Property

    public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
        nameof(HoverBackground),
        typeof(Brush),
        typeof(CornerButton),
        new PropertyMetadata(null));

    public Brush HoverBackground
    {
        get => (Brush)GetValue(HoverBackgroundProperty);
        set => SetValue(HoverBackgroundProperty, value);
    }

    #endregion

    static CornerButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerButton), new FrameworkPropertyMetadata(typeof(CornerButton)));
    }

    public CornerButton()
    {
        UpdateVisualElements();
    }
}
