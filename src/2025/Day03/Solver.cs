namespace _2025.Day03;

public class Solver()
    : Solver<string[], long>("Day03/input.txt")
{
    public override long PartOne(string[] strings)
        => strings
            .Select(bank => bank
                .Select((battery, index) => (Value: battery - '0', Offset: index))
                .ToArray())
            .Sum(batteries =>
            {
                var first = batteries[..^1].MaxBy(x => x.Value);

                var second = batteries[1..]
                    .Skip(first.Offset)
                    .MaxBy(x => x.Value);

                return int.Parse($"{first.Value}{second.Value}");
            });

    public override long PartTwo(string[] strings)
        => throw new NotImplementedException();

    public override string[] ParseInput(IEnumerable<string> input)
        => [.. input];
}

public class SolverTest : TestEngine<Solver, string[], long>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "987654321111111",
            "811111111111119",
            "234234234234278",
            "818181911112111",
        ])
        .ParsedAs([
            "987654321111111",
            "811111111111119",
            "234234234234278",
            "818181911112111",
        ])
        .ExpectsResult(357)
        .WithTheActualSolutionBeing(17613);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([
            "987654321111111",
            "811111111111119",
            "234234234234278",
            "818181911112111",
        ])
        .ExpectsResult(3121910778619)
        .WithTheActualSolutionBeing(0);
}
