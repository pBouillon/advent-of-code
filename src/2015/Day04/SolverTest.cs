using AdventOfCode.Commons;

namespace _2015.Day04;

public class SolverTest : TestEngine<Solver, string, int>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = "abcdef",
            Result = 609043,
        },
        Solution = 254575,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = "abcdef",
            Result = 6742839,
        },
        Solution = 1038736,
    };
}
