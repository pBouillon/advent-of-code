namespace _2022.Day04;

public class SolverTest : TestEngine<Solver, AssignementPair[], int>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = new[]
            {
                new AssignementPair("2-4,6-8"),
                new AssignementPair("2-3,4-5"),
                new AssignementPair("5-7,7-9"),
                new AssignementPair("2-8,3-7"),
                new AssignementPair("6-6,4-6"),
                new AssignementPair("2-6,4-8"),
            },
            Result = 2,
        },
        Solution = 466,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new ()
        {
            Input = new[]
            {
                new AssignementPair("2-4,6-8"),
                new AssignementPair("2-3,4-5"),
                new AssignementPair("5-7,7-9"),
                new AssignementPair("2-8,3-7"),
                new AssignementPair("6-6,4-6"),
                new AssignementPair("2-6,4-8"),
            },
            Result = 4,
        },
        Solution = 865,
    };
}
