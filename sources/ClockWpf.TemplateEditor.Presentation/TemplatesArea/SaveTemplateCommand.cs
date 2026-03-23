using System.Windows.Input;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using Microsoft.Win32;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class SaveTemplateCommand : ICommand
{
    private readonly WorkContextPool clockTemplatePool;

    public event EventHandler CanExecuteChanged;

    public SaveTemplateCommand(WorkContextPool clockTemplatePool)
    {
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));

        clockTemplatePool.CurrentWorkContextChanged += HandleCurrentTemplateEditContextChanged;
    }

    private void HandleCurrentTemplateEditContextChanged(object sender, EventArgs e)
    {
        OnCanExecuteChanged();
    }

    public bool CanExecute(object parameter)
    {
        return clockTemplatePool.CurrentWorkContext?.ClockTemplate != null;
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
            templateSerializer.SaveTemplate(clockTemplatePool.CurrentWorkContext.ClockTemplate, saveFileDialog.FileName);
        }
    }

    private void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
