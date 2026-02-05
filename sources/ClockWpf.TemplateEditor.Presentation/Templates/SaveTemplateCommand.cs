using System.Windows.Input;
using DustInTheWind.ClockWpf.Templates;
using Microsoft.Win32;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Templates;

public class SaveTemplateCommand : ICommand
{
    private readonly ClockTemplatePool clockTemplatePool;

    public event EventHandler CanExecuteChanged;

    public SaveTemplateCommand(ClockTemplatePool clockTemplatePool)
    {
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));
    }

    public bool CanExecute(object parameter)
    {
        return clockTemplatePool.CurrentTemplate != null;
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
            templateSerializer.SaveTemplate(clockTemplatePool.CurrentTemplate, saveFileDialog.FileName);
        }
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
