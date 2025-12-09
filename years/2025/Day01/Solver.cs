namespace _2025.Day01;

public class Solver()
    : Solver<int[], int>("Day01/input.txt")
{
    private static int[] TicksFor(int dial, int rotation)
        => [.. Enumerable
            .Range(1, Math.Abs(rotation))
            .Select(tick => (dial + (tick * Math.Sign(rotation)) + 100) % 100)];

    public override int PartOne(int[] rotations)
        => rotations
            .Aggregate(
                seed: (Dial: 50, Hits: 0),
                (dial, rotation) =>
                {
                    var next = TicksFor(dial.Dial, rotation)[^1];
                    return (next, dial.Hits + (next == 0 ? 1 : 0));
                })
            .Hits;

    public override int PartTwo(int[] rotations)
        => rotations
            .Aggregate(
                seed: (Dial: 50, Hits: 0),
                (dial, rotation) =>
                {
                    var ticks = TicksFor(dial.Dial, rotation);
                    return (ticks[^1], dial.Hits + ticks.Count(0));
                })
            .Hits;

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
