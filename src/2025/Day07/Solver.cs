namespace _2025.Day07;

public class Solver()
    : Solver<(int StartIndex, HashSet<int>[] SplittersIndexes)>("Day07/input.txt")
{
    public override (int StartIndex, HashSet<int>[] SplittersIndexes) ParseInput(IEnumerable<string> input)
    {
        var diagram = input.ToArray();

        var splittersIndexes = input
            .Select(line => line
                .Select((symbol, index) => (symbol, index))
                .Where(x => x.symbol == '^')
                .Select(x => x.index)
                .ToHashSet())
            .Where(indexes => indexes.Count > 0)
            .ToArray();

        return (
            StartIndex: diagram[0].IndexOf('S'),
            SplittersIndexes: splittersIndexes
        );
    }

    public override string PartOne((int StartIndex, HashSet<int>[] SplittersIndexes) input)
    {
        var beamsIndexes = new HashSet<int> { input.StartIndex };

        var splitterHitCount = 0;

        foreach (var splitters in input.SplittersIndexes)
        {
            var splittersHit = splitters
                .Where(beamsIndexes.Contains)
                .ToArray();

            splitterHitCount += splittersHit.Length;

            foreach (var index in splittersHit)
            {
                beamsIndexes.Remove(index);

                beamsIndexes.Add(index - 1);
                beamsIndexes.Add(index + 1);
            }
        }

        return splitterHitCount.ToString();
    }

    public override string PartTwo((int StartIndex, HashSet<int>[] SplittersIndexes) input)
        => ComputeBeamPaths([input.StartIndex], input.SplittersIndexes)
            .Length
            .ToString();

    private static int[][] ComputeBeamPaths(int[] beamPath, HashSet<int>[] SplittersIndexes)
    {
        var beamIndex = beamPath[^1];
        var splitters = SplittersIndexes[0];

        var isSplit = splitters.Contains(beamIndex);

        int[] nextBeamIndexes = isSplit
            ? [beamIndex - 1, beamIndex + 1]
            : [beamIndex];

        var nextBeamPaths = nextBeamIndexes
            .Select(index => beamPath.Concat([index]).ToArray())
            .ToArray();

        var isStillWithinDiagram = SplittersIndexes.Length > 1;

        return isStillWithinDiagram
            ? [.. nextBeamPaths.SelectMany(path => ComputeBeamPaths(path, SplittersIndexes[1..]))]
            : nextBeamPaths;
    }
}

public class SolverTest
    : TestEngine<Solver, (int StartIndex, HashSet<int>[] SplittersIndexes)>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            ".......S.......",
            "...............",
            ".......^.......",
            "...............",
            "......^.^......",
            "...............",
            ".....^.^.^.....",
            "...............",
            "....^.^...^....",
            "...............",
            "...^.^...^.^...",
            "...............",
            "..^...^.....^..",
            "...............",
            ".^.^.^.^.^...^.",
            "...............",
        ])
        .ParsedAs((
            StartIndex: 7,
            SplittersIndexes: [
                [7],
                [6, 8],
                [5, 7, 9],
                [4, 6, 10],
                [3, 5, 9, 11],
                [2, 6, 12],
                [1, 3, 5, 7, 9, 13],
            ]
        ))
        .ExpectsResult("21")
        .WithTheActualSolutionBeing("1690");

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput((
            StartIndex: 7,
            SplittersIndexes: [
                [7],
                [6, 8],
                [5, 7, 9],
                [4, 6, 10],
                [3, 5, 9, 11],
                [2, 6, 12],
                [1, 3, 5, 7, 9, 13],
            ]
        ))
        .ExpectsResult("40")
        .WithTheActualSolutionBeing("0");
}
