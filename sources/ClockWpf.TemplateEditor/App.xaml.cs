using System.Windows;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.Movements;
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
        LoadMovements(applicationState);

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

    private static void LoadMovements(ApplicationState applicationState)
    {
        List<Type> movementTypes = typeof(IMovement).Assembly.GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && typeof(IMovement).IsAssignableFrom(x))
            .ToList();

        applicationState.AvailableMovementTypes = movementTypes;

        if (movementTypes.Count > 0)
        {
            Type selectedMovementType = movementTypes
                .FirstOrDefault(x => x == typeof(LocalTimeMovement));

            IMovement movement = (IMovement)Activator.CreateInstance(selectedMovementType);
            movement.Start();

            applicationState.CurrentMovement = movement;
        }
    }
}

