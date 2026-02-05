using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.State;

public interface IClockMovementFactory
{
    T Create<T>()
        where T : IMovement;

    IMovement Create(Type type);
}
