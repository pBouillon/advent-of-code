namespace _2021.Day07;

public class SolverTest : TestEngine<Solver, int[], int>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 },
            Result = 37,
        },
        Solution = 347509,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 },
            Result = 168,
        },
        Solution = 98257206,
    };
}
