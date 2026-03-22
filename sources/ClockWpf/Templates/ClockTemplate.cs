using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf.Templates2;

public abstract class ClockTemplate : Collection<ShapeT>
{
    private bool isNew = true;

    public bool IsNew
    {
        get => isNew;
        private set
        {
            if (isNew == value)
                return;

            isNew = value;
            OnModified();
        }
    }

    private void OnModified()
    {
        Modified?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler Modified;

    public ClockTemplate()
    {
        IEnumerable<ShapeT> shapes = CreateShapes();

        foreach (ShapeT shape in shapes)
        {
            Items.Add(shape);
            //shape.Changed += HandleShapeChanged;
        }
    }

    protected abstract IEnumerable<ShapeT> CreateShapes();

    protected override void InsertItem(int index, ShapeT item)
    {
        ArgumentNullException.ThrowIfNull(item);

        base.InsertItem(index, item);

        //item.Changed += HandleShapeChanged;
    }

    protected override void RemoveItem(int index)
    {
        if (index >= 0 && index < Items.Count - 1)
        {
            ShapeT item = Items[index];
            //item.Changed -= HandleShapeChanged;
        }

        base.RemoveItem(index);
    }

    protected override void SetItem(int index, ShapeT item)
    {
        if (index >= 0 && index < Items.Count - 1)
        {
            ShapeT oldItem = Items[index];
            //oldItem.Changed -= HandleShapeChanged;
        }

        base.SetItem(index, item);

        //item.Changed += HandleShapeChanged;
    }

    protected override void ClearItems()
    {
        //foreach (ShapeT item in Items)
        //    item.Changed -= HandleShapeChanged;

        base.ClearItems();
    }

    //private void HandleShapeChanged(object sender, EventArgs e)
    //{
    //    IsNew = false;
    //}
}