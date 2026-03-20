using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf;

/// <summary>
/// A WPF control that displays an analog clock with customizable appearance and time movement logic.
/// </summary>
/// <remarks>
/// The <see cref="AnalogClock"/> control provides support for custom clock templates, shape collections, and movement
/// strategies through dependency properties. It enables data binding, styling, and animation via the WPF property
/// system. The control can be extended by assigning different implementations of <see cref="IMovement"/> to alter how time is
/// displayed and updated. The collection of shapes and the clock template can be customized to change the visual
/// appearance of the clock. The control is designed for use in WPF applications and supports theming and styling
/// through its default style key.
/// </remarks>
public class AnalogClock : Control
{
    #region PerformanceMeter DependencyProperty

    /// <summary>
    /// Identifies the <see cref="PerformanceMeter"/> dependency property.
    /// </summary>
    /// <remarks>
    /// This field is used to register and reference the <see cref="PerformanceMeter"/> property for the
    /// <see cref="AnalogClock"/> control. It enables styling, data binding, animation, and default value support for the property
    /// within the WPF property system.
    /// </remarks>
    public static readonly DependencyProperty PerformanceMeterProperty = DependencyProperty.Register(
        nameof(PerformanceMeter),
        typeof(PerformanceMeter),
        typeof(AnalogClock));

    /// <summary>
    /// Gets or sets the performance meter used to track and measure performance metrics for the current instance.
    /// </summary>
    public PerformanceMeter PerformanceMeter
    {
        get => (PerformanceMeter)GetValue(PerformanceMeterProperty);
        set => SetValue(PerformanceMeterProperty, value);
    }

    #endregion

    #region Shapes DependencyProperty

    private static readonly DependencyPropertyKey ShapesPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(Shapes),
        typeof(ObservableCollection<Shape>),
        typeof(AnalogClock),
        new PropertyMetadata());

    /// <summary>
    /// Identifies the <see cref="Shapes"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapesProperty = ShapesPropertyKey.DependencyProperty;

    /// <summary>
    /// Gets the collection of shapes displayed by the clock.
    /// </summary>
    /// <remarks>
    /// The collection is observable, so changes to its contents are automatically reflected in the
    /// UI. Assigning a new collection will replace the current set of shapes displayed.
    /// </remarks>
    public ObservableCollection<Shape> Shapes
    {
        get => (ObservableCollection<Shape>)GetValue(ShapesProperty);
        private set => SetValue(ShapesPropertyKey, value);
    }

    #endregion

    #region IsEmpty DependencyProperty

    private static readonly DependencyPropertyKey IsEmptyPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(IsEmpty),
        typeof(bool),
        typeof(AnalogClock),
        new PropertyMetadata(true));

    /// <summary>
    /// Identifies the IsEmpty dependency property.
    /// </summary>
    /// <remarks>
    /// This field is used to register and reference the IsEmpty property with the WPF property
    /// system. It enables styling, data binding, animation, and default value support for the IsEmpty property on
    /// AnalogClock controls.
    /// </remarks>
    public static readonly DependencyProperty IsEmptyProperty = IsEmptyPropertyKey.DependencyProperty;

    /// <summary>
    /// Gets a value indicating whether the current instance contains no shapes.
    /// </summary>
    public bool IsEmpty
    {
        get => (bool)GetValue(IsEmptyProperty);
        private set => SetValue(IsEmptyPropertyKey, value);
    }

    #endregion

    #region Movement DependencyProperty

    /// <summary>
    /// Identifies the Movement dependency property, which determines the time movement logic used by the analog clock
    /// control.
    /// </summary>
    /// <remarks>This property enables customization of how the clock's hands move by allowing different
    /// implementations of the IMovement interface. The default value is an instance of LocalTimeMovement. Changing this
    /// property affects how the clock displays and updates time.</remarks>
    public static readonly DependencyProperty MovementProperty = DependencyProperty.Register(
        nameof(Movement),
        typeof(IMovement),
        typeof(AnalogClock),
        new PropertyMetadata(new LocalTimeMovement()));

    /// <summary>
    /// Gets or sets the movement instance.
    /// The movement is responsible for the displayed time value and the frequence of updating it.
    /// </summary>
    /// <remarks>
    /// Assign an implementation of the IMovement interface to customize how the object moves.
    /// Changing this property at runtime updates the movement logic immediately.
    /// </remarks>
    public IMovement Movement
    {
        get => (IMovement)GetValue(MovementProperty);
        set => SetValue(MovementProperty, value);
    }

    #endregion

    #region ClockTemplate DependencyProperty

    /// <summary>
    /// Identifies the ClockTemplate dependency property.
    /// </summary>
    /// <remarks>
    /// This field is used to register and reference the ClockTemplate property for the AnalogClock
    /// control. It enables styling, data binding, animation, and other WPF property system services for the
    /// ClockTemplate property.
    /// </remarks>
    public static readonly DependencyProperty ClockTemplateProperty = DependencyProperty.Register(
        nameof(ClockTemplate),
        typeof(ClockTemplate),
        typeof(AnalogClock),
        new PropertyMetadata(null, HandleClockTemplateChanged));

    private static void HandleClockTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AnalogClock analogClock)
        {
            analogClock.Shapes.Clear();

            if (e.NewValue is ClockTemplate clockTemplate)
            {
                if (clockTemplate == null)
                    return;

                foreach (Shape shape in clockTemplate)
                    analogClock.Shapes.Add(shape);
            }
        }
    }

    /// <summary>
    /// Gets or sets the template used to display the clock.
    /// </summary>
    public ClockTemplate ClockTemplate
    {
        get => (ClockTemplate)GetValue(ClockTemplateProperty);
        set => SetValue(ClockTemplateProperty, value);
    }

    #endregion

    #region RotationDirection DependencyProperty

    /// <summary>
    /// Identifies the RotationDirection dependency property.
    /// </summary>
    /// <remarks>
    /// This field is used to register and reference the <see cref="RotationDirection"/> property for the
    /// <see cref="AnalogClock"/> control. It enables styling, data binding, animation, and default value support for the
    /// <see cref="RotationDirection"/> property in XAML.
    /// </remarks>
    public static readonly DependencyProperty RotationDirectionProperty = DependencyProperty.Register(
        nameof(RotationDirection),
        typeof(RotationDirection),
        typeof(AnalogClock),
        new FrameworkPropertyMetadata(RotationDirection.Clockwise));

    /// <summary>
    /// Gets or sets the direction in which the hands rotate.
    /// </summary>
    /// <remarks>
    /// Use this property to specify whether the hands move in a clockwise or counterclockwise
    /// direction. Changing this value affects the visual rotation behavior of the control.
    /// </remarks>
    [Category("Behavior")]
    [DefaultValue(RotationDirection.Clockwise)]
    [Description("Specifies the direction of rotation for the hands (clockwise or counterclockwise).")]
    public RotationDirection RotationDirection
    {
        get => (RotationDirection)GetValue(RotationDirectionProperty);
        set => SetValue(RotationDirectionProperty, value);
    }

    #endregion

    /// <summary>
    /// Initializes static members of the AnalogClock class and overrides the default style metadata for the control.
    /// </summary>
    /// <remarks>
    /// This static constructor ensures that the AnalogClock control uses its custom style by
    /// associating the control's type with its default style key. This is necessary for proper theming and styling in
    /// WPF applications.
    /// </remarks>
    static AnalogClock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock), new FrameworkPropertyMetadata(typeof(AnalogClock)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AnalogClock"/> class.
    /// </summary>
    public AnalogClock()
    {
        Shapes = [];
        Shapes.CollectionChanged += HandleShapesCollectionChanged;
    }

    private void HandleShapesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        IsEmpty = Shapes.Count == 0;
    }
}
