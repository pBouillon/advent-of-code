
using System.Text.RegularExpressions;

namespace _2023.Day04;

public class Solver : Solver<ScratchCard[], long>
{
    public Solver() : base("day04/input.txt") { }

    public override long PartOne(ScratchCard[] input)
        => input.Sum(card => card.Points);

    public override long PartTwo(ScratchCard[] input)
    {
        var cards = input.ToDictionary(card => card.Id);

        var toProcess = new Queue<ScratchCard>();
        foreach (var card in input)
        {
            toProcess.Enqueue(card);
        }

        var processed = 0;
        while (toProcess.Count > 0)
        {
            var card = toProcess.Dequeue();

            Enumerable.Range(card.Id, card.MatchingNumbers)
                .Select(id => cards[id + 1])
                .ToList()
                .ForEach(toProcess.Enqueue);
         
            ++processed;
        }

        return processed;
    }

    public override ScratchCard[] ParseInput(IEnumerable<string> input)
        => input
            .Select(card =>
            {
                var cardId = Regex
                    .Match(card, @"Card\s+(?<id>\d+):")
                    .Groups["id"]
                    .Value;

                var numbersIndex = card.LastIndexOf(':') + 1;
                var rawNumbers = card[(numbersIndex + 1)..].Split('|');

                var scratchedNumbers = rawNumbers[1]
                    .Split(' ')
                    .Where(x => x.Length > 0)
                    .Select(int.Parse)
                    .ToArray();

                var winningNumbers = rawNumbers[0]
                    .Split(' ')
                    .Where(x => x.Length > 0)
                    .Select(int.Parse)
                    .ToArray();

                return new ScratchCard
                {
                    Id = int.Parse(cardId),
                    ScratchedNumbers = scratchedNumbers,
                    WinningNumbers = winningNumbers,
                };
            })
            .ToArray();
}

public class ScratchCard
{
    public required int Id { get; init; }
    public required IReadOnlyList<int> ScratchedNumbers { get; init; } = [];
    public required IReadOnlyList<int> WinningNumbers { get; init; } = [];

    public int MatchingNumbers => ScratchedNumbers.Intersect(WinningNumbers).Count();

    public long Points => (long)Math.Pow(2, MatchingNumbers - 1);
}
