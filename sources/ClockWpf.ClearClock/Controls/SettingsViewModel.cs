using System.Collections.ObjectModel;
using System.Reflection;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.ClearClock.Controls;

public class SettingsViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

    public ObservableCollection<TemplateItemModel> Templates { get; } = [];

    public TemplateItemModel SelectedTemplate
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

    public SettingsViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        PopulateTemplateCollection();
    }

    private void PopulateTemplateCollection()
    {
        IEnumerable<TemplateItemModel> clockTemplates = EnumerateClockTemplates()
            .OrderBy(x => x.Name)
            .ToList();

        foreach (TemplateItemModel template in clockTemplates)
            Templates.Add(template);

        if (Templates.Count > 0)
        {
            SelectedTemplate = Templates
                .FirstOrDefault(x => x.Type == typeof(PlayfulTemplate));
        }
    }

    private static IEnumerable<TemplateItemModel> EnumerateClockTemplates()
    {
        Assembly clockWpfAssembly = typeof(ClockTemplate).Assembly;

        Type[] types = clockWpfAssembly.GetTypes();

        foreach (Type type in types)
        {
            if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(ClockTemplate)))
            {
                yield return new TemplateItemModel
                {
                    Name = type.Name
                        .Replace("ClockTemplate", "")
                        .Replace("Template", ""),
                    Type = type
                };
            }
        }
    }

    private void PublishTemplate(TemplateItemModel templateInfo)
    {
        ClockTemplate template = (ClockTemplate)Activator.CreateInstance(templateInfo.Type);
        applicationState.ClockTemplate = template;
    }
}
