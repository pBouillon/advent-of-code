namespace _2015.Day01;

public class SolverTest : TestEngine<Solver, string, int>
{
    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new Example
        {
            Input = ")())())",
            Result = -3,
        },
        Solution = 138,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new Example
        {
            Input = "()())",
            Result = 5,
        },
        Solution = 1771,
    };
}
