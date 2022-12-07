namespace _2022.Day06;

public class SolverTest : TestEngine<Solver, string, int>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
            Result = 7,
        },
        Solution = 1275,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
            Result = 19,
        },
        Solution = 3605,
    };
}
