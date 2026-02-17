using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

public class SlidingPanel : ContentControl
{
    #region ToggleButton DependencyProperty

    public static readonly DependencyProperty ToggleButtonProperty = DependencyProperty.Register(
        nameof(ToggleButton),
        typeof(ToggleButton),
        typeof(SlidingPanel),
        new PropertyMetadata(null, OnToggleButtonChanged));

    public ToggleButton ToggleButton
    {
        get => (ToggleButton)GetValue(ToggleButtonProperty);
        set => SetValue(ToggleButtonProperty, value);
    }

    private static void OnToggleButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SlidingPanel panel)
            return;

        if (e.OldValue is ToggleButton oldToggleButton)
        {
            oldToggleButton.Checked -= panel.OnToggleButtonChecked;
            oldToggleButton.Unchecked -= panel.OnToggleButtonUnchecked;
        }

        if (e.NewValue is ToggleButton newToggleButton)
        {
            newToggleButton.Checked += panel.OnToggleButtonChecked;
            newToggleButton.Unchecked += panel.OnToggleButtonUnchecked;
        }
    }

    #endregion

    #region SlideDuration DependencyProperty

    public static readonly DependencyProperty SlideDurationProperty = DependencyProperty.Register(
        nameof(SlideDuration),
        typeof(TimeSpan),
        typeof(SlidingPanel),
        new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

    public TimeSpan SlideDuration
    {
        get => (TimeSpan)GetValue(SlideDurationProperty);
        set => SetValue(SlideDurationProperty, value);
    }

    #endregion

    static SlidingPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SlidingPanel), new FrameworkPropertyMetadata(typeof(SlidingPanel)));
    }

    private Border border;
    private TranslateTransform translateTransform;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (border != null)
            return;

        border = GetTemplateChild("PART_Border") as Border;

        if (border != null)
        {
            translateTransform = new TranslateTransform();

            if (border.RenderTransform == null || border.RenderTransform == Transform.Identity)
            {
                border.RenderTransform = translateTransform;
            }
            else
            {
                TransformGroup transformGroup = new();
                transformGroup.Children.Add(border.RenderTransform);
                transformGroup.Children.Add(translateTransform);
                border.RenderTransform = transformGroup;
            }
        }
    }

    private void OnToggleButtonChecked(object sender, RoutedEventArgs e)
    {
        Show();
    }

    private void OnToggleButtonUnchecked(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        if (border == null)
            return;

        border.Visibility = Visibility.Visible;
        UpdateLayout();

        double panelWidth = ActualWidth;

        if (panelWidth > 0)
        {
            DoubleAnimation slideInAnimation = new()
            {
                From = panelWidth,
                To = 0,
                Duration = SlideDuration,
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            translateTransform.BeginAnimation(TranslateTransform.XProperty, slideInAnimation);
        }
    }

    public void Hide()
    {
        if (border == null)
            return;

        double panelWidth = ActualWidth;

        if (panelWidth > 0)
        {
            DoubleAnimation slideOutAnimation = new()
            {
                To = panelWidth,
                Duration = SlideDuration,
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseIn
                }
            };

            slideOutAnimation.Completed += (s, args) =>
            {
                border.Visibility = Visibility.Collapsed;
            };

            translateTransform.BeginAnimation(TranslateTransform.XProperty, slideOutAnimation);
        }
        else
        {
            border.Visibility = Visibility.Collapsed;
        }
    }
}
