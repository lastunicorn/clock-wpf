using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.ClearClock;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool areControlsVisible = false;
    private readonly ObservableCollection<TemplateInfo> availableTemplates = [];

    public MainWindow()
    {
        InitializeComponent();

        PopulateTemplateComboBox();

        AnalogClock1.ApplyClockTemplate(new SunTemplate());

        LocalTimeProvider timeProvider = new();
        timeProvider.Start();

        AnalogClock1.TimeProvider = timeProvider;

        SetVersionInfo();
    }

    private void SetVersionInfo()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Version version = assembly.GetName().Version;
        VersionTextBlock.Text = $"Version {version.Major}.{version.Minor}.{version.Build}";
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

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
    }

    private void AnalogClock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        areControlsVisible = !areControlsVisible;

        CloseButton.Visibility = areControlsVisible
            ? Visibility.Visible
            : Visibility.Collapsed;

        ResizeGrip.Visibility = areControlsVisible
            ? Visibility.Visible
            : Visibility.Collapsed;

        SettingsButton.Visibility = areControlsVisible
            ? Visibility.Visible
            : Visibility.Collapsed;

        e.Handled = true;
    }

    private void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsPage.Visibility = Visibility.Visible;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ResizeGrip_DragDelta(object sender, DragDeltaEventArgs e)
    {
        double newWidth = AnalogClock1.Width + e.HorizontalChange;
        double newHeight = AnalogClock1.Height + e.VerticalChange;

        double minSize = 100;
        double maxSize = 1000;

        double size = Math.Min(newWidth, newHeight);

        if (size >= minSize && size <= maxSize)
        {
            AnalogClock1.Width = size;
            AnalogClock1.Height = size;
        }
    }

    private void TemplateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (templateComboBox.SelectedItem is TemplateInfo selectedTemplate)
        {
            ClockTemplate template = (ClockTemplate)Activator.CreateInstance(selectedTemplate.Type);
            AnalogClock1.ApplyClockTemplate(template);

            SettingsPage.Visibility = Visibility.Collapsed;
        }
    }

    private void CloseSettingsButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsPage.Visibility = Visibility.Collapsed;
    }
}