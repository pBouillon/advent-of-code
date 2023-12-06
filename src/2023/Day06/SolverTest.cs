namespace _2023.Day06;

public class SolverTest : TestEngine<Solver, Race[], long>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
          RawInput = [
              "Time:      7  15   30",
              "Distance:  9  40  200",
          ],
          Input = [
              new Race(Time: 7, BestDistance: 9),
              new Race(Time: 15, BestDistance: 40),
              new Race(Time: 30, BestDistance: 200),
          ],
          Result = 288,
        },
        Solution = 316_800,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = [
                new Race(Time: 7, BestDistance: 9),
                new Race(Time: 15, BestDistance: 40),
                new Race(Time: 30, BestDistance: 200),
            ],
            Result = 71_503,
        },
        Solution = 45_647_654,
    };
}

