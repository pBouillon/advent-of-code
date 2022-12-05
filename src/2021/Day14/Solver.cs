namespace _2021.Day14;

public class Solver : Solver<(string, Dictionary<string, string>), long>
{
    public Solver() : base("Day14/input.txt") { }

    private static Dictionary<string, long> GetPairsCount(string polymer)
        => Enumerable.Range(0, polymer.Length - 1)
            .Select(i => polymer.Substring(i, 2))
            .GroupBy(pair => pair)
            .ToDictionary(
                group => group.Key,
                group => (long)group.Count());

    private static (Dictionary<string, long>, Dictionary<string, long>) GetNextPolymerPairs(
        Dictionary<string, long> pairs, Dictionary<string, long> count, IReadOnlyDictionary<string, string> conversions)
    {
        var next = pairs.ToDictionary(
            entry => entry.Key,
            entry => entry.Value);

        foreach (var (pair, pairCount) in pairs)
        {
            var production = conversions.GetValueOrDefault(pair);
            if (production is null) continue;

            // Increase the count of the produced element by the number of pairs that produce it
            count[production] = count.GetValueOrDefault(production) + pairCount;

            // Update all all the polymer pairs producing the element: AB -> Ax and xB
            next[pair] -= pairCount;

            var (a, x, b) = (pair[0], production, pair[1]);

            next[$"{a}{x}"] = next.GetValueOrDefault($"{a}{x}") + pairCount;
            next[$"{x}{b}"] = next.GetValueOrDefault($"{x}{b}") + pairCount;
        }

        // Remove the empty entries to not iterate on it on the next cycle
        next = next.Where(entry => entry.Value > 0)
            .ToDictionary(
                entry => entry.Key,
                entry => entry.Value);

        return (next, count);
    }

    private static Dictionary<string, long> GetElementsCountAfter(int cycles, string polymer, Dictionary<string, string> conversions)
    {
        var pairs = GetPairsCount(polymer);

        var count = polymer.ToHashSet()
            .ToDictionary(
                element => element.ToString(),
                element => (long)polymer.Count(@char => @char == element));

        (_, count) = Enumerable.Range(0, cycles)
            .Aggregate(
                (pairs, count),
                (current, _) => GetNextPolymerPairs(current.pairs, current.count, conversions));

        return count;
    }

    public override long PartOne((string, Dictionary<string, string>) input)
    {
        var (polymer, conversions) = input;
        var count = GetElementsCountAfter(10, polymer, conversions);

        var mostCommonCount = count.Max(elementCount => elementCount.Value);
        var leastCommonCount = count.Min(elementCount => elementCount.Value);

        return mostCommonCount - leastCommonCount;
    }

    public override long PartTwo((string, Dictionary<string, string>) input)
    {
        var (polymer, conversions) = input;
        var count = GetElementsCountAfter(40, polymer, conversions);

        var mostCommonCount = count.Max(elementCount => elementCount.Value);
        var leastCommonCount = count.Min(elementCount => elementCount.Value);

        return mostCommonCount - leastCommonCount;
    }

    public override (string, Dictionary<string, string>) ParseInput(IEnumerable<string> input)
    {
        var content = input.ToList();

        var polymer = content.First();

        var pairs = content.Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                var split = line.Split(" -> ");
                return new { Pair = split[0], Value = split[1] };
            })
            .ToDictionary(
                conversion => conversion.Pair,
                conversion => conversion.Value);

        return (polymer, pairs);
    }
}
