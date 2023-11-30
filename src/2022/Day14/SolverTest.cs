namespace _2022.Day14;

public class SolverTest : TestEngine<Solver, Cave, int>
{
    private readonly IEnumerable<string> _rawInput = new[]
    {
        "498,4 -> 498,6 -> 496,6",
        "503,4 -> 502,4 -> 502,9 -> 494,9",
    };

    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new Solver().ParseInput(_rawInput),
            Result = 24,
        },
        Solution = 901,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new Solver().ParseInput(_rawInput),
            Result = 93,
        },
        Solution = 24589,
    };
}
