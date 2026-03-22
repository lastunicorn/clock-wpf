using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.State;

public interface IClockTemplateFactory
{
    T Create<T>()
        where T : ClockTemplate;

    ClockTemplate Create(Type type);
}
