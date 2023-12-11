namespace _2023.Day09;

public class SolverTest : TestEngine<Solver, Report, long>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            RawInput = [
                "0 3 6 9 12 15",
                "1 3 6 10 15 21",
                "10 13 16 21 30 45",
            ],
            Input = new Report([
                new History([0, 3, 6, 9, 12, 15]),
                new History([1, 3, 6, 10, 15, 21]),
                new History([10, 13, 16, 21, 30, 45]),
            ]),
            Result = 114,
        },
        Solution = 1_898_776_583,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new Report([
                new History([0, 3, 6, 9, 12, 15]),
                new History([1, 3, 6, 10, 15, 21]),
                new History([10, 13, 16, 21, 30, 45]),
            ]),
            Result = 2,
        },
        Solution = 0,
    };
}
