using FluentAssertions;
using Xunit;

namespace _2025.Day09;

public class Solver()
    : Solver<Coordinate[]>("Day09/input.txt")
{
    public override Coordinate[] ParseInput(IEnumerable<string> input)
        => [.. input.Select(line =>
        {
            var parts = line
                .Split(',')
                .Select(long.Parse)
                .ToArray();

            return new Coordinate(parts[0], parts[1]);
        })];

    public override string PartOne(Coordinate[] input)
    {
        var coordinates = input.ToList();

        return coordinates
            .ToList()
            .Max(current => coordinates.Max(current.AreaCoveredByTheRectangleTo))
            .ToString();
    }

    public override string PartTwo(Coordinate[] coordinates)
    {
        throw new NotImplementedException();
    }
}

public sealed record Coordinate(long X, long Y)
{
    public long AreaCoveredByTheRectangleTo(Coordinate coordinate)
        => (Math.Abs(coordinate.X - X) + 1) * (Math.Abs(coordinate.Y - Y) + 1);

    public long ManhathanDistanceTo(Coordinate coordinate)
        => Math.Abs(coordinate.X - X) + Math.Abs(coordinate.Y - Y);
    
    public override string ToString()
        => $"({X}, {Y})";
}

public class SolverTest
    : TestEngine<Solver, Coordinate[]>
{
    [Trait("Part", "Subject")]
    [Theory]
    [InlineData(2, 5, 9, 7, 24)]
    [InlineData(7, 1, 11, 7, 35)]
    [InlineData(7, 3, 2, 3, 6)]
    [InlineData(2, 5, 11, 1, 50)]
    public void AreaCoveredUnitTests(int x1, int y1, int x2, int y2, int area)
    {
        var from = new Coordinate(x1, y1);
        var to = new Coordinate(x2, y2);

        from.AreaCoveredByTheRectangleTo(to).Should().Be(area);
    }

    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "7,1",
            "11,1",
            "11,7",
            "9,7",
            "9,5",
            "2,5",
            "2,3",
            "7,3",
        ])
        .ParsedAs([
            new(7,1),
            new(11,1),
            new(11,7),
            new(9,7),
            new(9,5),
            new(2,5),
            new(2,3),
            new(7,3),
        ])
        .ExpectsResult("50")
        .WithTheActualSolutionBeing("4771532800");

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([
            new(7,1),
            new(11,1),
            new(11,7),
            new(9,7),
            new(9,5),
            new(2,5),
            new(2,3),
            new(7,3),
        ])
        .ExpectsResult("0")
        .WithTheActualSolutionBeing("0");
}
