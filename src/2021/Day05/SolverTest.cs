namespace _2021.Day05;

public class SolverTest : TestEngine<Solver, IEnumerable<Range>, int>
{
    private readonly Range[] _ranges =
    [
        new(new Coordinate(0, 9), new Coordinate(5, 9)),
        new(new Coordinate(8, 0), new Coordinate(0, 8)),
        new(new Coordinate(9, 4), new Coordinate(3, 4)),
        new(new Coordinate(2, 2), new Coordinate(2, 1)),
        new(new Coordinate(7, 0), new Coordinate(7, 4)),
        new(new Coordinate(6, 4), new Coordinate(2, 0)),
        new(new Coordinate(0, 9), new Coordinate(2, 9)),
        new(new Coordinate(3, 4), new Coordinate(1, 4)),
        new(new Coordinate(0, 0), new Coordinate(8, 8)),
        new(new Coordinate(5, 5), new Coordinate(8, 2)),
    ];

    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = _ranges,
            Result = 5,
        },
        Solution = 6311,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = _ranges,
            Result = 12,
        },
        Solution = 19929,
    };
}
