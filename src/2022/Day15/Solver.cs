using System.Text.RegularExpressions;

namespace _2022.Day15;

public enum Status { Empty, Sensor, Beacon }

public record Coordinate(int Column, int Depth)
{
    public int DistanceTo(Coordinate target)
        => Math.Abs(Depth - target.Depth) + Math.Abs(Column - target.Column);

    public long TuningFrequency => Column * 4_000_000 + Depth;
}

public record Sensor(Coordinate Position, Coordinate ClosestBeacon)
{
    public static readonly Regex ParsingRegexp = new(
        "Sensor at x=(-?\\d+), y=(-?\\d+): closest beacon is at x=(-?\\d+), y=(-?\\d+)",
        RegexOptions.Compiled);

    private readonly int _range = Position.DistanceTo(ClosestBeacon);
   
    private int Radius => _range * 2 + 1;

    public static Sensor FromRaw(string raw)
    {
        var values = ParsingRegexp.Match(raw)
                    .Groups
                    .Cast<Group>()
                    .Skip(1)
                    .Select(group => int.Parse(group.Value))
                    .ToArray();

        var sensorPosition = new Coordinate(values[0], values[1]);
        var closestBeaconPosition = new Coordinate(values[2], values[3]);

        return new Sensor(sensorPosition, closestBeaconPosition);
    }

    public IEnumerable<Coordinate> ReadingsOnDepth(int depth)
    {
        var isAtRange = Position.DistanceTo(Position with { Depth = depth }) <= _range;

        var rangeAtDepth = Radius - (Math.Abs(Position.Depth - depth) * 2);

        return isAtRange
            ? Enumerable
                .Range(Position.Column - rangeAtDepth / 2, rangeAtDepth)
                .Select(column => new Coordinate(column, depth))
            : Enumerable.Empty<Coordinate>();
    }
}

public class Cave
{
    private readonly List<Sensor> _sensors;

    public Cave(List<Sensor> sensors)
        => _sensors = sensors;

    public int GetKnownPositionsAtDepth(int depth)
    {
        var coordinates = new HashSet<Coordinate>();

        var coordinatesCovered = _sensors
            .SelectMany(sensor => sensor.ReadingsOnDepth(depth))
            .ToHashSet()
            .Count;

        var sensorsOnLine = _sensors
            .Select(sensor => sensor.Position)
            .Count(sensor => sensor.Depth == depth);

        var beaconsOnLine = _sensors
            .Select(sensor => sensor.ClosestBeacon)
            .ToHashSet()
            .Count(beacon => beacon.Depth == depth);

        return coordinatesCovered - sensorsOnLine - beaconsOnLine;
    }

    public long GetTuningFrequencyOfBeacon(int offsetLimit = 4_000_000)
    {
        for (var i = 0; i < offsetLimit; ++i)
        {
            var readings = _sensors
                .SelectMany(sensor => sensor.ReadingsOnDepth(i))
                .ToHashSet()
                .Select(position => position.Column)
                .Where(column => column >= 0 && column <= offsetLimit)
                .OrderBy(column => column)
                .ToArray();

            for (var j = readings[1]; j < readings[^1]; ++j)
            {
                var previous = readings[j - 1];
                var current = readings[j];

                var gapFound = (current - previous) == 2;

                if (gapFound)
                {
                    var beacon = new Coordinate(current - 1, i);
                    return beacon.TuningFrequency;
                }
            }
        }

        return -1;
    }
}

public class Solver : Solver<Cave, long>
{
    public Solver() : base("Day15/input.txt") { }

    public override long PartOne(Cave input)
        => input.GetKnownPositionsAtDepth(2_000_000);

    public override long PartTwo(Cave input)
        => input.GetTuningFrequencyOfBeacon();

    public override Cave ParseInput(IEnumerable<string> input)
    {
        var sensors = input.Select(Sensor.FromRaw).ToList();

        var depths = sensors
            .SelectMany(sensor => new[] { sensor.Position, sensor.ClosestBeacon })
            .Select(coordinate => coordinate.Column)
            .OrderBy(column => column)
            .ToList();

        return new Cave(sensors);
    }
}
