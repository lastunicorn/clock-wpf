using System.Collections.ObjectModel;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Utils;

internal static class ObservableCollectionExtensions
{
    public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
    {
        foreach (T item in items)
            collection.Add(item);
    }
}