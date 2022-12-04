namespace _2021.Day12;

public class SolverTest : TestEngine<Solver, Dictionary<string, List<string>>, int>
{
    private readonly Dictionary<string, List<string>> _map = new()
    {
        ["start"] = new List<string> { "A", "b" },
        ["A"] = new List<string> { "start", "b", "c", "end" },
        ["b"] = new List<string> { "start", "A", "d", "end" },
        ["c"] = new List<string> { "A" },
        ["d"] = new List<string> { "b" },
        ["end"] = new List<string> { "A", "b" },
    };

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = _map,
            Result = 10,
        },
        Solution = 4104,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = _map,
            Result = 36,
        },
        Solution = 119760,
    };
}
