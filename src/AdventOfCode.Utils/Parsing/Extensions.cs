using System.Text.RegularExpressions;

namespace AdventOfCode.Utils.Parsing;

public static partial class Extensions
{
    [GeneratedRegex(@"[+-]?\d+")]
    private static partial Regex NumberRegex();

    public static long[] AsLongArray(this string line)
        => NumberRegex().Matches(line)
                .Cast<Match>()
                .Select(match => long.Parse(match.Value))
                .ToArray();
}
