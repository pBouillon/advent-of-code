namespace _2023.Day01;

public class SolverTest : TestEngine<Solver, string[], long>
{
    public override Puzzle PartOne => new()
    {
        Example = new Example
        {
            Input = [
                "1abc2",
                "pqr3stu8vwx",
                "a1b2c3d4e5f",
                "treb7uchet",
            ],
            Result = 142,
        },
        Solution = 55_816,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new Example
        {
            Input = [
                "eightwothree",
                "two1nine",
                "abcone2threexyz",
                "xtwone3four",
                "4nineeightseven2",
                "zoneight234",
                "7pqrstsixteen",
            ],
            Result = 281,
        },
        Solution = 54_980,
    };
}
