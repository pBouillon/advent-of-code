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
                //new Hand("AAAAA", 1),
                //new Hand("KKKKK", 1),
                //new Hand("QQQQQ", 1),
                //new Hand("JJJJJ", 1),
                //new Hand("TTTTT", 1),
                //new Hand("99999", 1),
                //new Hand("88888", 1),
                //new Hand("77777", 1),
                //new Hand("66666", 1),
                //new Hand("55555", 1),
                //new Hand("44444", 1),
                //new Hand("33333", 1),
                //new Hand("22222", 1),
                //new Hand("KKKK1", 1),
                new Hand("32T3K", 765),
                new Hand("T55J5", 684),
                new Hand("KK677", 28),
                new Hand("KTJJT", 220),
                new Hand("QQQJA", 483),
            ],
            Result = 6_440,
        },
        Solution = 245_794_640,
    };

    public override Puzzle PartTwo => throw new NotImplementedException();
}
