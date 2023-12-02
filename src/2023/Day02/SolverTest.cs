namespace _2023.Day02;

public class SolverTest : TestEngine<Solver, Game[], long>
{
    public override Puzzle PartOne => new()
    {
        Example = new Example
        {
            RawInput = [
               "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
            ],
            Input = [
                new Game(1, [
                    new SetOfCube(RedCount: 4, GreenCount: 0, BlueCount: 3),
                    new SetOfCube(RedCount: 1, GreenCount: 2, BlueCount: 6),
                    new SetOfCube(RedCount: 0, GreenCount: 2, BlueCount: 0),
                ]),
                new Game(2, [
                    new SetOfCube(RedCount: 0, GreenCount: 2, BlueCount: 1),
                    new SetOfCube(RedCount: 1, GreenCount: 3, BlueCount: 4),
                    new SetOfCube(RedCount: 0, GreenCount: 1, BlueCount: 1),
                ]),
                new Game(3, [
                    new SetOfCube(RedCount: 20, GreenCount: 8, BlueCount: 6),
                    new SetOfCube(RedCount: 4, GreenCount: 13, BlueCount: 5),
                    new SetOfCube(RedCount: 1, GreenCount: 5, BlueCount: 0),
                ]),
                new Game(4, [
                    new SetOfCube(RedCount: 3, GreenCount: 1, BlueCount: 6),
                    new SetOfCube(RedCount: 6, GreenCount: 3, BlueCount: 0),
                    new SetOfCube(RedCount: 14, GreenCount: 3, BlueCount: 15),
                ]),
                new Game(5, [
                    new SetOfCube(RedCount: 6, GreenCount: 3, BlueCount: 1),
                    new SetOfCube(RedCount: 1, GreenCount: 2, BlueCount: 2),
                ]),
            ],
            Result = 8,
        },
        Solution = 2_795,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new Example
        {
            Input = [
                new Game(1, [
                    new SetOfCube(RedCount: 4, GreenCount: 0, BlueCount: 3),
                    new SetOfCube(RedCount: 1, GreenCount: 2, BlueCount: 6),
                    new SetOfCube(RedCount: 0, GreenCount: 2, BlueCount: 0),
                ]),
                new Game(2, [
                    new SetOfCube(RedCount: 0, GreenCount: 2, BlueCount: 1),
                    new SetOfCube(RedCount: 1, GreenCount: 3, BlueCount: 4),
                    new SetOfCube(RedCount: 0, GreenCount: 1, BlueCount: 1),
                ]),
                new Game(3, [
                    new SetOfCube(RedCount: 20, GreenCount: 8, BlueCount: 6),
                    new SetOfCube(RedCount: 4, GreenCount: 13, BlueCount: 5),
                    new SetOfCube(RedCount: 1, GreenCount: 5, BlueCount: 0),
                ]),
                new Game(4, [
                    new SetOfCube(RedCount: 3, GreenCount: 1, BlueCount: 6),
                    new SetOfCube(RedCount: 6, GreenCount: 3, BlueCount: 0),
                    new SetOfCube(RedCount: 14, GreenCount: 3, BlueCount: 15),
                ]),
                new Game(5, [
                    new SetOfCube(RedCount: 6, GreenCount: 3, BlueCount: 1),
                    new SetOfCube(RedCount: 1, GreenCount: 2, BlueCount: 2),
                ]),
            ],
            Result = 2_286,
        },
        Solution = 75_561,
    };
}
