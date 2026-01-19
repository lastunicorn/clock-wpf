namespace DustInTheWind.ClockWpf.ClearClock.Controls;

public class SettingsPageModel : PageViewModel
{
    public SettingsViewModel SettingsViewModel { get; }

    public AboutViewModel AboutViewModel { get; }

    public SettingsCloseCommand SettingsCloseCommand { get; }

    public SettingsPageModel(ApplicationState applicationState, PageEngine pageEngine)
    {
        ArgumentNullException.ThrowIfNull(applicationState);

        SettingsViewModel = new SettingsViewModel(applicationState);
        AboutViewModel = new AboutViewModel();
        SettingsCloseCommand = new SettingsCloseCommand(pageEngine);
    }
}
