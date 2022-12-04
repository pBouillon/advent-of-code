namespace _2021.Day04;

public class SolverTest : TestEngine<Solver, (IEnumerable<int>, BingoGrid[]), int>
{
    private readonly int[] _drawn =
    {
        07, 04, 09, 05, 11, 17, 23, 02, 00, 14,
        21, 24, 10, 16, 13, 06, 15, 25, 12, 22,
        18, 20, 08, 19, 03, 26, 01
    };

    private readonly BingoGrid[] _grids =
    {
        new(new[]
        {
            new[] { 22, 13, 17, 11,  0 },
            new[] {  8,  2, 23,  4, 24 },
            new[] { 21, 9, 14, 16, 7 },
            new[] { 6, 10, 3, 18, 5 },
            new[] { 1, 12, 20, 15, 19 },
        }),
        new(new[]
        {
            new[] {  3, 15,  0,  2, 22 },
            new[] {  9, 18, 13, 17,  5 },
            new[] { 19,  8,  7, 25, 23 },
            new[] { 20, 11, 10, 24,  4 },
            new[] { 14, 21, 16, 12,  6 },
        }),

        new(new[]
        {
            new[] { 14, 21, 17, 24,  4 },
            new[] { 10, 16, 15,  9, 19 },
            new[] { 18,  8, 23, 26, 20 },
            new[] { 22, 11, 13,  6,  5 },
            new[] {  2,  0, 12,  3,  7 },
        }),
    };

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = (_drawn, _grids),
            Result = 4512,
        },
        Solution = 63552,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = (_drawn, _grids),
            Result = 1924,
        },
        Solution = 9020,
    };
}
