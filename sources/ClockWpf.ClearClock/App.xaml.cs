using System.Windows;

namespace DustInTheWind.ClockWpf.ClearClock;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        ApplicationState applicationState = new();
        
        MainWindow mainWindow = new(applicationState);
        mainWindow.Show();

        MainWindow = mainWindow;
    }
}

