namespace _2015.Day03;

public class SolverTest : TestEngine<Solver, string, int>
{
    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = "^v^v^v^v^v",
            Result = 2,
        },
        Solution = 2565,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = "^v^v^v^v^v",
            Result = 11,
        },
        Solution = 2639,
    };
}
