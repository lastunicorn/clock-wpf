using System.Collections.ObjectModel;
using System.Windows;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

/// <summary>
/// Interaction logic for CabinetWindow.xaml
/// </summary>
public partial class CabinetWindow : Window
{
    public ObservableCollection<ClockTemplate> ClockTemplates { get; private set; } = [];

    public CabinetWindow()
    {
        InitializeComponent();

        DataContext = new CabinetViewModel();

        //foreach (Type clockTemplateType in clockTemplateTypes)
        //{
        //    AnalogClock analogClock = new()
        //    {
        //        ClockTemplate = (ClockTemplate)Activator.CreateInstance(clockTemplateType),
        //        Movement = movement,
        //        Width = 100,
        //        Height = 100,
        //        Margin = new Thickness(10)
        //    };

        //    Container.Children.Add(analogClock);
        //}
    }
}
