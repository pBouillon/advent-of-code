namespace _2025.Day07;

public class Solver()
    : Solver<(int StartIndex, int[][] SplittersIndexes)>("Day07/input.txt")
{
    public override (int StartIndex, int[][] SplittersIndexes) ParseInput(IEnumerable<string> input)
    {
        var diagram = input.ToArray();

        var splittersIndexes = input
            .Select(line => line
                .Select((symbol, index) => (symbol, index))
                .Where(x => x.symbol == '^')
                .Select(x => x.index)
                .ToArray())
            .Where(indexes => indexes.Length > 0)
            .ToArray();

        return (
            StartIndex: diagram[0].IndexOf('S'),
            SplittersIndexes: splittersIndexes
        );
    }

    public override string PartOne((int StartIndex, int[][] SplittersIndexes) input)
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

    public override string PartTwo((int StartIndex, int[][] SplittersIndexes) input)
    {
        throw new NotImplementedException();
    }
}

public class SolverTest
    : TestEngine<Solver, (int StartIndex, int[][] SplittersIndexes)>
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
        .ExpectsResult("0")
        .WithTheActualSolutionBeing("0");
}
