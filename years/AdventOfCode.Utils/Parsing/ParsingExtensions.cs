using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Utils.Parsing;

/// <summary>
/// Helpers to extract values from the lines of a puzzle input.
/// </summary>
public static partial class ParsingExtensions
{
    /// <summary>
    /// Matches an integer. A sign only counts when it does not follow a digit.
    /// </summary>
    [GeneratedRegex(@"(?<!\d)[+-]?\d+")]
    private static partial Regex NumberRegex();

    /// <summary>
    /// Extracts every integer in the line, ignoring anything else.
    /// A sign only counts when it does not follow a digit: "2-4" gives 2 and 4.
    /// </summary>
    public static T[] ExtractNumbers<T>(this string line)
        where T : INumber<T>
        => [.. NumberRegex()
            .Matches(line)
            .Select(match =>
                T.Parse(match.Value, CultureInfo.InvariantCulture))];

    /// <summary>
    /// Extracts every integer in the line as a long, see
    /// <see cref="ExtractNumbers{T}(string)"/>.
    /// </summary>
    public static long[] AsLongArray(this string line)
        => line.ExtractNumbers<long>();
}
