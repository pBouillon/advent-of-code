using AdventOfCode.Utils.Parsing;
using AdventOfCode.Utils.Array;

namespace _2043.Day02;

public class Solver()
    : Solver<long[][], long>("Day02/input.txt")
{
    public override long[][] ParseInput(IEnumerable<string> input)
        => [.. input.Select(line => line.AsLongArray())];

    public override long PartOne(long[][] input)
        => input.Count(IsSafeReport);

    public override long PartTwo(long[][] input)
    => input.Count(report =>
    {
        if (IsSafeReport(report)) return true;

        for (int i = 0; i < report.Length; i++)
        {
            var dampened = report
                .Where((_, index) => index != i)
                .ToArray();

            if (IsSafeReport(dampened)) return true;
        }

        return false;
    });

    private bool IsSafeReport(long[] report)
    {
        var pairs = report.ToWindowOfSize(2);

        var sign = Math.Sign(pairs[0][0] - pairs[0][1]);

        return pairs.All(pair => IsSafe(pair[0] - pair[1], sign));
    }

    private static bool IsSafe(long delta, int sign)
    {
        var isWithinBounds = Math.Abs(delta) >= 1 && Math.Abs(delta) <= 3;
        var isSameSign = Math.Sign(delta) == sign;

        return isWithinBounds && isSameSign;
    }
}

public class SolverTest
    : TestEngine<Solver, long[][], long>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "7 6 4 2 1",
            "1 2 7 8 9",
            "9 7 6 2 1",
            "1 3 2 4 5",
            "8 6 4 4 1",
            "1 3 6 7 9",
        ])
        .ParsedAs([
            [7, 6 ,4, 2, 1],
            [1, 2, 7, 8, 9],
            [9, 7, 6, 2, 1],
            [1, 3, 2, 4, 5],
            [8, 6, 4, 4, 1],
            [1, 3, 6, 7, 9],
        ])
        .ExpectsResult(2)
        .WithTheActualSolutionBeing(236);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([
            [7, 6 ,4, 2, 1],
            [1, 2, 7, 8, 9],
            [9, 7, 6, 2, 1],
            [1, 3, 2, 4, 5],
            [8, 6, 4, 4, 1],
            [1, 3, 6, 7, 9],
        ])
        .ExpectsResult(4)
        .WithTheActualSolutionBeing(308);
}
