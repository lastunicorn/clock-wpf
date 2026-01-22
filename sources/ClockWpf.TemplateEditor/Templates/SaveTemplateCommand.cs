using System.Windows.Input;
using DustInTheWind.ClockWpf.Templates;
using Microsoft.Win32;

namespace DustInTheWind.ClockWpf.TemplateEditor.Templates;

public class SaveTemplateCommand : ICommand
{
    private readonly ApplicationState applicationState;

    public event EventHandler CanExecuteChanged;

    public SaveTemplateCommand(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }

    public bool CanExecute(object parameter)
    {
        return applicationState.CurrentTemplate != null;
    }

    public void Execute(object parameter)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
            DefaultExt = ".json",
            FileName = "template.json",
            Title = "Save Clock Template"
        };

        bool? result = saveFileDialog.ShowDialog();

        if (result == true)
        {
            TemplateSerializer templateSerializer = new();
            templateSerializer.SaveTemplate(applicationState.CurrentTemplate, saveFileDialog.FileName);
        }
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
