namespace _2015.Day03;

public class SolverTest : TestEngine<Solver, string, int>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = "^v^v^v^v^v",
            Result = 2,
        },
        Solution = 2565,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = "^v^v^v^v^v",
            Result = 11,
        },
        Solution = 2639,
    };
}
