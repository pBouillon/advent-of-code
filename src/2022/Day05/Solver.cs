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
    private readonly IDictionary<int, string> _stacks = new Dictionary<int, string>();

    public CratesStack(IDictionary<int, string> stacks)
        => _stacks = stacks;

    public void Apply(CraneOperation operation, string craneModel)
    {
        var (count, from, to) = operation;

        var moved = _stacks[from][^count..];

        if (craneModel == "CrateMover 9000")
        {
            moved = new string(moved.Reverse().ToArray());
        }

        _stacks[to] += moved;
        _stacks[from] = _stacks[from][..^count];
    }

    public IEnumerable<char> GetTopCrateOnEachStack()
        => _stacks.Values.Select(crates => crates[^1]);
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

        string GetCratesOnColumn(int column)
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
                    string.Empty,
                    (crates, row) => crates + row[index],
                    crates => crates.TrimEnd());
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
                entry => entry.Crates);

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
