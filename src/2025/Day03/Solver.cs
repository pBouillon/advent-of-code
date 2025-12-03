namespace _2025.Day03;

public class Solver()
    : Solver<int[][], long>("Day03/input.txt")
{
    public static long LongestPossibleNumberIn(int[] bank, int finalNumberLength)
    {
        var result = 0L;
        var singleDigitsSkipped = 0;

        for (var currentPowerOfTen = finalNumberLength; currentPowerOfTen > 0; --currentPowerOfTen)
        {
            var numbersOfPowerOfTenTooBig = finalNumberLength - currentPowerOfTen;
            var reserved = currentPowerOfTen - 1;

            var window = bank[(numbersOfPowerOfTenTooBig + singleDigitsSkipped)..^reserved];

            var (value, offset) = window
                .Select((battery, index) => (Value: battery, Offset: index))
                .MaxBy(battery => battery.Value);

            result += (long)(value * Math.Pow(10, currentPowerOfTen - 1));
            singleDigitsSkipped += offset;
        }

        return result;
    }

    public override long PartOne(int[][] strings)
        => strings
            .Select(bank => LongestPossibleNumberIn(bank, finalNumberLength: 2))
            .Sum();

    public override long PartTwo(int[][] strings)
        => strings
            .Select(bank => LongestPossibleNumberIn(bank, finalNumberLength: 12))
            .Sum();

    public override int[][] ParseInput(IEnumerable<string> input)
        => [.. input.Select(bank => bank.Select(battery => battery - '0').ToArray())];
}

public class SolverTest : TestEngine<Solver, int[][], long>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "987654321111111",
            "811111111111119",
            "234234234234278",
            "818181911112111",
        ])
        .ParsedAs([
            [9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1],
            [8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9],
            [2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8],
            [8, 1, 8, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1],
        ])
        .ExpectsResult(357)
        .WithTheActualSolutionBeing(17_613);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([
            [9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1],
            [8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9],
            [2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8],
            [8, 1, 8, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1],
        ])
        .ExpectsResult(3_121_910_778_619)
        .WithTheActualSolutionBeing(175_304_218_462_560);
}
