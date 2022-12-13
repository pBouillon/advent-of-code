namespace _2022.Day11;

public class SolverTest : TestEngine<Solver, Monkeys, ulong>
{
    private readonly Monkey[] _monkeys = new[]
    {
        new Monkey(
            items: new Item[] { new(79), new(98) },
            inspect: (item) => item.WorryLevel * 19,
            @throw: (item) => item.WorryLevel % 23 == 0 ? 2 : 3),

        new Monkey(
            items: new Item[] { new(54), new(65), new(75), new(74) },
            inspect: (item) => item.WorryLevel + 6,
            @throw: (item) => item.WorryLevel % 19 == 0 ? 2 : 0),

        new Monkey(
            items: new Item[] { new(79), new(60), new(97) },
            inspect: (item) => item.WorryLevel * item.WorryLevel,
            @throw: (item) => item.WorryLevel % 13 == 0 ? 1 : 3),

        new Monkey(
            items: new Item[] { new(74) },
            inspect: (item) => item.WorryLevel + 3,
            @throw: (item) => item.WorryLevel % 17 == 0 ? 0 : 1),
    };

    private Monkeys GetMonkeys()
    {
        var monkeys = new Monkeys();

        for (var i = 0; i < _monkeys.Length; ++i)
        {
            monkeys.Add(i, _monkeys[i]);
        }

        return monkeys;
    }

    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            Input = GetMonkeys(),
            Result = 10605uL,
        },
        Solution = 62491uL,
    };

    public override Puzzle PartTwo => new()
    {
        // WiP
        ShouldSkipTests = true,

        Example = new()
        {
            Input = GetMonkeys(),
            Result = 2713310158uL,
        },
        Solution = 0,
    };
}
