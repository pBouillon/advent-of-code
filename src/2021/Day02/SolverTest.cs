namespace _2021.Day02;

public class SolverTest : TestEngine<Solver, IEnumerable<Command>, int>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new[]
            {
                new Command("forward", 5),
                new Command("down", 5),
                new Command("forward", 8),
                new Command("up", 3),
                new Command("down", 8),
                new Command("forward", 2),
            },
            Result = 150,
        },
        Solution = 1728414,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new[]
            {
                new Command("forward", 5),
                new Command("down", 5),
                new Command("forward", 8),
                new Command("up", 3),
                new Command("down", 8),
                new Command("forward", 2),
            },
            Result = 900,
        },
        Solution = 1765720035,
    };
}
