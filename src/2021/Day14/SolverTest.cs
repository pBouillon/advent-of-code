namespace _2021.Day14;

public class SolverTest : TestEngine<Solver, (string, Dictionary<string, string>), long>
{
    private readonly Dictionary<string, string> _insertions = new Dictionary<string, string>
    {
        { "CH", "B" },
        { "HH", "N" },
        { "CB", "H" },
        { "NH", "C" },
        { "HB", "C" },
        { "HC", "B" },
        { "HN", "C" },
        { "NN", "C" },
        { "BH", "H" },
        { "NC", "B" },
        { "NB", "B" },
        { "BN", "B" },
        { "BB", "N" },
        { "BC", "B" },
        { "CC", "N" },
        { "CN", "C" },
    };

    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = ("NNCB", _insertions),
            Result = 1588,
        },
        Solution = 3048,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = ("NNCB", _insertions),
            Result = 2188189693529,
        },
        Solution = 3288891573057,
    };
}
