using System.Collections.Generic;

namespace Rethunk.LC.RadarIdentQuickSwitch;

public static class IdentTable
{
    public static Dictionary<T, string> Assign<T>(IReadOnlyList<T> targets) where T : notnull
    {
        var idents = new Dictionary<T, string>(targets.Count);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] is not null)
                idents[targets[i]] = i.ToString();
        }
        return idents;
    }
}
