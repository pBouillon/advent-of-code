namespace _2023.Day07;

public class Solver : Solver<Hand[], long>
{
    public Solver() : base("Day07/input.txt") { }

    public override long PartOne(Hand[] hands)
        => hands
            .OrderBy(hand => hand, new HandComparer())
            .Select((hand, rank) => hand.Bid * (rank + 1))
            .Sum();

    public override long PartTwo(Hand[] hands)
        => hands
            .OrderBy(hand => hand, new HandWithJokerComparer())
            .Select((hand, rank) => hand.Bid * (rank + 1))
            .Sum();

    public override Hand[] ParseInput(IEnumerable<string> input)
        => input
            .Select(line =>
            {
                var parts = line.Split(' ');
                return new Hand(parts[0], long.Parse(parts[1]));
            })
            .ToArray();
}

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand? x, Hand? y)
    {
        var strengthDifference = GetHandType(y!)
            .CompareTo(GetHandType(x!));

        if (strengthDifference != 0) return strengthDifference;

        return x!.Cards.Select(CardValue)
            .Zip(y!.Cards.Select(CardValue))
            .Select((values) => values.First - values.Second)
            .FirstOrDefault(diff => diff != 0);
    }

    private static HandType GetHandType(Hand hand)
    {
        var cardFrequency = hand.Cards
            .GroupBy(card => card)
            .Select(group => group.Count())
            .OrderByDescending(card => card)
            .ToArray();

        return cardFrequency switch
        {
            [5] => HandType.FiveOfAKind,
            [4, 1] => HandType.FourOfAKind,
            [3, 2] => HandType.FullHouse,
            [3, 1, 1] => HandType.ThreeOfAKind,
            [2, 2, 1] => HandType.TwoPair,
            [2, 1, 1, 1] => HandType.OnePair,
            _ => HandType.HighCard
        };
    }

    private int CardValue(char card)
        => card switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            'T' => 10,
            var digit when char.IsDigit(card) => int.Parse(digit.ToString()),
            _ => 0,
        };
}

public class HandWithJokerComparer : IComparer<Hand>
{
    public int Compare(Hand? x, Hand? y)
    {
        var strengthDifference = GetHandType(y!)
            .CompareTo(GetHandType(x!));

        if (strengthDifference != 0) return strengthDifference;

        return x!.Cards.Select(CardValue)
            .Zip(y!.Cards.Select(CardValue))
            .Select((values) => values.First - values.Second)
            .FirstOrDefault(diff => diff != 0);
    }

    private static HandType GetHandType(Hand hand)
    {
        var jokersCount = hand.Cards.Count(card => card == 'J');
        if (jokersCount == 5) return HandType.FiveOfAKind;

        var cardFrequency = hand.Cards
            .Where(card => card != 'J')
            .GroupBy(card => card)
            .Select(group => group.Count())
            .OrderByDescending(card => card)
            .ToArray();

        cardFrequency[0] += jokersCount;

        return cardFrequency switch
        {
            [5] => HandType.FiveOfAKind,
            [4, 1] => HandType.FourOfAKind,
            [3, 2] => HandType.FullHouse,
            [3, 1, 1] => HandType.ThreeOfAKind,
            [2, 2, 1] => HandType.TwoPair,
            [2, 1, 1, 1] => HandType.OnePair,
            _ => HandType.HighCard
        };
    }

    private int CardValue(char card)
        => card switch
        {
            'J' => 0,
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'T' => 10,
            var digit when char.IsDigit(card) => int.Parse(digit.ToString()),
            _ => 0,
        };
}

public class Hand(string cards, long bid)
{
    public string Cards { get; init; } = cards;
    public long Bid { get; init; } = bid;
}

public enum HandType
{
    FiveOfAKind,
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard,
}
