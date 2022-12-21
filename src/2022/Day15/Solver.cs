using _2022.Day05;
using _2022.Day14;

using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace _2022.Day15;

public record Coordinate(int X, int Y)
{
    public int Depth => X;
    public int Width => Y;
}

public record Sensor(Coordinate Position, Coordinate ClosestBeacon)
{
    public static readonly Regex ParsingRegexp = new(
        "Sensor at x=(-?\\d+), y=(-?\\d+): closest beacon is at x=(-?\\d+), y=(-?\\d+)",
        RegexOptions.Compiled);

    public int Range = Math.Abs(Position.X - ClosestBeacon.X)
        + Math.Abs(Position.Y - ClosestBeacon.Y);

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
}

public enum Status { Unknown, Empty, Sensor, Beacon }

public class Cave
{
    private readonly Status[,] _cave;

    public Cave(int depth, int width)
    {
        _cave = new Status[depth, width];

        for (var i = 0; i < depth * width; ++i)
        {
            _cave[i % depth, i / depth] = Status.Unknown;
        }
    }

    public void AddReadingOf(Sensor sensor)
    {
        var (position, beacon) = sensor;

        _cave[position.Depth, position.Width] = Status.Beacon;
        _cave[beacon.Depth, beacon.Width] = Status.Sensor;

        var current = position with { X = Math.Max(0, position.X - sensor.Range) };
        var width = 0;

        do
        {
            // TODO - Draw pixel Sensor's readings
            //var cell = _cave[position.Depth, column];

            //if (cell == Status.Unknown)
            //{
            //    cell = Status.Empty;
            //}

            width += current.Depth < position.Depth ? 2 : -2;
            current = current with { X = current.X + 1 };
        } while (width > 0);
    }

    public override string ToString()
    {
        var readings = new StringBuilder();

        for (int depth = 0; depth < _cave.GetLength(0); depth++)
        {
            for (int column = 0; column < _cave.GetLength(1); column++)
            {
                var symbol = _cave[depth, column] switch
                {
                    Status.Unknown => '.',
                    Status.Beacon => 'B',
                    Status.Sensor => 'S',
                    Status.Empty => '#',
                    _ => '?',
                };

                readings.Append(symbol);
            }

            readings.AppendLine();
        }

        return readings.ToString();
    }
}

public class Solver : Solver<Cave, int>
{
    public Solver() : base("Day15/input.txt") { }

    public override int PartOne(Cave input)
    {
        throw new NotImplementedException();
    }

    public override int PartTwo(Cave input)
    {
        throw new NotImplementedException();
    }

    public override Cave ParseInput(IEnumerable<string> input)
    {
        var sensors = input.Select(Sensor.FromRaw).ToList();

        var allCoordinates = sensors.SelectMany(sensor => new[] { sensor.Position, sensor.ClosestBeacon }).ToList();

        var depth = allCoordinates.Select(coordinate => coordinate.X).Max();
        var width = allCoordinates.Select(coordinate => coordinate.Y).Max();

        var cave = new Cave(depth, width);

        foreach (var sensor in sensors)
        {
            cave.AddReadingOf(sensor);
        }

        return cave;
    }
}
