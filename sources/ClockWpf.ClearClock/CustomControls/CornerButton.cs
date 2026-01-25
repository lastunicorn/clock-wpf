using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.ClearClock.CustomControls;

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

    public CornerType Corner
    {
        get => (CornerType)GetValue(CornerTypeProperty);
        set => SetValue(CornerTypeProperty, value);
    }

    #endregion

    #region CornerRadius Dependency Property

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(double),
        typeof(CornerButton),
        new PropertyMetadata(0.1, HandleCornerRadiusPropertyChanged));

    private static void HandleCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CornerButton cornerButton)
            cornerButton.UpdateVisualElements();
    }

    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
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

    private void UpdateVisualElements()
    {
        switch (Corner)
        {
            case CornerType.TopLeft:
                Data = GenerateTopLeftGeometry();
                ContentHorizontalAlignment = HorizontalAlignment.Left;
                ContentVerticalAlignment = VerticalAlignment.Top;

                break;

            case CornerType.TopRight:
                Data = GenerateTopRightGeometry();
                ContentHorizontalAlignment = HorizontalAlignment.Right;
                ContentVerticalAlignment = VerticalAlignment.Top;
                break;

            case CornerType.BottomLeft:
                Data = GenerateBottomLeftGeometry();
                ContentHorizontalAlignment = HorizontalAlignment.Left;
                ContentVerticalAlignment = VerticalAlignment.Bottom;
                break;

            case CornerType.BottomRight:
                Data = GenerateBottomRightGeometry();
                ContentHorizontalAlignment = HorizontalAlignment.Right;
                ContentVerticalAlignment = VerticalAlignment.Bottom;
                break;

            default:
                return;
        }
    }

    private Geometry GenerateTopLeftGeometry()
    {
        PathFigure pathFigure = GenerateTopLeftPathFigure(CornerRadius);

        PathGeometry geometry = new()
        {
            Figures =
            {
                pathFigure
            }
        };

        geometry.Freeze();

        return geometry;
    }

    private Geometry GenerateTopRightGeometry()
    {
        PathFigure pathFigure = GenerateTopLeftPathFigure(CornerRadius);

        PathGeometry geometry = new()
        {
            Figures =
            {
                pathFigure
            },
            Transform = new RotateTransform(90, 0.5, 0.5)
        };

        geometry.Freeze();

        return geometry;
    }

    private Geometry GenerateBottomRightGeometry()
    {
        PathFigure pathFigure = GenerateTopLeftPathFigure(CornerRadius);

        PathGeometry geometry = new()
        {
            Figures =
            {
                pathFigure
            },
            Transform = new RotateTransform(180, 0.5, 0.5)
        };

        geometry.Freeze();

        return geometry;
    }

    private Geometry GenerateBottomLeftGeometry()
    {
        PathFigure pathFigure = GenerateTopLeftPathFigure(CornerRadius);

        PathGeometry geometry = new()
        {
            Figures =
            {
                pathFigure
            },
            Transform = new RotateTransform(270, 0.5, 0.5)
        };

        geometry.Freeze();

        return geometry;
    }

    private static PathFigure GenerateTopLeftPathFigure(double cornerRadius = 0.1)
    {
        // size = 1 x 1
        // corner radius = 0.1
        //
        // M 0.1865,0.9501 - Starts from bottom-left corner
        // A 0.1,0.1 0 0 1 0,0.9 - Draw the arc of the bottom-left corner
        // L 0,0.1 - Draw line to top-left corner
        // A 0.1,0.1 0 0 1 0.1,0 - Draw the arc of the top-left corner
        // L 0.9,0 - Draw line to top-right corner
        // A 0.1,0.1 0 0 1 0.9501,0.1865 - Draw arc of the top-right corner
        // A 2,2 0 0 1 0.1,1 - Draw big arc from top-right corner to bottom-left corner
        // Z - Close the path

        CircleTouch touchPointTopRight = new(1 - cornerRadius, cornerRadius, cornerRadius, 2, 2);
        CircleTouch touchPointBottomLeft = new(cornerRadius, 1 - cornerRadius, cornerRadius, 2, 2);

        return new PathFigure()
        {
            // Starts from bottom-left corner
            StartPoint = touchPointBottomLeft,
            IsClosed = true,
            Segments =
            {
                // Draw the arc of the bottom-left corner
                new ArcSegment
                {
                    Point = new Point(0, 1 - cornerRadius),
                    Size = new Size(cornerRadius, cornerRadius),
                    RotationAngle = 0,
                    IsLargeArc = false,
                    SweepDirection = SweepDirection.Clockwise
                },

                // Draw line to top-left corner
                new LineSegment
                {
                    Point = new Point(0, cornerRadius)
                },

                // Draw the arc of the top-left corner
                new ArcSegment
                {
                    Point = new Point(cornerRadius, 0),
                    Size = new Size(cornerRadius, cornerRadius),
                    RotationAngle = 0,
                    IsLargeArc = false,
                    SweepDirection = SweepDirection.Clockwise
                },

                // Draw line to top-right corner
                new LineSegment
                {
                    Point = new Point(1 - cornerRadius, 0)
                },

                // Draw arc of the top-right corner
                new ArcSegment
                {
                    Point = touchPointTopRight,
                    Size = new Size(cornerRadius, cornerRadius),
                    RotationAngle = 0,
                    IsLargeArc = false,
                    SweepDirection = SweepDirection.Clockwise
                },

                // Draw big arc from top-right corner to bottom-left corner
                new ArcSegment
                {
                    Point = touchPointBottomLeft,
                    Size = new Size(2, 2),
                    RotationAngle = 0,
                    IsLargeArc = false,
                    SweepDirection = SweepDirection.Counterclockwise
                }
            }
        };
    }
}
