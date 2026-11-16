using Xunit;

namespace Rethunk.LC.RadarIdentQuickSwitch;

public class QuickSwitchParseTests
{
    [Fact]
    public void Disabled_does_not_consume_a_valid_index()
    {
        Assert.False(IdentTable.TryQuickSwitchIndex(enabled: false, sentence: "1", targetCount: 4, out _));
    }
}
