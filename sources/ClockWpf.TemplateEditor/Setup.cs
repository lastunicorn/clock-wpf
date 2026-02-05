using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation;
using DustInTheWind.ClockWpf.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ClockWpf.TemplateEditor;

internal static class Setup
{
    public static void ConfigureServices(ServiceCollection serviceCollection)
    {
        ApplicationState applicationState = CreateApplicationState();
        serviceCollection.AddSingleton(applicationState);

        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddTransient<MainViewModel>();
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