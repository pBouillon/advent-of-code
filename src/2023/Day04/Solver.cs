
using System.Text.RegularExpressions;

namespace _2023.Day04;

public class Solver : Solver<ScratchCard[], long>
{
    public Solver() : base("day04/input.txt") { }

    public override long PartOne(ScratchCard[] input)
        => input.Sum(card => card.Points);

    public override long PartTwo(ScratchCard[] input)
    {
        throw new NotImplementedException();
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

    public long Points => (long)Math.Pow(
        2,
        ScratchedNumbers.Intersect(WinningNumbers).Count() - 1);
}
