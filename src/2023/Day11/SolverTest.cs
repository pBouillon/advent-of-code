using AdventOfCode.Utils.Matrix;

namespace _2023.Day11;

public class SolverTest : TestEngine<Solver, Image, long>
{
    private readonly string[] _rawImage = [
        "...#......",
        ".......#..",
        "#.........",
        "..........",
        "......#...",
        ".#........",
        ".........#",
        "..........",
        ".......#..",
        "#...#.....",
    ];

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new Image(_rawImage.ParseMatrix()),
            Result = 374,
        },
        Solution = 9_918_828,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new Image(_rawImage.ParseMatrix()),
            Result = 0,
        },
        Solution = 0,
    };
}
