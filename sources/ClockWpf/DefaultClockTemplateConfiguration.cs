using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates2;
using DustInTheWind.ClockWpf.Templates2.Shapes;

namespace DustInTheWind.ClockWpf;

public class DefaultClockTemplateConfiguration : ClockTemplateConfiguration
{
    public DefaultClockTemplateConfiguration()
    {
        Setup<FlatBackgroundT, FlatBackground>();
        Setup<FancyBackgroundT, FancyBackground>();
        Setup<SpecularReflectionT, SpecularReflection>();

        Setup<PinT, Pin>();
        Setup<TextShapeT, TextShape>();

        Setup<TicksT, Ticks>();
        Setup<TextRimT, TextRim>();
        Setup<HourNumeralsT, HourNumerals>();

        Setup<DotHandT, DotHand>();
        Setup<BarHandT, BarHand>();
        Setup<SimpleLineHandT, SimpleLineHand>();
        Setup<BladeHandT, BladeHand>();
        Setup<Blade2HandT, Blade2Hand>();
        Setup<NibHandT, NibHand>();
        Setup<DiamondHandT, DiamondHand>();
        Setup<FancySweepHandT, FancySweepHand>();
        Setup<PeepholeHandT, PeepholeHand>();
    }
}
