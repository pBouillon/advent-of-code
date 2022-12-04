namespace _2021.Day11;

public class SolverTest : TestEngine<Solver, Dictionary<Coordinate, int>, int>
{
    private static Dictionary<Coordinate, int> GetOctopuses()
    {
        var raw = new[]
        {
            "5483143223",
            "2745854711",
            "5264556173",
            "6141336146",
            "6357385478",
            "4167524645",
            "2176841721",
            "6882881134",
            "4846848554",
            "5283751526"
        };

        return new Dictionary<Coordinate, int>(
            from y in Enumerable.Range(0, 10)
            from x in Enumerable.Range(0, 10)
            select new KeyValuePair<Coordinate, int>(new Coordinate(x, y), raw[y][x] - '0')
        );
    }

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = GetOctopuses(),
            Result = 1656,
        },
        Solution = 1613
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = GetOctopuses(),
            Result = 195,
        },
        Solution = 510
    };
}
