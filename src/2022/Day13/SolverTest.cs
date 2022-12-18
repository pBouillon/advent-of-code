namespace _2022.Day13;

public class SolverTest : TestEngine<Solver, IEnumerable<Packet>, int>
{
    private readonly string[] _raw = new[]
    {
        "[1,1,3,1,1]",
        "[1,1,5,1,1]",
        "",
        "[[1],[2,3,4]]",
        "[[1],4]",
        "",
        "[9]",
        "[[8,7,6]]",
        "[[4,4],4,4]",
        "[[4,4],4,4,4]",
        "",
        "[7,7,7,7]",
        "[7,7,7]",
        "",
        "[]",
        "[3]",
        "",
        "[[[]]]",
        "[[]]",
        "",
        "[1,[2,[3,[4,[5,6,7]]]],8,9]",
        "[1,[2,[3,[4,[5,6,0]]]],8,9]",
    };

    private IEnumerable<Packet> Input => new Solver().ParseInput(_raw);

    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,

        Example = new()
        {
            Input = Input,
            Result = 13,
        },
        Solution = 0,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,

        Example = new()
        {
            Input = Input,
            Result = 0,
        },
        Solution = 0,
    };
}
