namespace _2021.Day07;

public class SolverTest : TestEngine<Solver, int[], int>
{
    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = [16, 1, 2, 0, 4, 2, 7, 1, 2, 14],
            Result = 37,
        },
        Solution = 347509,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = [16, 1, 2, 0, 4, 2, 7, 1, 2, 14],
            Result = 168,
        },
        Solution = 98257206,
    };
}
