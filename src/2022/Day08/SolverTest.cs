namespace _2022.Day08;

public class SolverTest : TestEngine<Solver, int[,], int>
{
    private readonly int[,] _forest = new int[,]
    {
        { 3, 0, 3, 7, 3 },
        { 2, 5, 5, 1, 2 },
        { 6, 5, 3, 3, 2 },
        { 3, 3, 5, 4, 9 },
        { 3, 5, 3, 9, 0 },
    };

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = _forest,
            Result = 21,
        },
        Solution = 1851,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = _forest,
            Result = 8,
        },
        Solution = 574080,
    };
}
