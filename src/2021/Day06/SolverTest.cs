namespace _2021.Day06;

public class SolverTest : TestEngine<Solver, Dictionary<int, long>, long>
{
    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new()
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 2 },
                { 4, 1 }
            },
            Result = 5934,
        },
        Solution = 352872,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new()
            {
                { 1, 1 },
                { 2, 1 },
                { 3, 2 },
                { 4, 1 }
            },
            Result = 26984457539,
        },
        Solution = 1604361182149,
    };
}
