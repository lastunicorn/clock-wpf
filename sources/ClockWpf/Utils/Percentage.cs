namespace DustInTheWind.ClockWpf.Utils;

/// <summary>
/// Represents a percentage value constrained between 0 and 100.
/// </summary>
/// <remarks>
/// The Percentage type is useful for expressing and manipulating values as percentages in a
/// type-safe manner. It supports implicit conversions to and from float, and can be used directly
/// in arithmetic expressions to apply percentage calculations.
/// The value is always clamped to the range 0 to 100.
/// </remarks>
public record struct Percentage
{
    public float Value { get; }

    public Percentage(float value)
    {
        Value = Math.Clamp(value, 0, 100);
    }

    public static float operator *(float number, Percentage percentage)
    {
        return number * (percentage.Value / 100f);
    }

    public static implicit operator float(Percentage percentage)
    {
        return percentage.Value;
    }

    public static implicit operator Percentage(float value)
    {
        return new Percentage(value);
    }
}