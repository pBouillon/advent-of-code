
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace _2023.Day06;

public class Solver : Solver<Race[], long>
{
    public Solver() : base("Day06/input.txt") { }

    public override long PartOne(Race[] races)
        => races.Aggregate(
            seed: 1,
            (curr, race) => curr *= Enumerable.Range(0, (int)race.Time)
                .SkipWhile(holdTime => !race.BeatBestTimeWithPressedFor(holdTime))
                .TakeWhile(holdTime => race.BeatBestTimeWithPressedFor(holdTime))
                .Count());

    public override long PartTwo(Race[] races)
    {
        var allTimes = races.Select(race => race.Time.ToString());
        var allDistances = races.Select(race => race.BestDistance.ToString());

        var longerRace = new Race(
            Time: long.Parse(string.Join("", allTimes)),
            BestDistance: long.Parse(string.Join("", allDistances)));

        var (x1, x2) = longerRace.Roots();

        x1 = Math.Floor(x1 + 1) ;
        x2 = Math.Ceiling(x2 - 1);

        return (long)(x2 - x1) + 1;
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

public record Race(long Time, long BestDistance)
{
    public bool BeatBestTimeWithPressedFor(long holdTime)
        => (holdTime * (Time - holdTime)) > BestDistance;

    public (double, double) Roots()
    {
        var a = -1;
        var b = Time;
        var c = - BestDistance;

        var delta = Math.Pow(b, 2) - 4 * a * c;

        var x1 = (-b + Math.Sqrt(delta)) / 2 * a;
        var x2 = (-b - Math.Sqrt(delta)) / 2 * a;

        return (x1, x2);
    }
}
