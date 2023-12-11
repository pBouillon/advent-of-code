using AdventOfCode.Utils.Parsing;

namespace _2023.Day09;

public class Solver : Solver<Report, long>
{
    public Solver() : base("Day09/input.txt") { }

    public override long PartOne(Report input)
        => input.Histories.Sum(history => history.LastPrediction);

    public override long PartTwo(Report input)
        => input.Histories.Sum(history => history.FirstPrediction);

    public override Report ParseInput(IEnumerable<string> input)
    {
        var histories = input
            .Select(line => new History(line.AsLongArray()))
            .ToArray();

        return new Report(histories);
    }
}

public class Report(History[] histories)
{
    public History[] Histories { get; init; } = histories;
}

public class History(long[] measures)
{
    public long[] Measures { get; init; } = measures;

    public readonly long LastPrediction = ComputePredictedValueFrom(measures);
    public readonly long FirstPrediction = ComputePredictedValueFrom(measures.Reverse().ToArray());

    private static long ComputePredictedValueFrom(long[] measures)
    {
        var predicted = 0L;

        while (measures.Any(measure => measure != 0))
        {
            predicted += measures[^1];

            measures = measures.Zip(measures.Skip(1))
                .Select((a) => a.Second - a.First)
                .ToArray();
        }

        return predicted;
    }
}
