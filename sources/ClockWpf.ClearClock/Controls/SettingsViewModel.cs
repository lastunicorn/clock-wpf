using System.Collections.ObjectModel;
using System.Reflection;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.ClearClock.Controls;

public class SettingsViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

    public ObservableCollection<TemplateItemModel> TemplateTypes { get; } = [];

    public TemplateItemModel SelectedTemplateType
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();

            PublishTemplate(field);
        }
    }

    public ClockTemplate SelectedTemplate
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public SettingsViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        if (applicationState.AvailableTemplateTypes?.Count > 0)
        {
            IEnumerable<TemplateItemModel> clockTemplates = applicationState.AvailableTemplateTypes
                .Select(x => new TemplateItemModel
                {
                    Name = x.Name
                        .Replace("ClockTemplate", "")
                        .Replace("Template", ""),
                    Type = x
                })
                .OrderBy(x => x.Name)
                .ToList();

            foreach (TemplateItemModel templateItemModel in clockTemplates)
                TemplateTypes.Add(templateItemModel);
        }

        SelectedTemplate = applicationState.ClockTemplate;

        if (applicationState.ClockTemplate != null)
        {
            Type selectedClockTemplateType = applicationState.ClockTemplate.GetType();
            SelectedTemplateType = TemplateTypes
                .FirstOrDefault(x => x.Type == selectedClockTemplateType);
        }
    }

    private void PublishTemplate(TemplateItemModel templateInfo)
    {
        ClockTemplate template = (ClockTemplate)Activator.CreateInstance(templateInfo.Type);
        applicationState.ClockTemplate = template;
        SelectedTemplate = template;
    }
}
