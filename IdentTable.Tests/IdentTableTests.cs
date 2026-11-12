using Rethunk.LC.RadarIdentQuickSwitch;
using Xunit;

namespace RadarIdentQuickSwitch.Tests;

public class IdentTableTests
{
    [Fact]
    public void Assign_AfterRemovingIndex4_FormerIndex5MapsTo4()
    {
        var targets = new List<string> { "0", "1", "2", "3", "X", "Y" };
        targets.RemoveAt(4);
        var idents = IdentTable.Assign(targets);
        Assert.Equal("4", idents["Y"]);
    }
}
