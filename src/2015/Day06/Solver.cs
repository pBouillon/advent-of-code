using System.Text.RegularExpressions;

namespace _2015.Day06;

public enum Status
{
    Toggle,
    TurnOn,
    TurnOff,
}

public record Coordinate(int X, int Y);

public record Command(Status Status, Coordinate Source, Coordinate Target);

public class Solver : Solver<Command[], long>
{
    protected override string InputPath => "Day06/input.txt";

    public override long PartOne(Command[] input)
    {
        var grid = new bool[1000, 1000];

        input
            .ToList()
            .ForEach(command =>
            {
                var (status, origin, target) = command;

                for (var row = origin.X; row <= target.X; ++row)
                {
                    for (var column = origin.Y; column <= target.Y; ++column)
                    {
                        grid[row, column] = status switch
                        {
                            Status.Toggle => !grid[row, column],
                            Status.TurnOn => true,
                            Status.TurnOff => false,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }
                }
            });

        return grid
            .Cast<bool>()
            .Count(lightened => lightened);
    }

    public override long PartTwo(Command[] input)
    {
        var grid = new int[1000, 1000];

        input
            .ToList()
            .ForEach(command =>
            {
                var (status, origin, target) = command;

                for (var row = origin.X; row <= target.X; ++row)
                {
                    for (var column = origin.Y; column <= target.Y; ++column)
                    {
                        grid[row, column] = status switch
                        {
                            Status.Toggle => grid[row, column] + 2,
                            Status.TurnOn => grid[row, column] + 1,
                            Status.TurnOff => Math.Max(grid[row, column] - 1, 0),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }
                }
            });

        return grid
            .Cast<int>()
            .Sum();
    }

    public override Command[] ReadInput(string inputPath)
        => File
            .ReadLines(inputPath)
            .Select(line =>
            {
                var status = line.Contains("turn on")
                    ? Status.TurnOn
                    : line.Contains("turn off")
                        ? Status.TurnOff
                        : Status.Toggle;

                var coordinates = Regex
                    .Match(line, @"(\d+),(\d+) through (\d+),(\d+)")
                    .Groups
                    .Cast<Group>()
                    .Skip(1)
                    .Select(group => int.Parse(group.Value))
                    .ToArray();

                return new Command(
                    status,
                    new Coordinate(coordinates[0], coordinates[1]),
                    new Coordinate(coordinates[2], coordinates[3]));
            })
            .ToArray();
}
