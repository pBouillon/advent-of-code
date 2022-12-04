using AdventOfCode.Commons;

namespace _2022.Day01;

public class PuzzleTest : TestEngine<Solver, IEnumerable<long[]>, long>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new List<long[]>
            {
                new long[] { 1000, 2000, 3000 },
                new long[] { 4000 },
                new long[] { 5000, 6000 },
                new long[] { 7000, 8000, 9000 },
                new long[] { 10000 },
            },
            Result = 24000,
        },
        Solution = 72017,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new List<long[]>
            {
                new long[] { 1000, 2000, 3000 },
                new long[] { 4000 },
                new long[] { 5000, 6000 },
                new long[] { 7000, 8000, 9000 },
                new long[] { 10000 },
            },
            Result = 45000,
        },
        Solution = 212520,
    };
}
