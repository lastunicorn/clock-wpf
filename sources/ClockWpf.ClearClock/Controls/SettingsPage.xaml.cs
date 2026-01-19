using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.ClockWpf.ClearClock.Controls;

/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : UserControl
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private void CloseSettingsButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
    }
}
