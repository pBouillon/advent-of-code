using System.Diagnostics;
using System.Text.RegularExpressions;

namespace _2022.Day05;

public partial record CraneOperation(int Count, int From, int To)
{
    [GeneratedRegex("move (\\d+) from (\\d+) to (\\d+)", RegexOptions.Compiled)]
    private static partial Regex OperationExtractionRegexp();

    public static readonly Regex ParsingRegexp = OperationExtractionRegexp();
}

public class CratesStack
{
    private readonly IDictionary<int, Stack<char>> _stacks = new Dictionary<int, Stack<char>>();

    public CratesStack(IDictionary<int, Stack<char>> stacks)
        => _stacks = stacks;

    public void Apply(CraneOperation operation, string crateModel)
    {
        var (count, from, to) = operation;

        void CrateMover9000Move()
        {
            for (var i = 0; i < count; ++i)
            {
                var moved = _stacks[from].Pop();
                _stacks[to].Push(moved);
            }
        }

        void CrateMover9001Move()
        {
            var crates = new Stack<char>();

            for (var i = 0; i < count; ++i)
            {
                var moved = _stacks[from].Pop();
                crates.Push(moved);
            }

            while (crates.Count > 0)
            {
                var moved = crates.Pop();
                _stacks[to].Push(moved);
            }
        }

        Action move = crateModel switch
        {
            "CrateMover 9000" => CrateMover9000Move,
            "CrateMover 9001" => CrateMover9001Move,
            _ => throw new UnreachableException()
        };

        move();
    }

    public IEnumerable<char> GetTopCrateOnEachStack()
        => _stacks.Select(kvp => kvp.Value.Peek());
}

public class CraneOrdering
{
    private readonly CratesStack _crates;

    private readonly IEnumerable<CraneOperation> _operations;

    public CraneOrdering(CratesStack crates, IEnumerable<CraneOperation> operations)
        => (_crates, _operations) = (crates, operations);

    public string GetTopCratesAfterOperationsUsing(string crateModel = "CrateMover 9000")
    {
        foreach (var operation in _operations)
        {
            _crates.Apply(operation, crateModel);
        }

        return string.Join(string.Empty, _crates.GetTopCrateOnEachStack());
    }
}

public class Solver : Solver<CraneOrdering, string>
{
    public Solver() : base("Day05/input.txt") { }

    public override string PartOne(CraneOrdering input)
        => input.GetTopCratesAfterOperationsUsing("CrateMover 9000");

    public override string PartTwo(CraneOrdering input)
        => input.GetTopCratesAfterOperationsUsing("CrateMover 9001");

    public override CraneOrdering ParseInput(IEnumerable<string> input)
    {
        var content = input.ToList();

        var drawnStacks = content
            .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
            .Reverse()
            .ToArray();

        IEnumerable<char> GetCratesOnColumn(int column)
        {
            // Column is 1-based
            var indice = column - 1;
            
            const int offset = 4;
            var index = 1 + indice * offset;

            return drawnStacks
                // Skip the indexes
                .Skip(1)
                // Retrieve the crates letter
                .Aggregate(
                    new Queue<char>(),
                    (crates, row) =>
                    {
                        var crate = row[index];

                        if (crate != ' ')
                        {
                            crates.Enqueue(row[index]);
                        }

                        return crates;
                    });
        }

        var columnCount = drawnStacks.First().Count(char.IsDigit);

        var stacks = Enumerable.Range(1, columnCount)
            .Select(i => new
            {
                Index = i,
                Crates = GetCratesOnColumn(i),
            })
            .ToDictionary(
                entry => entry.Index,
                entry => new Stack<char>(entry.Crates));

        var operations = content
            // Skip the crates stacks
            .SkipWhile(line => !string.IsNullOrWhiteSpace(line))
            // Skip the blank line
            .Skip(1)
            // Parse the operations
            .Select(line =>
            {
                var groups = CraneOperation.ParsingRegexp.Match(line)
                    .Groups
                    .Cast<Group>()
                    .Skip(1)
                    .Select(group => int.Parse(group.Value))
                    .ToArray();

                return new CraneOperation(groups[0], groups[1], groups[2]);
            });

        return new CraneOrdering(new CratesStack(stacks), operations);
    }
}
