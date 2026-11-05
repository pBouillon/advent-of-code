namespace _2026.Day01;

public class Solver202601()
    : Solver<int[], int>(year: 2026, day: 01)
{
    public override int PartOne(int[] rotations)
        => throw new NotImplementedException();


    public override int PartTwo(int[] rotations)
        => throw new NotImplementedException();

    public override int[] ParseInput(IEnumerable<string> input)
        => throw new NotImplementedException();
}

public class SolverTest
    : TestEngine<Solver202601, int[], int>
{
    public override Puzzle PartOne
        => PuzzleBuilder
            .FromInput([])
            .ParsedAs([])
            .ExpectsResult(0)
            .WithTheActualSolutionBeing(0);

    public override Puzzle PartTwo
        => PuzzleBuilder
            .FromParsedInput([])
            .ExpectsResult(0)
            .WithTheActualSolutionBeing(0);
}
