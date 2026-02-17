using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Behaviors;

public static class SlidingPanelBehavior
{
    public static readonly DependencyProperty ToggleButtonProperty = DependencyProperty.RegisterAttached(
        "ToggleButton",
        typeof(ToggleButton),
        typeof(SlidingPanelBehavior),
        new PropertyMetadata(null, OnToggleButtonChanged));

    public static readonly DependencyProperty SlideDurationProperty = DependencyProperty.RegisterAttached(
        "SlideDuration",
        typeof(TimeSpan),
        typeof(SlidingPanelBehavior),
        new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

    public static ToggleButton GetToggleButton(DependencyObject obj)
    {
        return (ToggleButton)obj.GetValue(ToggleButtonProperty);
    }

    public static void SetToggleButton(DependencyObject obj, ToggleButton value)
    {
        obj.SetValue(ToggleButtonProperty, value);
    }

    public static TimeSpan GetSlideDuration(DependencyObject obj)
    {
        return (TimeSpan)obj.GetValue(SlideDurationProperty);
    }

    public static void SetSlideDuration(DependencyObject obj, TimeSpan value)
    {
        obj.SetValue(SlideDurationProperty, value);
    }

    private static void OnToggleButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement panel)
            return;

        if (e.OldValue is ToggleButton oldToggleButton)
        {
            oldToggleButton.Checked -= OnToggleButtonChecked;
            oldToggleButton.Unchecked -= OnToggleButtonUnchecked;
        }

        if (e.NewValue is ToggleButton newToggleButton)
        {
            newToggleButton.Checked += OnToggleButtonChecked;
            newToggleButton.Unchecked += OnToggleButtonUnchecked;
        }
    }

    private static void OnToggleButtonChecked(object sender, RoutedEventArgs e)
    {
        ToggleButton toggleButton = (ToggleButton)sender;
        FrameworkElement panel = FindPanelForToggleButton(toggleButton);

        if (panel == null)
            return;

        panel.Visibility = Visibility.Visible;
        panel.UpdateLayout();

        double panelWidth = panel.ActualWidth;

        if (panelWidth > 0)
        {
            TimeSpan duration = GetSlideDuration(panel);

            DoubleAnimation slideInAnimation = new()
            {
                From = panelWidth,
                To = 0,
                Duration = duration,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            panel.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideInAnimation);
        }
    }

    private static void OnToggleButtonUnchecked(object sender, RoutedEventArgs e)
    {
        ToggleButton toggleButton = (ToggleButton)sender;
        FrameworkElement panel = FindPanelForToggleButton(toggleButton);

        if (panel == null)
            return;

        double panelWidth = panel.ActualWidth;

        if (panelWidth > 0)
        {
            TimeSpan duration = GetSlideDuration(panel);

            DoubleAnimation slideOutAnimation = new()
            {
                To = panelWidth,
                Duration = duration,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };

            slideOutAnimation.Completed += (s, args) =>
            {
                panel.Visibility = Visibility.Collapsed;
            };

            panel.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideOutAnimation);
        }
        else
        {
            panel.Visibility = Visibility.Collapsed;
        }
    }

    private static FrameworkElement FindPanelForToggleButton(ToggleButton toggleButton)
    {
        DependencyObject current = toggleButton;

        while (current != null)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(current);

            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(current, i);

                if (child is FrameworkElement element && GetToggleButton(element) == toggleButton)
                    return element;

                FrameworkElement found = FindPanelInTree(child, toggleButton);

                if (found != null)
                    return found;
            }

            current = VisualTreeHelper.GetParent(current);
        }

        return null;
    }

    private static FrameworkElement FindPanelInTree(DependencyObject parent, ToggleButton toggleButton)
    {
        int childCount = VisualTreeHelper.GetChildrenCount(parent);

        for (int i = 0; i < childCount; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);

            if (child is FrameworkElement element && GetToggleButton(element) == toggleButton)
                return element;

            FrameworkElement found = FindPanelInTree(child, toggleButton);

            if (found != null)
                return found;
        }

        return null;
    }
}
