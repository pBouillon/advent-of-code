namespace _2021.Day09;

public class SolverTest : TestEngine<Solver, int[][], long>
{
    private readonly int[][] _area =
    {
        new[] { 2, 1, 9, 9, 9, 4, 3, 2, 1, 0 },
        new[] { 3, 9, 8, 7, 8, 9, 4, 9, 2, 1 },
        new[] { 9, 8, 5, 6, 7, 8, 9, 8, 9, 2 },
        new[] { 8, 7, 6, 7, 8, 9, 6, 7, 8, 9 },
        new[] { 9, 8, 9, 9, 9, 6, 5, 6, 7, 8 },
    };

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = _area,
            Result = 15,
        },
        Solution = 591,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = _area,
            Result = 1134,
        },
        Solution = 1113424,
    };
}
