using System.Text.RegularExpressions;

namespace _2023.Day02;

public class Solver : Solver<Game[], long>
{
    public Solver() : base("Day02/input.txt") { }

    public override long PartOne(Game[] games)
    {
        var setup = new SetOfCube(RedCount: 12, GreenCount: 13, BlueCount: 14);

        return games
            .Where(game => game.WouldBePossibleWith(setup))
            .Sum(game => game.Id);
    }

    public override long PartTwo(Game[] games)
        => games
            .Select(game => game.MinimalPossibleSet)
            .Sum(set => set.Power);

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
                .Select(SetOfCube.From)
                .ToArray();

            return new Game(gameId, hands);
        })
        .ToArray();
}

public record SetOfCube(int RedCount, int GreenCount, int BlueCount)
{
    public static SetOfCube From(string hand)
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

        return new SetOfCube(
            RedCount: redCount,
            GreenCount: greenCount,
            BlueCount: blueCount);
    }

    public long Power => RedCount * GreenCount * BlueCount;

    public bool WouldBePossibleWith(SetOfCube hand)
        => RedCount <= hand.RedCount
            && GreenCount <= hand.GreenCount
            && BlueCount <= hand.BlueCount;
}

public record Game(int Id, SetOfCube[] Sets)
{
    public bool WouldBePossibleWith(SetOfCube setup)
        => Sets.All(hand => hand.WouldBePossibleWith(setup));

    public SetOfCube MinimalPossibleSet
        => Sets.Aggregate(
            new SetOfCube(RedCount: 0, GreenCount: 0, BlueCount: 0),
            (current, next) => current with
            {
                RedCount = Math.Max(next.RedCount, current.RedCount),
                GreenCount = Math.Max(next.GreenCount, current.GreenCount),
                BlueCount = Math.Max(next.BlueCount, current.BlueCount)
            });
}
