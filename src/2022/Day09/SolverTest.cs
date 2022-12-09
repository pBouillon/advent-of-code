namespace _2022.Day09;

public class SolverTest : TestEngine<Solver, IEnumerable<Motion>, int>
{
    private readonly Motion[] _motions = new Motion[]
    {
        new(Direction.Right, 4),
        new(Direction.Up, 4),
        new(Direction.Left, 3),
        new(Direction.Down, 1),
        new(Direction.Right, 4),
        new(Direction.Down, 1),
        new(Direction.Left, 5),
        new(Direction.Right, 2),
    };

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = _motions,
            Result = 13,
        },
        Solution = 5930,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = _motions,
            Result = 1,
        },
        Solution = 2443,
    };
}
