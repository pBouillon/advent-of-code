namespace _2021.Day04;

public class SolverTest : TestEngine<Solver, (IEnumerable<int>, BingoGrid[]), int>
{
    private readonly int[] _drawn =
    [
        07, 04, 09, 05, 11, 17, 23, 02, 00, 14,
        21, 24, 10, 16, 13, 06, 15, 25, 12, 22,
        18, 20, 08, 19, 03, 26, 01
    ];

    private readonly BingoGrid[] _grids =
    [
        new(
        [
            [22, 13, 17, 11,  0],
            [8,  2, 23,  4, 24],
            [21, 9, 14, 16, 7],
            [6, 10, 3, 18, 5],
            [1, 12, 20, 15, 19],
        ]),
        new(
        [
            [3, 15,  0,  2, 22],
            [9, 18, 13, 17,  5],
            [19,  8,  7, 25, 23],
            [20, 11, 10, 24,  4],
            [14, 21, 16, 12,  6],
        ]),

        new(
        [
            [14, 21, 17, 24,  4],
            [10, 16, 15,  9, 19],
            [18,  8, 23, 26, 20],
            [22, 11, 13,  6,  5],
            [2,  0, 12,  3,  7],
        ]),
    ];

    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = (_drawn, _grids),
            Result = 4512,
        },
        Solution = 63552,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = (_drawn, _grids),
            Result = 1924,
        },
        Solution = 9020,
    };
}
