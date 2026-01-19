using System.Windows.Controls;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.ClearClock.Controls;

/// <summary>
/// Interaction logic for SettingsControl.xaml
/// </summary>
public partial class SettingsControl : UserControl
{
    public SettingsControl()
    {
        InitializeComponent();

        LocalTimeProvider localTimeProvider = new();
        localTimeProvider.Start();

        analogClock1.TimeProvider = localTimeProvider;
    }
}
