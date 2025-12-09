namespace _2025.Day02;

public class Solver()
    : Solver<Range[], long>("Day02/input.txt")
{
    public override long PartOne(Range[] ranges)
        => ranges
            .SelectMany(range => range.ContainedIds())
            .Where(id => id.IsMirrored)
            .Sum(id => id.Value);

    public override long PartTwo(Range[] ranges)
        => ranges
            .SelectMany(range => range.ContainedIds())
            .Where(id => id.ContainsOnlyRepeatingNumbers)
            .Sum(id => id.Value);

    public override Range[] ParseInput(IEnumerable<string> input)
        => [.. input
            .First()
            .Split(',')
            .Select(range =>
            {
                var ids = range.Split('-').ToArray();
                return new Range(ids[0], ids[1]);
            })];
}

public sealed record Id(string Number)
{
    public bool ContainsOnlyRepeatingNumbers
        = Enumerable
            .Range(0, Number.Length / 2 + 1)
            .Any(i => Number.Split(
                    Number[0..i],
                    StringSplitOptions.RemoveEmptyEntries
                ).Length == 0);

    public bool IsMirrored = Number[..(Number.Length / 2)] == Number[(Number.Length / 2)..];

    public long Value = long.Parse(Number);
}

public sealed record Range
{
    public Id Start { get; init; }
    public Id End { get; init; }

    public Range(string start, string end)
        => (Start, End) = (new Id(start), new Id(end));

    public IEnumerable<Id> ContainedIds()
    {
        var range = End.Value - Start.Value + 1;

        for (var i = 0; i < range; ++i)
        {
            var id = new Id($"{i + Start.Value}");
            yield return id;
        }
    }
}

public class SolverTest : TestEngine<Solver, Range[], long>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput(["11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"])
        .ParsedAs([
            new Range("11", "22"),
            new Range("95", "115"),
            new Range("998", "1012"),
            new Range("1188511880", "1188511890"),
            new Range("222220", "222224"),
            new Range("1698522", "1698528"),
            new Range("446443", "446449"),
            new Range("38593856", "38593862"),
            new Range("565653", "565659"),
            new Range("824824821", "824824827"),
            new Range("2121212118", "2121212124"),
        ])
        .ExpectsResult(1_227_775_554)
        .WithTheActualSolutionBeing(23_039_913_998);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([
            new Range("11", "22"),
            new Range("95", "115"),
            new Range("998", "1012"),
            new Range("1188511880", "1188511890"),
            new Range("222220", "222224"),
            new Range("1698522", "1698528"),
            new Range("446443", "446449"),
            new Range("38593856", "38593862"),
            new Range("565653", "565659"),
            new Range("824824821", "824824827"),
            new Range("2121212118", "2121212124"),
        ])
        .ExpectsResult(4_174_379_265)
        .WithTheActualSolutionBeing(35_950_619_148);
}
