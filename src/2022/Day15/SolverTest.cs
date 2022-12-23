namespace _2022.Day15;

public class SolverTest : TestEngine<Solver, Cave, int>
{
    private readonly IEnumerable<string> _rawInput = new[]
    {
        "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
        "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
        "Sensor at x=13, y=2: closest beacon is at x=15, y=3",
        "Sensor at x=12, y=14: closest beacon is at x=10, y=16",
        "Sensor at x=10, y=20: closest beacon is at x=10, y=16",
        "Sensor at x=14, y=17: closest beacon is at x=10, y=16",
        "Sensor at x=8, y=7: closest beacon is at x=2, y=10",
        "Sensor at x=2, y=0: closest beacon is at x=2, y=10",
        "Sensor at x=0, y=11: closest beacon is at x=2, y=10",
        "Sensor at x=20, y=14: closest beacon is at x=25, y=17",
        "Sensor at x=17, y=20: closest beacon is at x=21, y=22",
        "Sensor at x=16, y=7: closest beacon is at x=15, y=3",
        "Sensor at x=14, y=3: closest beacon is at x=15, y=3",
        "Sensor at x=20, y=1: closest beacon is at x=15, y=3",
    };

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new Solver().ParseInput(_rawInput),
            // The solver will run for y = 2_000_000 which makes 0 with the example
            Result = 0,
        },
        Solution = 4793062,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,

        Example = new()
        {
            Input = new Solver().ParseInput(_rawInput),
            Result = 56000011,
        },
        Solution = 0,
    };
}
