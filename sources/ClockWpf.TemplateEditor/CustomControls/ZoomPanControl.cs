using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.TemplateEditor.CustomControls;

public class ZoomPanControl : ContentControl
{
    #region ResetViewCommand DependencyProperty

    public static readonly DependencyProperty ResetViewCommandProperty = DependencyProperty.Register(
        nameof(ResetViewCommand),
        typeof(ICommand),
        typeof(ZoomPanControl),
        new PropertyMetadata(new ResetViewCommand()));

    internal ICommand ResetViewCommand
    {
        get => (ICommand)GetValue(ResetViewCommandProperty);
        private set => SetValue(ResetViewCommandProperty, value);
    }

    #endregion

    static ZoomPanControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomPanControl), new FrameworkPropertyMetadata(typeof(ZoomPanControl)));
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild("PART_Content") is FrameworkElement contentPresenter)
        {
            TransformGroup transformGroup = new();

            transformGroup.Children.Add(new ScaleTransform(1.0, 1.0));
            transformGroup.Children.Add(new TranslateTransform(0.0, 0.0));

            contentPresenter.RenderTransform = transformGroup;
        }
    }
}
