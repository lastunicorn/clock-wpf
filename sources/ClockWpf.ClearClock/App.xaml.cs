using System.Windows;
using DustInTheWind.ClockWpf.ClearClock.Controls;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.ClearClock;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ApplicationState applicationState = CreateApplicationState();
        PageEngine pageEngine = CreatePageEngine();

        MainWindow mainWindow = new()
        {
            DataContext = new MainViewModel(applicationState, pageEngine)
        };
        mainWindow.Show();

        MainWindow = mainWindow;
    }

    private static ApplicationState CreateApplicationState()
    {
        ApplicationState applicationState = new();

        List<Type> templateTypes = EnumerateClockTemplates().ToList();
        applicationState.AvailableTemplateTypes = templateTypes;

        if (templateTypes?.Count > 0)
        {
            Type selectedTemplateType = templateTypes
             .FirstOrDefault(x => x == typeof(PlayfulTemplate));

            applicationState.ClockTemplate = (ClockTemplate)Activator.CreateInstance(selectedTemplateType);
        }

        return applicationState;
    }

    private static IEnumerable<Type> EnumerateClockTemplates()
    {
        return typeof(ClockTemplate).Assembly.GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(ClockTemplate)));
    }

    private static PageEngine CreatePageEngine()
    {
        PageEngine pageEngine = new();

        pageEngine.Pages.Add(new Page
        {
            Id = "clock",
            ViewType = typeof(ClockPage)
        });

        pageEngine.Pages.Add(new Page
        {
            Id = "settings",
            ViewType = typeof(SettingsPage)
        });

        pageEngine.SelectPage("clock");

        return pageEngine;
    }
}

