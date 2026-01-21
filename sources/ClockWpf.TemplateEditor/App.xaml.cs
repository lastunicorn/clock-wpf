using System.Windows;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;
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

        ServiceCollection serviceCollection = new();

        ApplicationState applicationState = CreateApplicationState();
        serviceCollection.AddSingleton(applicationState);

        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddTransient<MainViewModel>();

        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        MainWindow mainWindow = serviceProvider.GetService<MainWindow>();
        mainWindow.DataContext = serviceProvider.GetService<MainViewModel>();
        mainWindow.Show();

        MainWindow = mainWindow;
    }

    private static ApplicationState CreateApplicationState()
    {
        ApplicationState applicationState = new();

        LoadTemplates(applicationState);
        LoadTimeProviders(applicationState);

        return applicationState;
    }

    private static void LoadTemplates(ApplicationState applicationState)
    {
        List<Type> templateTypes = typeof(ClockTemplate).Assembly.GetTypes()
                    .Where(x => x.IsClass && !x.IsAbstract && typeof(ClockTemplate).IsAssignableFrom(x))
                    .ToList();

        applicationState.AvailableTemplateTypes = templateTypes;

        if (templateTypes.Count > 0)
        {
            Type selectedTemplateType = templateTypes
                .FirstOrDefault(x => x == typeof(DefaultTemplate));

            applicationState.CurrentTemplate = (ClockTemplate)Activator.CreateInstance(selectedTemplateType);
        }
    }

    private static void LoadTimeProviders(ApplicationState applicationState)
    {
        List<Type> timeProviderTypes = typeof(ITimeProvider).Assembly.GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && typeof(ITimeProvider).IsAssignableFrom(x))
            .ToList();

        applicationState.AvailableTimeProviderTypes = timeProviderTypes;

        if (timeProviderTypes.Count > 0)
        {
            Type selectedTimeProviderType = timeProviderTypes
                .FirstOrDefault(x => x == typeof(LocalTimeProvider));

            ITimeProvider timeProvider = (ITimeProvider)Activator.CreateInstance(selectedTimeProviderType);
            timeProvider.Start();

            applicationState.CurrentTimeProvider = timeProvider;
        }
    }
}

