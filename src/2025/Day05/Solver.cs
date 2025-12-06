using FluentAssertions;

namespace _2025.Day05;

public class Solver()
    : Solver<(List<Range>, long[]), long>("Day05/input.txt")
{
    public override long PartOne((List<Range>, long[]) input)
    {
        var (ranges, ids) = input;
        return ids.Count(id => ranges.Any(range => range.Contains(id)));
    }

    public override long PartTwo((List<Range>, long[]) input)
    {
        var (ranges, _) = input;
        return ranges.Sum(range => range.NumberOfContainedIds);
    }

    public override (List<Range>, long[]) ParseInput(IEnumerable<string> input)
    {
        var ranges = input
            .TakeWhile(line => line.Contains('-'))
            .Select(line =>
            {
                var indexes = line.Split('-')
                    .Select(long.Parse)
                    .ToArray();

                return new Range(indexes[0], indexes[1]);
            })
            .ToList();

        var hasBeenReduced = true;
        while (hasBeenReduced)
        {
            var reduced = Reduced(ranges);

            hasBeenReduced = reduced.Count < ranges.Count;
            if (hasBeenReduced) ranges = reduced;
        }

        var ingredients = input
            .SkipWhile(line => line.Contains('-'))
            .Skip(1)
            .Select(long.Parse)
            .ToArray();

        return (ranges, ingredients);
    }

    public static List<Range> Reduced(List<Range> ranges)
    {
        var result = new List<Range>();

        var remaining = new List<Range>(ranges);

        while (remaining.Count > 0)
        {
            var current = remaining[0];
            remaining.RemoveAt(0);

            var overlaps = remaining
                .Where(current.OverlapsWith)
                .ToList();

            if (overlaps.Count > 0)
            {
                remaining.RemoveAll(current.OverlapsWith);
                result.AddRange(overlaps.Select(current.Merge));
            }
            else result.Add(current);
        }

        return result;
    }
}

public sealed record Range(long Start, long End)
{
    public long NumberOfContainedIds = (End - Start) + 1;

    public bool Contains(long number)
        => number >= Start && number <= End;

    public Range Merge(Range other)
        => OverlapsWith(other)
            ? new(
                Math.Min(Start, other.Start),
                Math.Max(End, other.End)
            )
            : throw new ArgumentException(
                $"Unable to merge non-overlapping range {this} with {other}");

    public bool OverlapsWith(Range other)
        => other.Contains(Start) || other.Contains(End)
        || Contains(other.Start) || Contains(other.End);
}

public class SolverTest : TestEngine<Solver, (List<Range>, long[]), long>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32",
        ])
        .ParsedAs((
            [new Range(3, 5), new Range(10, 20)],
            [1, 5, 8, 11, 17, 32]
        ))
        .ExpectsResult(3)
        .WithTheActualSolutionBeing(707);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput((
            [new Range(3, 5), new Range(10, 20)],
            [1, 5, 8, 11, 17, 32]
        ))
        .ExpectsResult(14)
        .WithTheActualSolutionBeing(361615643045059);
}
