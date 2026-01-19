using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DustInTheWind.ClockWpf.ClearClock.CustomControls;

/// <summary>
/// Interaction logic for RoundPage.xaml
/// </summary>
public partial class RoundPage : ContentControl
{
    #region CloseCommand Dependency Property

    public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
        nameof(CloseCommand),
        typeof(ICommand),
        typeof(RoundPage),
        new PropertyMetadata(default(ICommand)));

    public ICommand CloseCommand
    {
        get => (ICommand)GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }

    #endregion

    static RoundPage()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundPage), new FrameworkPropertyMetadata(typeof(RoundPage)));
    }
}
