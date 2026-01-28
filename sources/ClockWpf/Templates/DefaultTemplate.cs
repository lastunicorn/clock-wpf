using System.Windows.Media;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.Templates;

public class DefaultTemplate : ClockTemplate
{
    private FlatBackground background;
    private Ticks minuteTicks;
    private Ticks hourTicks;
    private HourNumerals hourNumerals;
    private CapsuleHand hourHand;
    private CapsuleHand minuteHand;
    private SimpleHand secondHand;

    private TemplateStyle style = TemplateStyle.White;

    public TemplateStyle Style
    {
        get => style;
        set
        {
            if (style != value)
            {
                style = value;
                UpdateColors();
            }
        }
    }

    private void UpdateColors()
    {
        switch (Style)
        {
            case TemplateStyle.White:
                background.FillBrush = Brushes.WhiteSmoke;
                minuteTicks.StrokeBrush = new SolidColorBrush(Color.FromRgb(0xa0, 0xa0, 0xa0));
                hourTicks.StrokeBrush = Brushes.Black;
                hourNumerals.FillBrush = Brushes.Black;
                hourHand.FillBrush = Brushes.Black;
                minuteHand.FillBrush = Brushes.Black;
                secondHand.StrokeBrush = Brushes.Red;
                break;

            case TemplateStyle.Black:
                background.FillBrush = Brushes.Black;
                minuteTicks.StrokeBrush = new SolidColorBrush(Color.FromRgb(0x60, 0x60, 0x60));
                hourTicks.StrokeBrush = Brushes.WhiteSmoke;
                hourNumerals.FillBrush = Brushes.WhiteSmoke;
                hourHand.FillBrush = Brushes.WhiteSmoke;
                minuteHand.FillBrush = Brushes.WhiteSmoke;
                secondHand.StrokeBrush = Brushes.OrangeRed;
                break;
        }

        if (minuteTicks.StrokeBrush.CanFreeze == true)
            minuteTicks.StrokeBrush.Freeze();
    }

    protected override IEnumerable<Shape> CreateShapes()
    {
        background = new()
        {
            Name = "Background",
            FillBrush = Brushes.WhiteSmoke
        };
        yield return background;

        minuteTicks = new()
        {
            Name = "Minute Ticks",
            SkipIndex = 5,
            StrokeBrush = new SolidColorBrush(Color.FromRgb(0xa0, 0xa0, 0xa0))
        };

        if (minuteTicks.StrokeBrush.CanFreeze)
            minuteTicks.StrokeBrush.Freeze();

        yield return minuteTicks;

        hourTicks = new()
        {
            Name = "Hour Ticks",
            Angle = 30,
            OffsetAngle = 30,
            StrokeThickness = 1.5
        };
        yield return hourTicks;

        hourNumerals = new()
        {
            Name = "Hour Numerals",
            FillBrush = Brushes.Black,
            DistanceFromEdge = 26
        };
        yield return hourNumerals;

        hourHand = new()
        {
            Name = "Hour Hand",
            TimeComponent = TimeComponent.Hour,
            Length = 48,
            Width = 8,
            TailLength = 4,
            StrokeThickness = 0,
            FillBrush = Brushes.Black
        };
        yield return hourHand;

        minuteHand = new()
        {
            Name = "Minute Hand",
            TimeComponent = TimeComponent.Minute,
            Length = 85,
            Width = 8,
            TailLength = 4,
            StrokeThickness = 0,
            FillBrush = Brushes.Black
        };
        yield return minuteHand;

        secondHand = new()
        {
            Name = "Second Hand",
            TimeComponent = TimeComponent.Second,
            Length = 96,
            TailLength = 14,
            StrokeBrush = Brushes.Red,
            StrokeThickness = 1,
            IntegralValue = true,
            PinDiameter = 8
        };
        yield return secondHand;
    }

    public enum TemplateStyle
    {
        White,
        Black
    }

    //public IEnumerable<TemplateStyleClass> CreateStyles()
    //{
    //    yield return new WhiteStyle("White", this);
    //}

    //private class WhiteStyle : TemplateStyleClass
    //{
    //    private readonly DefaultTemplate template;

    //    public WhiteStyle(DefaultTemplate template)
    //    {
    //        this.template = template ?? throw new ArgumentNullException(nameof(template));
    //    }

    //    public override void Apply()
    //    {
    //        template.background.FillBrush = Brushes.WhiteSmoke;
    //        template.minuteTicks.StrokeBrush = new SolidColorBrush(Color.FromRgb(0xa0, 0xa0, 0xa0));
    //        template.hourTicks.StrokeBrush = Brushes.Black;
    //        template.hourNumerals.FillBrush = Brushes.Black;
    //        template.hourHand.FillBrush = Brushes.Black;
    //        template.minuteHand.FillBrush = Brushes.Black;
    //        template.secondHand.StrokeBrush = Brushes.Red;
    //    }
    //}
}

[TemplateStyle("White")]
public abstract class TemplateStyleClass
{
    public abstract void Apply();
}

internal class TemplateStyleAttribute : Attribute
{
    public string Name { get; }

    public TemplateStyleAttribute(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}