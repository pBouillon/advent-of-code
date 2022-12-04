namespace _2021.Day06;

public class Solver : Solver<Dictionary<int, long>, long>
{
    protected override string InputPath => "Day06/input.txt";

    private static Dictionary<int, long> GetNextGeneration(Dictionary<int, long> current)
    {
        var next = current.ToDictionary(
                entry => entry.Key - 1,
                entry => entry.Value);

        if (!next.ContainsKey(-1)) return next;

        next[6] = next.GetValueOrDefault(6, 0) + next[-1];
        next[8] = next.GetValueOrDefault(8, 0) + next[-1];
        next.Remove(-1);

        return next;
    }

    private static long GetPopulationAfterSomeTime(Dictionary<int, long> initial, int elapsedDays)
        => Enumerable.Range(0, elapsedDays)
            .Aggregate(
                initial,
                (current, _) => GetNextGeneration(current))
            .Values
            .Sum();

    public override long PartOne(Dictionary<int, long> input)
        => GetPopulationAfterSomeTime(input, 80);

    public override long PartTwo(Dictionary<int, long> input)
        => GetPopulationAfterSomeTime(input, 256);

    public override Dictionary<int, long> ReadInput(string inputPath)
        => File
            .ReadLines(inputPath)
            .First()
            .Split(",")
            .Select(int.Parse)
            .GroupBy(internalTimer => internalTimer)
            .ToDictionary(value => value.Key, value => (long)value.Count());
}
