using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

public class CurrentMovementChangedEventArgs : EventArgs
{
    public IMovement OldMovement { get; }

    public IMovement NewMovement { get; }

    public CurrentMovementChangedEventArgs(IMovement oldMovement, IMovement newMovement)
    {
        OldMovement = oldMovement;
        NewMovement = newMovement;
    }
}
