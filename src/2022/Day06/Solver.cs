using System.Diagnostics;

namespace _2022.Day06;

public record Marker(int Index, string Value);

public class Solver : Solver<string, int>
{
    public Solver() : base("Day06/input.txt") { }

    private static int FindMarkerIn(string haystack, int markerLength)
    {
        for (var i = 0; i < haystack.Length - markerLength; ++i)
        {
            var marker = haystack[i..(i + markerLength)];
            var differentChars = marker.ToHashSet().Count;

            var isMarker = differentChars == markerLength;

            if (isMarker)
            {
                return i + markerLength;
            }
        }

        return -1;
    }

    public override int PartOne(string input)
        => FindMarkerIn(input, markerLength: 4);

    public override int PartTwo(string input)
        => FindMarkerIn(input, markerLength: 14);

    public override string ParseInput(IEnumerable<string> input)
        => input.First();
}
