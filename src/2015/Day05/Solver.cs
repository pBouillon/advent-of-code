namespace _2015.Day05;

public class Solver : Solver<IEnumerable<string>, int>
{
    protected override string InputPath => "Day05/input.txt";

    private static bool ContainsAtLeastThreeVowels(string @string)
        => @string.Count("aeiou".Contains) > 2;

    private static bool ContainsAnyConsecutiveLetter(string @string)
        => Enumerable
            .Range(0, @string.Length - 1)
            .Any(offset => @string[offset] == @string[offset + 1]);

    private static bool ContainsAnyForbiddenString(string @string)
        => new[] { "ab", "cd", "pq", "xy" }.Any(@string.Contains);

    private static bool ContainsAnyLetterSeparatedByAnother(string @string)
        => Enumerable
            .Range(0, @string.Length - 2)
            .Any(offset => @string[offset] == @string[offset + 2]);

    private static bool ContainsAnyRepeatingSequence(string @string)
        => Enumerable
            .Range(0, @string.Length - 2)
            .Any(offset => @string.IndexOf(
                @string.Substring(offset, 2),
                offset + 2,
                StringComparison.Ordinal) > -1);

    public override int PartOne(IEnumerable<string> input)
        => input.Count(@string =>
            ContainsAtLeastThreeVowels(@string)
            && ContainsAnyConsecutiveLetter(@string)
            && !ContainsAnyForbiddenString(@string));

    public override int PartTwo(IEnumerable<string> input)
         => input.Count(@string =>
            ContainsAnyLetterSeparatedByAnother(@string)
            && ContainsAnyRepeatingSequence(@string));

    public override IEnumerable<string> ReadInput(string inputPath)
        => File.ReadLines(inputPath);
}
