namespace DustInTheWind.ClockWpf.ClearClock.Controls;

public class SettingsPageModel : ViewModelBase
{
    public SettingsViewModel SettingsViewModel { get; }

    public AboutViewModel AboutViewModel { get; }

    public SettingsPageModel(ApplicationState applicationState)
    {
        ArgumentNullException.ThrowIfNull(applicationState);

        SettingsViewModel = new SettingsViewModel(applicationState);
        AboutViewModel = new AboutViewModel();
    }
}
