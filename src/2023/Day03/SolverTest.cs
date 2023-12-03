namespace _2023.Day03;

public class SolverTest : TestEngine<Solver, EngineSchematic, long>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            RawInput = [
                "467..114..",
                "...*......", // (1, 3)
                "..35..633.",
                "......#...", // (3, 6)
                "617*......", // (4, 3)
                ".....+.58.", // (5, 5)
                "..592.....",
                "......755.",
                "...$.*....", // (8, 3), (8, 5)
                ".664.598.."
                ],
            Input = new EngineSchematic([
                new(X: 1, Y: 3),
                new(X: 4, Y: 3),
                new(X: 8, Y: 3),
                new(X: 5, Y: 5),
                new(X: 8, Y: 5),
                new(X: 3, Y: 6),
            ])
            {
                PartNumbers = [467, 35, 633, 617, 592, 755, 664, 598],
            },
            Result = 4_361,
        },
        Solution = 556_057,
    };

    public override Puzzle PartTwo => throw new NotImplementedException();
}
