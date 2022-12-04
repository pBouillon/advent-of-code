using AdventOfCode.Commons;

namespace _2015.Day05;

public class SolverTest : TestEngine<Solver, IEnumerable<string>, int>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new[]
            {
                "ugknbfddgicrmopn",  // Nice
                "aaa",               // Nice
                "jchzalrnumimnmhp",  // Not nice
                "haegwjzuvuyypxyu",  // Not nice
                "dvszwmarrgswjxmb"   // Not nice
            },
            Result = 2,
        },
        Solution = 236,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new[]
            {
            "qjhvhtzxzqqjkmpb",  // Nice
            "xxyxx",             // Nice
            "uurcxstgmygtbstg",  // Not nice
            "ieodomkazucvgmuy"   // Not nice
            },
            Result = 2,
        },
        Solution = 51,
    };
}
