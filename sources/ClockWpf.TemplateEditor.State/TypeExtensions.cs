namespace DustInTheWind.ClockWpf.TemplateEditor.State;

public static class TypeExtensions
{
    public static ClockTemplateMetadata AsClockTemplateMetadata(this Type type)
    {
        return new ClockTemplateMetadata(type);
    }
}
