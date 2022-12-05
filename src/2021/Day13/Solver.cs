using System.Text.RegularExpressions;

namespace _2021.Day13;

public enum Axis { X, Y }

public record Coordinate(int X, int Y);

public record FoldingInstruction(Axis Direction, int Offset);

public class Solver : Solver<(ISet<Coordinate>, IEnumerable<FoldingInstruction>), string>
{
    private readonly Dictionary<string, string> _letters = new()
    {
        { "011010011001111110011001", "A" },
        { "011010011000100010010110", "C" },
        { "100110011111100110011001", "H" },
        { "001100010001000110010110", "J" },
        { "100110101100101010101001", "K" },
        { "100110011001100110010110", "U" },
        { "111100010010010010001111", "Z" },
    };

    public Solver() : base("Day13/input.txt") { }

    private static ISet<Coordinate> GetPointsAfterFolds(ISet<Coordinate> points, IEnumerable<FoldingInstruction> instructions)
        => instructions.Aggregate(
            points,
            (current, instruction) => instruction.Direction switch
            {
                Axis.X => current.Select(point => point.X > instruction.Offset
                        ? point with { X = 2 * instruction.Offset - point.X }
                        : point)
                    .ToHashSet(),
                Axis.Y => current.Select(point => point.Y > instruction.Offset
                        ? point with { Y = 2 * instruction.Offset - point.Y }
                        : point)
                    .ToHashSet(),
                _ => throw new ArgumentOutOfRangeException()
            });

    public override string PartOne((ISet<Coordinate>, IEnumerable<FoldingInstruction>) input)
    {
        var (points, instructions) = input;

        return GetPointsAfterFolds(points, instructions.Take(1))
            .Count
            .ToString();
    }

    private string InterpretLetterAt(int index, ICollection<Coordinate> points)
    {
        var letterSpace = new
        {
            Length = 4,
            Height = 6,
            Padding = 1,
        };

        var padding = index * (letterSpace.Length + letterSpace.Padding);

        var hash = string.Empty;

        for (var y = 0; y < letterSpace.Height; ++y)
        {
            for (var x = padding; x < letterSpace.Length + padding; ++x)
            {
                hash += points.Contains(new Coordinate(x, y))
                    ? "1"
                    : "0";
            }
        }

        return _letters.GetValueOrDefault(hash, string.Empty);
    }

    public override string PartTwo((ISet<Coordinate>, IEnumerable<FoldingInstruction>) input)
    {
        var (points, instructions) = input;

        points = GetPointsAfterFolds(points, instructions);

        return Enumerable.Range(0, 8)
            .Aggregate(
                string.Empty,
                (current, next) => current + InterpretLetterAt(next, points));
    }

    public override (ISet<Coordinate>, IEnumerable<FoldingInstruction>) ParseInput(IEnumerable<string> input)
    {
        var content = input.ToList();

        var points = content.TakeWhile(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                var split = line
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();

                return new Coordinate(split[0], split[1]);
            })
            .ToHashSet();

        var instructions = content.SkipWhile(line => string.IsNullOrEmpty(line) || char.IsDigit(line[0]))
            .Select(line =>
            {
                var matches = Regex
                    .Match(line, @"fold along ([xy])=(\d+)")
                    .Groups
                    .Cast<Group>()
                    .Skip(1)
                    .Select(group => group.Value)
                    .ToArray();

                return new FoldingInstruction(
                    matches[0] == "x" ? Axis.X : Axis.Y,
                    int.Parse(matches[1]));
            });

        return (points, instructions);
    }
}
