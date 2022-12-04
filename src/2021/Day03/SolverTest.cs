namespace _2021.Day03;

public class SolverTest : TestEngine<Solver, string[], int>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new[]
            {
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010",
            },
            Result = 198,
        },
        Solution = 2003336,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new[]
            {
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010",
            },
            Result = 230,
        },
        Solution = 1877139,
    };
}
