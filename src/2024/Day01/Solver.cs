using AdventOfCode.Utils.Parsing;

namespace _2024.Day01;

public class Solver()
    : Solver<(Queue<long>, Queue<long>), long>("Day01/input.txt")
{
    public override long PartOne((Queue<long>, Queue<long>) input)
    {
        var (first, second) = input;

        var res = 0L;
        while (first.Count > 0)
        {
            var firstId = first.Dequeue();
            var secondId = second.Dequeue();

            res += Math.Abs(firstId - secondId);
        }

        return res;
    }


    public override long PartTwo((Queue<long>, Queue<long>) input)
    {
        var (first, second) = input;

        var occurrences = second
            .GroupBy(id => id)
            .ToDictionary(
                group => group.Key,
                group => group.Count());

        var res = 0L;
        while (first.Count > 0)
        {
            var firstId = first.Dequeue();

            res += firstId * occurrences.GetValueOrDefault(firstId);
        }

        return res;
    }

    public override (Queue<long>, Queue<long>) ParseInput(IEnumerable<string> input)
    {
        var ids = input.Select(line => line.AsLongArray());

        var firstIdsList = ids
            .Select(line => line.First())
            .Order();

        var secondIdsList = ids
            .Select(line => line.Last())
            .Order();

        return (
            new Queue<long>(firstIdsList),
            new Queue<long>(secondIdsList)
        );
    }
}

public class SolverTest : TestEngine<Solver, (Queue<long>, Queue<long>), long>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3",
        ])
        .ParsedAs((
            new Queue<long>([1, 2, 3, 3, 3, 4]),
            new Queue<long>([3, 3, 3, 4, 5, 9])
        ))
        .ExpectsResult(11)
        .WithTheActualSolutionBeing(1_530_215);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput((
            new Queue<long>([1, 2, 3, 3, 3, 4]),
            new Queue<long>([3, 3, 3, 4, 5, 9])
        ))
        .ExpectsResult(31)
        .WithTheActualSolutionBeing(26_800_609);
}
