using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.Tests.Movements.TransitionBySpeedTests;

public class DisposeTests
{
    [Fact]
    public void WhenDisposingTwice_ThenNoExceptionIsThrown()
    {
        TransitionBySpeed transition = new(_ => { });
        transition.Dispose();

        transition.Dispose();
    }
}
