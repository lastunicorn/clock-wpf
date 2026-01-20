using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DustInTheWind.ClockWpf.Demo;

public class DependencyPropertyInfo : INotifyPropertyChanged
{
    private object value;

    public event PropertyChangedEventHandler PropertyChanged;

    public string Name { get; set; }

    public object Value
    {
        get => value;
        set
        {
            if (this.value == value)
                return;

            this.value = value;
            OnPropertyChanged();

            if (DependencyProperty != null && DependencyObject != null)
            {
                object convertedValue = ConvertValue(value);

                if (convertedValue != null)
                    DependencyObject.SetValue(DependencyProperty, convertedValue);
            }
        }
    }

    public string TypeName { get; set; }

    public DependencyProperty DependencyProperty { get; set; }

    public DependencyObject DependencyObject { get; set; }

    public bool IsDouble => TypeName == "Double";

    private object ConvertValue(object value)
    {
        if (value == null)
            return null;

        Type targetType = DependencyProperty.PropertyType;

        if (value.GetType() == targetType)
            return value;

        if (value is string stringValue)
        {
            if (targetType == typeof(double))
            {
                if (double.TryParse(stringValue, NumberStyles.Float, CultureInfo.InvariantCulture, out double doubleValue))
                    return doubleValue;
            }
            else if (targetType == typeof(int))
            {
                if (int.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int intValue))
                    return intValue;
            }
            else if (targetType == typeof(float))
            {
                if (float.TryParse(stringValue, NumberStyles.Float, CultureInfo.InvariantCulture, out float floatValue))
                    return floatValue;
            }
        }

        try
        {
            return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
        }
        catch
        {
            return null;
        }
    }

    public override string ToString()
    {
        string valueString = Value?.ToString() ?? "null";
        return $"{Name} ({TypeName}): {valueString}";
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
