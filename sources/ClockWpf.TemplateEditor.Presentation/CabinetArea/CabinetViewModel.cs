using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.CabinetArea;

public class CabinetViewModel : ViewModelBase
{
    private ObservableCollection<ClockTemplateDescriptor> clockTemplates = [];

    public ObservableCollection<ClockTemplateDescriptor> ClockTemplates
    {
        get => clockTemplates;
        private set
        {
            if (clockTemplates == value)
                return;

            clockTemplates = value;
            OnPropertyChanged();
        }
    }

    public IMovement Movement
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public CabinetViewModel()
    {
        Movement = new LocalTimeMovement();
        Movement.Start();

        IEnumerable<Type> clockTemplateTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(t => typeof(ClockTemplate).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (Type clockTemplateType in clockTemplateTypes)
        {
            ClockTemplate clockTemplate = (ClockTemplate)Activator.CreateInstance(clockTemplateType);

            ClockTemplates.Add(new ClockTemplateDescriptor
            {
                ClockTemplate = clockTemplate,
                Name = clockTemplateType.AsClockTemplateMetadata().Name
            });
        }
    }
}
