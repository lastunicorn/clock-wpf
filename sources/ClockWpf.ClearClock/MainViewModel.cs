using DustInTheWind.ClockWpf.ClearClock.Controls;

namespace DustInTheWind.ClockWpf.ClearClock;

public class MainViewModel : ViewModelBase
{
    public SettingsPageModel SettingsPageModel { get; }

    public MainViewModel(ApplicationState applicationState)
    {
        ArgumentNullException.ThrowIfNull(applicationState);
        SettingsPageModel = new SettingsPageModel(applicationState);
    }
}
