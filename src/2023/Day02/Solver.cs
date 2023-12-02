using System.Text.RegularExpressions;

namespace _2023.Day02;

public class Solver : Solver<Game[], long>
{
    public Solver() : base("Day02/input.txt") { }

    public override long PartOne(Game[] games)
    {
        var setup = new Hand(RedCount: 12, GreenCount: 13, BlueCount: 14);

        return games
            .Where(game => game.WouldBePossibleWith(setup))
            .Sum(game => game.Id);
    }

    public override long PartTwo(Game[] games)
        => 0;

    public override Game[] ParseInput(IEnumerable<string> input)
        => input.Select(line =>
        {
            var gameId = Regex.Match(line, @"Game (\d+): ")
                .Groups
                .Cast<Group>()
                .Skip(1)
                .Select(group => int.Parse(group.Value))
                .Single();

            var hands = line.Split(';')
                .Select(Hand.From)
                .ToArray();

            return new Game(gameId, hands);
        })
        .ToArray();
}

public record Hand(int RedCount, int GreenCount, int BlueCount)
{
    public static Hand From(string hand)
    {
        var redCount = Regex.Match(hand, @"(\d+) red")
                .Groups
                .Cast<Group>()
                .Select(group => int.Parse(group.Value))
                .Skip(1)
                .FirstOrDefault();

        var greenCount = Regex.Match(hand, @"(\d+) green")
                .Groups
                .Cast<Group>()
                .Select(group => int.Parse(group.Value))
                .Skip(1)
                .FirstOrDefault();

        var blueCount = Regex.Match(hand, @"(\d+) blue")
                .Groups
                .Cast<Group>()
                .Select(group => int.Parse(group.Value))
                .Skip(1)
                .FirstOrDefault();

        return new Hand(
            RedCount: redCount,
            GreenCount: greenCount,
            BlueCount: blueCount);
    }

    public bool WouldBePossibleWith(Hand hand)
        => RedCount <= hand.RedCount
            && GreenCount <= hand.GreenCount
            && BlueCount <= hand.BlueCount;
}

public record Game(int Id, Hand[] Hands)
{
    public bool WouldBePossibleWith(Hand setup)
        => Hands.All(hand => hand.WouldBePossibleWith(setup));
}
