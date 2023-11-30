namespace _2015.Day06;

public class SolverTest : TestEngine<Solver, Command[], long>
{
    private readonly Command[] _commands =
    [
        new(Status.Toggle, new Coordinate(0, 0), new Coordinate(999, 999)),
    ];

    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = _commands,
            Result = 1000000,
        },
        Solution = 569999,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = _commands,
            Result = 2000000,
        },
        Solution = 17836115,
    };
}
