using AdventOfCode.Utils.Matrix;

namespace _2023.Day03;

public class SolverTest : TestEngine<Solver, EngineSchematic, long>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            //RawInput = [
            //    "467..114..",
            //    "...*......", // (3, 1)
            //    "..35..633.",
            //    "......#...", // (6, 3)
            //    "617*......", // (3, 4)
            //    ".....+.58.", // (5, 5)
            //    "..592.....",
            //    "......755.",
            //    "...$.*....", // (3, 8), (5, 8)
            //    ".664.598.."
            //],
            Input = new EngineSchematic([
                new Symbol(new(X: 3, Y: 1), '*'),
                new Symbol(new(X: 6, Y: 3), '#'),
                new Symbol(new(X: 3, Y: 4), '*'),
                new Symbol(new(X: 5, Y: 5), '+'),
                new Symbol(new(X: 3, Y: 8), '$'),
                new Symbol(new(X: 5, Y: 8), '*'),
            ])
            {
                Parts = [
                    new Part(new Coordinate(X: 0, Y: 0), Value: 467),
                    // new Part(new Coordinate(X: 5, Y: 0), Value: 114),
                    new Part(new Coordinate(X: 2, Y: 2), Value: 35),
                    new Part(new Coordinate(X: 6, Y: 2), Value: 633),
                    new Part(new Coordinate(X: 0, Y: 4), Value: 617),
                    // new Part(new Coordinate(X: 7, Y: 5), Value: 58),
                    new Part(new Coordinate(X: 2, Y: 6), Value: 592),
                    new Part(new Coordinate(X: 6, Y: 7), Value: 755),
                    new Part(new Coordinate(X: 1, Y: 9), Value: 664),
                    new Part(new Coordinate(X: 5, Y: 9), Value: 598),
                ],
            },
            Result = 4_361,
        },
        Solution = 556_057,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = new EngineSchematic([
                new Symbol(new(X: 3, Y: 1), '*'),
                new Symbol(new(X: 6, Y: 3), '#'),
                new Symbol(new(X: 3, Y: 4), '*'),
                new Symbol(new(X: 5, Y: 5), '+'),
                new Symbol(new(X: 3, Y: 8), '$'),
                new Symbol(new(X: 5, Y: 8), '*'),
            ])
            {
                Parts = [
                    new(new Coordinate(X: 0, Y: 0), Value: 467),
                    // new Part(new Coordinate(X: 5, Y: 0), Value: 114),
                    new Part(new Coordinate(X: 2, Y: 2), Value: 35),
                    new Part(new Coordinate(X: 6, Y: 2), Value: 633),
                    new Part(new Coordinate(X: 0, Y: 4), Value: 617),
                    // new Part(new Coordinate(X: 7, Y: 5), Value: 58),
                    new Part(new Coordinate(X: 2, Y: 6), Value: 592),
                    new Part(new Coordinate(X: 6, Y: 7), Value: 755),
                    new Part(new Coordinate(X: 1, Y: 9), Value: 664),
                    new Part(new Coordinate(X: 5, Y: 9), Value: 598),
                ],
            },
            Result = 467_835,
        },
        Solution = 82_824_352,
    };
}
