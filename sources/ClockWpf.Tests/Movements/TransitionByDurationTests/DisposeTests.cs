using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionByDurationTests;

public class DisposeTests
{
    [Fact]
    public void WhenDisposingTwice_ThenNoExceptionIsThrown()
    {
        TransitionByDuration transition = new(_ => { });
        transition.Dispose();

        transition.Dispose();
    }
}
