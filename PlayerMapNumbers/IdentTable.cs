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

    public static bool IsValidIndex(int index, int count) =>
        index >= 0 && index < count;

    public static bool TryQuickSwitchIndex(bool enabled, string sentence, int targetCount, out int index)
    {
        index = 0;
        if (!enabled)
            return false;
        var tokens = sentence.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length != 1 || !int.TryParse(tokens[0], out index))
        {
            index = 0;
            return false;
        }
        return IsValidIndex(index, targetCount);
    }
}
