using System.Reflection;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class WorkContextDescriptor : ViewModelBase
{
    public string Name { get; }

    public string Description { get; }

    public Type ClockTemplateType { get; }

    public WorkContextState WorkContextState
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

    public WorkContextDescriptor(WorkContext workContext)
    {
        ArgumentNullException.ThrowIfNull(workContext);

        Type clockTemplateType = workContext.ClockTemplateType;

        ClockTemplateType = clockTemplateType;

        TemplateAttribute templateAttribute = clockTemplateType.GetCustomAttribute<TemplateAttribute>();

        Name = templateAttribute?.Name ?? clockTemplateType.Name
            .Replace("ClockTemplate", "")
            .Replace("Template", "");

        Description = templateAttribute?.Description;

        workContext.StateChanged += HandleWorkContextStateChanged;
        WorkContextState = workContext.State;
    }

    private void HandleWorkContextStateChanged(object sender, EventArgs e)
    {
        if (sender is not WorkContext workContext)
            return;

        WorkContextState = workContext.State;
    }
}
