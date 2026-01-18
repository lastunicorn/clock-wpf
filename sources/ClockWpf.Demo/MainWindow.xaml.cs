using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.Demo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    private readonly ObservableCollection<TemplateInfo> availableTemplates = [];
    private Shape selectedShape;

    public event PropertyChangedEventHandler PropertyChanged;

    public Shape SelectedShape
    {
        get => selectedShape;
        set
        {
            if (selectedShape != value)
            {
                selectedShape = value;
                OnPropertyChanged();
            }
        }
    }

    public MainWindow()
    {
        InitializeComponent();

        DataContext = this;

        PopulateTemplateComboBox();

        LocalTimeProvider localTimeProvider = new();
        localTimeProvider.Start();

        analogClock1.TimeProvider = localTimeProvider;
    }

    private void PopulateTemplateComboBox()
    {
        IEnumerable<TemplateInfo> clockTemplates = EnumerateClockTemplates()
            .OrderBy(x => x.Name)
            .ToList();

        foreach (TemplateInfo template in clockTemplates)
            availableTemplates.Add(template);

        templateComboBox.ItemsSource = availableTemplates;

        if (availableTemplates.Count > 0)
        {
            TemplateInfo initialTemplateInfo = availableTemplates
                .FirstOrDefault(x => x.Type == typeof(PlayfulTemplate));

            int capsuleIndex = availableTemplates.IndexOf(initialTemplateInfo);

            templateComboBox.SelectedIndex = capsuleIndex >= 0
                ? capsuleIndex
                : 0;
        }
    }

    private static IEnumerable<TemplateInfo> EnumerateClockTemplates()
    {
        Assembly clockWpfAssembly = typeof(ClockTemplate).Assembly;

        Type[] types = clockWpfAssembly.GetTypes();

        foreach (Type type in types)
        {
            if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(ClockTemplate)))
            {
                yield return new TemplateInfo
                {
                    Name = type.Name
                        .Replace("ClockTemplate", "")
                        .Replace("Template", ""),
                    Type = type
                };
            }
        }
    }

    private void TemplateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (templateComboBox.SelectedItem is TemplateInfo selectedTemplate)
        {
            ClockTemplate template = (ClockTemplate)Activator.CreateInstance(selectedTemplate.Type);
            analogClock1.ApplyClockTemplate(template);
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}