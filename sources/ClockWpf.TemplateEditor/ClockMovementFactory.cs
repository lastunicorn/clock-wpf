using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ClockWpf.TemplateEditor;

public class ClockMovementFactory : IClockMovementFactory
{
    private readonly IServiceProvider serviceProvider;

    public ClockMovementFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public T Create<T>()
        where T : IMovement
    {
        return serviceProvider.GetService<T>();
    }

    public IMovement Create(Type type)
    {
        return (IMovement)serviceProvider.GetService(type);
    }
}