using DustInTheWind.ClockWpf.ClearClock.Controls;

namespace DustInTheWind.ClockWpf.ClearClock;

public class MainViewModel : ViewModelBase
{
    public ClockPageModel ClockPageModel { get; }

    public SettingsPageModel SettingsPageModel { get; }

    public MainViewModel(ApplicationState applicationState)
    {
        ArgumentNullException.ThrowIfNull(applicationState);

        ClockPageModel = new ClockPageModel(applicationState);
        SettingsPageModel = new SettingsPageModel(applicationState);
    }
}
