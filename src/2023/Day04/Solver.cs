
using System.Text.RegularExpressions;

namespace _2023.Day04;

public class Solver : Solver<ScratchCard[], long>
{
    public Solver() : base("day04/input.txt") { }

    public override long PartOne(ScratchCard[] input)
        => input.Sum(card => card.Points);

    public override long PartTwo(ScratchCard[] input)
    {
        var gainsOf = input.ToDictionary(
            card => card.Id,
            card => card.MatchingNumbers);

        var cache = new Dictionary<int, int>();

        int ComputeWonCardsFor(int cardId)
        {
            if (cache.TryGetValue(cardId, out var cached))
            {
                return cached;
            }

            var matchingNumbers = gainsOf[cardId];
            if (matchingNumbers == 0)
            {
                cache[cardId] = 0;
                return 0;
            }

            var gain = matchingNumbers
                + Enumerable
                    .Range(0, matchingNumbers)
                    .Sum(offset => ComputeWonCardsFor(cardId + offset + 1));

            cache[cardId] = gain;
            return gain;
        }

        return input.Aggregate(
            seed: 0,
            (count, card) => count
                // The card itself
                + 1
                // The cards it will make we earn
                + ComputeWonCardsFor(card.Id));
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

                static int[] numbersOf(string raw) => raw
                    .Split(' ')
                    .Where(x => x.Length > 0)
                    .Select(int.Parse)
                    .ToArray();

                return new ScratchCard
                {
                    Id = int.Parse(cardId),
                    ScratchedNumbers = numbersOf(rawNumbers[1]),
                    WinningNumbers = numbersOf(rawNumbers[0]),
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
