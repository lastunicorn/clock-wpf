using System.Reflection;
using System.Windows;

namespace DustInTheWind.ClockWpf.TemplateEditor;

public static class DependencyPropertyHelper
{
    public static IEnumerable<DependencyPropertyInfo> GetDependencyProperties(DependencyObject dependencyObject)
    {
        if (dependencyObject == null)
            yield break;

        Type type = dependencyObject.GetType();

        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

        IEnumerable<FieldInfo> dependencyPropertyFields = fields
            .Where(x => x.FieldType == typeof(DependencyProperty));

        foreach (FieldInfo fieldInfo in dependencyPropertyFields)
        {
            DependencyProperty dependencyProperty = (DependencyProperty)fieldInfo.GetValue(null);

            if (dependencyProperty != null)
            {
                object value = dependencyObject.GetValue(dependencyProperty);
                string propertyName = dependencyProperty.Name;
                string typeName = dependencyProperty.PropertyType.Name;

                yield return new DependencyPropertyInfo
                {
                    Name = propertyName,
                    Value = value,
                    TypeName = typeName,
                    DependencyProperty = dependencyProperty,
                    DependencyObject = dependencyObject
                };
            }
        }
    }
}
