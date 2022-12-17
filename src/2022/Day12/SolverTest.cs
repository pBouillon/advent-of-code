namespace _2022.Day12;

public class SolverTest : TestEngine<Solver, ElevationMap, int>
{
    private readonly string[] _rawInput = new[]
    {
        "Sabqponm",
        "abcryxxl",
        "accszExk",
        "acctuvwj",
        "abdefghi"
    };

    private ElevationMap Map
        => new Solver().ParseInput(_rawInput);

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = Map,
            Result = 31,
        },
        Solution = 425,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = Map,
            Result = 29,
        },
        Solution = 418,
    };
}
