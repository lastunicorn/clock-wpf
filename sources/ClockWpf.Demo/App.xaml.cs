using System.Windows;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ClockWpf.Demo;

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

        List<Type> templateTypes = typeof(ClockTemplate).Assembly.GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(ClockTemplate)))
            .ToList();

        applicationState.AvailableTemplateTypes = templateTypes;

        if (templateTypes?.Count > 0)
        {
            Type selectedTemplateType = templateTypes
                .FirstOrDefault(x => x == typeof(DefaultTemplate));

            applicationState.CurrentTemplate = (ClockTemplate)Activator.CreateInstance(selectedTemplateType);
        }

        LocalTimeProvider localTimeProvider = new();
        localTimeProvider.Start();

        applicationState.CurrentTimeProvider = localTimeProvider;

        return applicationState;
    }
}

