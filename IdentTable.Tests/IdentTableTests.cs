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

    [Fact]
    public void Assign_SecondCallOnFreshList_StartsAt0()
    {
        IdentTable.Assign(new List<string> { "p0", "p1", "p2" });
        var idents = IdentTable.Assign(new List<string> { "q0" });
        Assert.Equal("0", idents["q0"]);
        Assert.Single(idents);
    }
}
