namespace _2015.Day02;

public class SolverTest : TestEngine<Solver, IEnumerable<Dimension>, int>
{
    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new[] { new Dimension(2, 3, 4) },
            Result = 58,
        },
        Solution = 1586300,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new[] { new Dimension(1, 1, 10) },
            Result = 14,
        },
        Solution = 3737498,
    };
}
