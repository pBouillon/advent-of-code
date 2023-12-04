namespace _2023.Day04;

public class SolverTest : TestEngine<Solver, ScratchCard[], long>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            RawInput = [
                "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
                "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
                "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
                "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
                "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
                "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11",
            ],
            Input = [
                new ScratchCard
                {
                    Id = 1,
                    ScratchedNumbers = [83, 86, 6, 31, 17, 9, 48, 53],
                    WinningNumbers = [41, 48, 83, 86, 17],
                },
                new ScratchCard
                {
                    Id = 2,
                    ScratchedNumbers = [61, 30, 68, 82, 17, 32, 24, 19],
                    WinningNumbers = [13, 32, 20, 16, 61],
                },
                new ScratchCard
                {
                    Id = 3,
                    ScratchedNumbers = [69, 82, 63, 72, 16, 21, 14, 1],
                    WinningNumbers = [1, 21, 53, 59, 44],
                },
                new ScratchCard
                {
                    Id = 4,
                    ScratchedNumbers = [59, 84, 76, 51, 58, 5, 54, 83],
                    WinningNumbers = [41, 92, 73, 84, 69],
                },
                new ScratchCard
                {
                    Id = 5,
                    ScratchedNumbers = [88, 30, 70, 12, 93, 22, 82, 36],
                    WinningNumbers = [87, 83, 26, 28, 32],
                },
                new ScratchCard
                {
                    Id = 6,
                    ScratchedNumbers = [74, 77, 10, 23, 35, 67, 36, 11],
                    WinningNumbers = [31, 18, 13, 56, 72],
                },
            ],
            Result = 13,
        },
        Solution = 21_558,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new()
        {
            Input = [
                new ScratchCard
                {
                    Id = 1,
                    ScratchedNumbers = [83, 86, 6, 31, 17, 9, 48, 53],
                    WinningNumbers = [41, 48, 83, 86, 17],
                },
                new ScratchCard
                {
                    Id = 2,
                    ScratchedNumbers = [61, 30, 68, 82, 17, 32, 24, 19],
                    WinningNumbers = [13, 32, 20, 16, 61],
                },
                new ScratchCard
                {
                    Id = 3,
                    ScratchedNumbers = [69, 82, 63, 72, 16, 21, 14, 1],
                    WinningNumbers = [1, 21, 53, 59, 44],
                },
                new ScratchCard
                {
                    Id = 4,
                    ScratchedNumbers = [59, 84, 76, 51, 58, 5, 54, 83],
                    WinningNumbers = [41, 92, 73, 84, 69],
                },
                new ScratchCard
                {
                    Id = 5,
                    ScratchedNumbers = [88, 30, 70, 12, 93, 22, 82, 36],
                    WinningNumbers = [87, 83, 26, 28, 32],
                },
                new ScratchCard
                {
                    Id = 6,
                    ScratchedNumbers = [74, 77, 10, 23, 35, 67, 36, 11],
                    WinningNumbers = [31, 18, 13, 56, 72],
                },
            ],
            Result = 30,
        },
        Solution = 10_425_665,
    };
}
