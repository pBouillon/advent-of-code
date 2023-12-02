namespace _2023.Day02;

public class SolverTest : TestEngine<Solver, Game[], long>
{
    public override Puzzle PartOne => new()
    {
        Example = new Example
        {
            Input = [
                new Game(1, [
                    new Hand(RedCount: 4, GreenCount: 0, BlueCount: 3),
                    new Hand(RedCount: 1, GreenCount: 2, BlueCount: 6),
                    new Hand(RedCount: 0, GreenCount: 2, BlueCount: 0),
                ]),
                new Game(2, [
                    new Hand(RedCount: 0, GreenCount: 2, BlueCount: 1),
                    new Hand(RedCount: 1, GreenCount: 3, BlueCount: 4),
                    new Hand(RedCount: 0, GreenCount: 1, BlueCount: 1),
                ]),
                new Game(3, [
                    new Hand(RedCount: 20, GreenCount: 8, BlueCount: 6),
                    new Hand(RedCount: 4, GreenCount: 13, BlueCount: 5),
                    new Hand(RedCount: 1, GreenCount: 5, BlueCount: 0),
                ]),
                new Game(4, [
                    new Hand(RedCount: 3, GreenCount: 1, BlueCount: 6),
                    new Hand(RedCount: 6, GreenCount: 3, BlueCount: 0),
                    new Hand(RedCount: 14, GreenCount: 3, BlueCount: 15),
                ]),
                new Game(5, [
                    new Hand(RedCount: 6, GreenCount: 3, BlueCount: 1),
                    new Hand(RedCount: 1, GreenCount: 2, BlueCount: 2),
                ]),
            ],
            Result = 8,
        },
        Solution = 2795,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new Example
        {
            Input = Array.Empty<Game>(),
            Result = 0,
        },
        Solution = 0,
    };
}
