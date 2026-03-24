using DustInTheWind.ClockWpf.TemplateEditor.State;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class WorkContextDescriptor : ViewModelBase
{
    public string TemplateName { get; }

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

        ClockTemplateType = workContext.ClockTemplateType;
        TemplateName = workContext.TemplateName;
        Description = workContext.Description;

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
