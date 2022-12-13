using System.Linq;
using System.Text.RegularExpressions;

namespace _2022.Day11;

public record Item(ulong WorryLevel);

public record Throw(Item Item, int To);

public class Monkey
{
    public ulong InspectedItems { get; private set; } = 0;

    private readonly Queue<Item> _items = new();

    private readonly Func<Item, ulong> _inspect;

    private readonly Func<Item, int> _throw;

    public Monkey(IEnumerable<Item> items, Func<Item, ulong> inspect, Func<Item, int> @throw)
    {
        _items = new Queue<Item>(items);

        _throw = @throw;
        _inspect = inspect;
    }

    public void Receive(Item item)
        => _items.Enqueue(item);

    public Queue<Throw> PlayTurn(bool canBeBored = true)
    {
        var thrown = new Queue<Throw>();

        while (_items.Count > 0)
        {
            var item = _items.Dequeue();

            item = Inspect(item);

            if (canBeBored)
            {
                item = GetBoredOf(item);
            }

            var @throw = new Throw(item, _throw(item));

            thrown.Enqueue(@throw);
        }

        return thrown;
    }

    private Item Inspect(Item item)
    {
        ++InspectedItems;

        return item with
        {
            WorryLevel = _inspect(item)
        };
    }

    private static Item GetBoredOf(Item item)
        => item with
        {
            WorryLevel = (ulong)Math.Floor(item.WorryLevel / 3d),
        };

    public override string ToString()
        => $"Inspected: {InspectedItems} - Items: "
            + string.Join(", ", _items.Select(i => i.WorryLevel));
}

public class Monkeys
{
    private readonly Dictionary<int, Monkey> _monkeys = new();

    public ulong MonkeyBusiness
        => _monkeys.Values
            .Select(monkey => monkey.InspectedItems)
            .OrderByDescending(inspectedItems => inspectedItems)
            .Take(2)
            .Aggregate(
                1uL,
                (count, inspectedItems) => count * inspectedItems);

    public void Add(int id, Monkey monkey)
        => _monkeys.Add(id, monkey);

    public void PlayRounds(int count, bool canBeBored = true)
        => Enumerable.Range(0, count)
            .ToList()
            .ForEach(_ => PlayRound(canBeBored));

    public void PlayRound(bool canBeBored = true)
    {
        foreach (var (_, monkey) in _monkeys)
        {
            var thrown = monkey.PlayTurn(canBeBored);

            foreach (var (item, to) in thrown)
            {
                _monkeys[to].Receive(item);
            }
        }
    }
}

public class Solver : Solver<Monkeys, ulong>
{
    public Solver() : base("Day11/input.txt") { }

    public override ulong PartOne(Monkeys monkeys)
    {
        monkeys.PlayRounds(count: 20);
        return monkeys.MonkeyBusiness;
    }

    public override ulong PartTwo(Monkeys monkeys)
    {
        monkeys.PlayRounds(count: 10_000, canBeBored: false);
        return monkeys.MonkeyBusiness;
    }

    public override Monkeys ParseInput(IEnumerable<string> input)
    {
        const int monkeyDefinitionLenght = 6;

        var monkeyDefinitions = input
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select((x, i) => new
            {
                Key = i / monkeyDefinitionLenght,
                Value = x,
            })
            .GroupBy(
                x => x.Key,
                x => x.Value,
                (_, group) => group.ToArray())
            .ToArray();

        static IEnumerable<long> GetNumbersIn(string line)
            => Regex.Matches(line, @"\d+")
                .OfType<Match>()
                .Select(m => long.Parse(m.Value));

        // Compute the divider
        static int MonkeyDivider(string[] definition)
            => (int)GetNumbersIn(definition[3]).First();

        var divider = monkeyDefinitions.Select(MonkeyDivider)
            .Aggregate(
                1uL, 
                (divider, monkeyDivider) => divider * (ulong)monkeyDivider);

        // Create the monkeys from the definition and the divider
        static (int, Monkey) CreateMonkeyFrom(string[] definition, ulong divider)
        {
            // Items
            var startingItems = GetNumbersIn(definition[1])
                .Select(worryLevel => new Item((ulong)worryLevel));

            // Throw
            var divisibleBy = (int)GetNumbersIn(definition[3]).First();
            var ifTrue = (int)GetNumbersIn(definition[4]).First();
            var ifFalse = (int)GetNumbersIn(definition[5]).First();

            Func<Item, int> @throw = (item) => item.WorryLevel % (ulong)divisibleBy == 0
                ? ifTrue
                : ifFalse;

            // Inspection
            var operation = definition[2].Split(" = ")[1];
            var parsed = operation.Split(" ");

            Func<Item, ulong> inspect = (item) =>
            {
                var left = parsed[0] == "old"
                    ? item.WorryLevel
                    : ulong.Parse(parsed[0]);

                var right = parsed[2] == "old"
                    ? item.WorryLevel
                    : ulong.Parse(parsed[2]);

                return parsed[1] switch
                {
                    "+" => left + right,
                    "-" => left - right,
                    "*" => left * right,
                    "/" => left / right,
                    _ => throw new ArgumentException(),
                } % divider;
            };

            // Result
            var id = (int)GetNumbersIn(definition[0]).First();
            var monkey = new Monkey(startingItems, inspect, @throw);

            return (id, monkey);
        }

        var createdMonkeys = monkeyDefinitions.Select(definition => CreateMonkeyFrom(definition, divider));

        var monkeys = new Monkeys();

        foreach (var (id, monkey) in createdMonkeys)
        {
            monkeys.Add(id, monkey);
        }

        return monkeys;
    }
}
