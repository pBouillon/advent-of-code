namespace _2021.Day15;

public class SolverTest : TestEngine<Solver, int[][], long>
{
    private readonly int[][] _map =
    {
        new[] { 1, 1, 6, 3, 7, 5, 1, 7, 4, 2 },
        new[] { 1, 3, 8, 1, 3, 7, 3, 6, 7, 2 },
        new[] { 2, 1, 3, 6, 5, 1, 1, 3, 2, 8 },
        new[] { 3, 6, 9, 4, 9, 3, 1, 5, 6, 9 },
        new[] { 7, 4, 6, 3, 4, 1, 7, 1, 1, 1 },
        new[] { 1, 3, 1, 9, 1, 2, 8, 1, 3, 7 },
        new[] { 1, 3, 5, 9, 9, 1, 2, 4, 2, 1 },
        new[] { 3, 1, 2, 5, 4, 2, 1, 6, 3, 9 },
        new[] { 1, 2, 9, 3, 1, 3, 8, 5, 2, 1 },
        new[] { 2, 3, 1, 1, 9, 4, 4, 5, 8, 1 },
    };

    private readonly int[] _shortestPath = { 1, 1, 2, 1, 3, 6, 5, 1, 1, 1, 5, 1, 1, 3, 2, 3, 2, 1, 1 };

    public override Puzzle PartOne => new()
    { 
        Example = new()
        {
            Input = _map,
            Result = _shortestPath.Skip(1).Sum(),
        },
        Solution = 373,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = _map,
            Result = 315,
        },
        Solution = 2868,
    };
}
