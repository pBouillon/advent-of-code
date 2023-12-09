namespace _2023.Day07;

public class SolverTest : TestEngine<Solver, Hand[], long>
{
    public override Puzzle PartOne => new()
    {
        Example = new Example
        {
            RawInput = [
                "32T3K 765",
                "T55J5 684",
                "KK677 28",
                "KTJJT 220",
                "QQQJA 483",
            ],
            Input = [
                new Hand("32T3K", 765),
                new Hand("T55J5", 684),
                new Hand("KK677", 28),
                new Hand("KTJJT", 220),
                new Hand("QQQJA", 483),
            ],
            Result = 6_440,
        },
        Solution = 248_422_077,
    };

    public override Puzzle PartTwo => new()
    {
        Example = new Example
        {
            Input = [
                new Hand("32T3K", 765),
                new Hand("T55J5", 684),
                new Hand("KK677", 28),
                new Hand("KTJJT", 220),
                new Hand("QQQJA", 483),
            ],
            Result = 5_905,
        },
        Solution = 0,
    };
}
