using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MainArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MiscellaneousArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MovementsArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using DustInTheWind.ClockWpf.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ClockWpf.TemplateEditor;

internal static class Setup
{
    public static void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IClockTemplateFactory, ClockTemplateFactory>();
        serviceCollection.AddSingleton<IClockMovementFactory, ClockMovementFactory>();

        AddTemplates(serviceCollection);
        AddMovements(serviceCollection);

        serviceCollection.AddSingleton<ApplicationState>();

        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddTransient<MainViewModel>();

        serviceCollection.AddTransient<ClockViewModel>();
        serviceCollection.AddTransient<MiscellaneousViewModel>();
        serviceCollection.AddTransient<TemplatesViewModel>();
        serviceCollection.AddTransient<ShapesViewModel>();
        serviceCollection.AddTransient<MovementsViewModel>();
        serviceCollection.AddTransient<CabinetViewModel>();
        serviceCollection.AddTransient<AvailableShapesViewModel>();
        serviceCollection.AddTransient<InUseShapesViewModel>();
    }

    private static void AddTemplates(ServiceCollection serviceCollection)
    {
        IEnumerable<Type> templateTypes = EnumerateTemplateTypes();

        foreach (Type templateType in templateTypes)
            serviceCollection.AddTransient(templateType);

        serviceCollection.AddSingleton(serviceProvider =>
        {
            IClockTemplateFactory clockTemplateFactory = serviceProvider.GetService<IClockTemplateFactory>();
            WorkContextPool clockTemplatePool = new(clockTemplateFactory);

            IEnumerable<Type> templateTypes = EnumerateTemplateTypes();

            clockTemplatePool.AddRange(templateTypes);
            clockTemplatePool.OpenWorkContext<DefaultLightTemplate>();

            return clockTemplatePool;
        });
    }

    private static IEnumerable<Type> EnumerateTemplateTypes()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && typeof(ClockTemplate).IsAssignableFrom(x));
    }

    private static void AddMovements(ServiceCollection serviceCollection)
    {
        IEnumerable<Type> movementTypes = EnumerateMovementTypes();

        foreach (Type movementType in movementTypes)
            serviceCollection.AddTransient(movementType);

        serviceCollection.AddSingleton(serviceProvider =>
        {
            IClockMovementFactory clockMovementFactory = serviceProvider.GetService<IClockMovementFactory>();
            ClockMovementPool clockMovementPool = new(clockMovementFactory);

            IEnumerable<Type> movementTypes = EnumerateMovementTypes();

            clockMovementPool.AddRange(movementTypes);
            clockMovementPool.SetDefault<LocalTimeMovement>();

            return clockMovementPool;
        });
    }

    private static IEnumerable<Type> EnumerateMovementTypes()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && typeof(IMovement).IsAssignableFrom(x));
    }
}
