using System.Windows;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MainArea;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ClockWpf.TemplateEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        IServiceProvider serviceProvider = ConfigureServices();

        MainWindow = CreateAndShowMainWindow(serviceProvider);
    }

    private static IServiceProvider ConfigureServices()
    {
        ServiceCollection serviceCollection = new();
        Setup.ConfigureServices(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }

    private static MainWindow CreateAndShowMainWindow(IServiceProvider serviceProvider)
    {
        MainWindow mainWindow = serviceProvider.GetService<MainWindow>();
        mainWindow.DataContext = serviceProvider.GetService<MainViewModel>();
        mainWindow.Show();

        return mainWindow;
    }
}
