namespace _2022.Day02;

public class PuzzleTest : TestEngine<Solver, IEnumerable<Round>, int>
{
    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new[]
            {
                new Round("A", "Y"),
                new Round("B", "X"),
                new Round("C", "Z"),
            },
            Result = 15,
        },
        Solution = 11666,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new[]
            {
                new Round("A", "Y"),
                new Round("B", "X"),
                new Round("C", "Z"),
            },
            Result = 12,
        },
        Solution = 12767,
    };
}
