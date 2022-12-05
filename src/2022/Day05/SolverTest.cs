namespace _2022.Day05;

public class SolverTest : TestEngine<Solver, CraneOrdering, string>
{
#pragma warning disable IDE0051 // Remove unused private members
    private const string rawInput = @"
            [D]    
        [N] [C]    
        [Z] [M] [P]
         1   2   3 

        move 1 from 2 to 1
        move 3 from 1 to 3
        move 2 from 2 to 1
        move 1 from 1 to 2"
#pragma warning restore IDE0051 // Remove unused private members
;

    private readonly CratesStack _stacks = new(new Dictionary<int, string>()
    {
        { 1, "ZN" },
        { 2, "MCD" },
        { 3, "P" },
    });

    private readonly CraneOperation[] _operations =
    {
        new(1, 2, 1),
        new(3, 1, 3),
        new(2, 2, 1),
        new(1, 1, 2),
    };

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new CraneOrdering(_stacks, _operations),
            Result = "CMZ",
        },
        Solution = "FRDSQRRCD",
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new CraneOrdering(_stacks, _operations),
            Result = "MCD",
        },
        Solution = "HRFTQVWNN",
    };
}
