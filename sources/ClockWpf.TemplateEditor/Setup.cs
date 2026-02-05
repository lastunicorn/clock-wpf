using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation;
using DustInTheWind.ClockWpf.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ClockWpf.TemplateEditor;

internal static class Setup
{
    public static void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IClockTemplateFactory, TemplateFactory>();

        IEnumerable<Type> templateTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && typeof(ClockTemplate).IsAssignableFrom(x));

        foreach (Type templateType in templateTypes)
            serviceCollection.AddTransient(templateType);

        serviceCollection.AddSingleton(serviceProvider =>
        {
            IClockTemplateFactory clockTemplateFactory = serviceProvider.GetService<IClockTemplateFactory>();
            ClockTemplatePool clockTemplatePool = new(clockTemplateFactory);

            IEnumerable<Type> templateTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract && typeof(ClockTemplate).IsAssignableFrom(x));

            clockTemplatePool.AddRange(templateTypes);
            clockTemplatePool.SetDefault<DefaultTemplate>();
            
            return clockTemplatePool;
        });

        ApplicationState applicationState = CreateApplicationState();
        serviceCollection.AddSingleton(applicationState);

        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddTransient<MainViewModel>();
    }

    private static ApplicationState CreateApplicationState()
    {
        ApplicationState applicationState = new();

        LoadMovements(applicationState);

        return applicationState;
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
