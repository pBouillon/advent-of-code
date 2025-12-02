namespace _2025.Day01;

public class Solver()
    : Solver<int[], int>("Day01/input.txt")
{
    private static int[] TicksFor(int dial, int rotation) 
        => [.. Enumerable
            .Range(1, Math.Abs(rotation))
            .Select(tick => (dial + (tick * Math.Sign(rotation)) + 100) % 100)];

    public override int PartOne(int[] rotations)
    {
        var clickZeroHits = 0;

        var dial = 50;
        foreach (var rotation in rotations)
        {
            dial = TicksFor(dial, rotation)[^1];
            if (dial == 0) ++clickZeroHits;
        }

        return clickZeroHits;
    }

    public override int PartTwo(int[] rotations)
    {
        var pointedAtZeroOccurrences = 0;

        var dial = 50;
        foreach (var rotation in rotations)
        {
            var ticks = TicksFor(dial, rotation);
            
            pointedAtZeroOccurrences += ticks.Count(tick => tick == 0);

            dial = TicksFor(dial, rotation)[^1];
        }

        return pointedAtZeroOccurrences;
    }

    public override int[] ParseInput(IEnumerable<string> input)
        => [.. input.Select(rotation =>
        {
            var clicks = int.Parse(rotation[1..]);
            return rotation.StartsWith('L')
                ? -1 * clicks
                : clicks;
        })];
}

public class SolverTest : TestEngine<Solver, int[], int>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput(["L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82"])
        .ParsedAs([-68, -30, 48, -5, 60, -55, -1, -99, 14, -82])
        .ExpectsResult(3)
        .WithTheActualSolutionBeing(1_097);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([-68, -30, 48, -5, 60, -55, -1, -99, 14, -82])
        .ExpectsResult(6)
        .WithTheActualSolutionBeing(7_101);
}
