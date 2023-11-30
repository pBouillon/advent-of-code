namespace _2021.Day01;

public class SolverTest : TestEngine<Solver, int[], int>
{
    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = [199, 200, 208, 210, 200, 207, 240, 269, 260, 263],
            Result = 7,
        },
        Solution = 1696,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = [199, 200, 208, 210, 200, 207, 240, 269, 260, 263],
            Result = 5,
        },
        Solution = 1737,
    };
}
