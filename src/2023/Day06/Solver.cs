
using System.Text.RegularExpressions;

namespace _2023.Day06;

public class Solver : Solver<Race[], long>
{
    public Solver() : base("Day06/input.txt") { }

    public override long PartOne(Race[] races)
    {
        var count = 1;
        foreach (var race in races)
        {
            var waysToWin = Enumerable.Range(0, race.Time)
                .Select((holdTime) => holdTime * (race.Time - holdTime))
                .Where(distance => distance > race.BestDistance)
                .ToArray();

            count *= waysToWin.Length;
        }
        return count;
    }
    //=> races.Aggregate(
    //        seed: 1,
    //        (curr, race) => Enumerable
    //            .Range(0, race.Time)
    //            .Select((holdTime) => holdTime * (race.Time - holdTime))
    //            .Where(distance => distance > race.BestDistance)
    //            .Count());

    public override long PartTwo(Race[] races)
    {
        throw new NotImplementedException();
    }
    public override Race[] ParseInput(IEnumerable<string> input)
    {
        var document = input.ToArray();

        var times = GetNumbers(document[0]);
        var distances = GetNumbers(document[1]);

        return times.Zip(
                distances,
                (time, distance) => new Race(time, distance))
            .ToArray();

        static int[] GetNumbers(string line)
            => Regex.Matches(line, @"\d+")
                .Cast<Match>()
                .Select(match => int.Parse(match.Value))
                .ToArray();
    }
}

public record Race(int Time, int BestDistance);
