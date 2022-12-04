using Newtonsoft.Json.Linq;

namespace _2021.Day09;

public record Coordinate(int X, int Y);

public class Solver : Solver<int[][], long>
{
    protected override string InputPath => "Day09/input.txt";

    private static IEnumerable<Coordinate> GetNeighbors(Coordinate coordinate, IReadOnlyList<int[]> grid)
    {
        var neighbors = new List<Coordinate>();

        // Top neighbor
        if (coordinate.X - 1 > -1)
            neighbors.Add(coordinate with { X = coordinate.X - 1 });

        // Down neighbor
        if (coordinate.X + 1 < grid.Count)
            neighbors.Add(coordinate with { X = coordinate.X + 1 });

        // Left neighbor
        if (coordinate.Y - 1 > -1)
            neighbors.Add(coordinate with { Y = coordinate.Y - 1 });

        // Right neighbor
        if (coordinate.Y + 1 < grid[0].Length)
            neighbors.Add(coordinate with { Y = coordinate.Y + 1 });

        return neighbors;
    }

    private static bool IsLowPointCoordinate(Coordinate candidate, IReadOnlyList<int[]> grid)
        => GetNeighbors(candidate, grid)
            .All(neighbor => grid[candidate.X][candidate.Y] < grid[neighbor.X][neighbor.Y]);

    private static IEnumerable<Coordinate> GetLowestPointsCoordinates(IReadOnlyList<int[]> grid)
    {
        var lowestPointCoordinates = new List<Coordinate>();

        for (var i = 0; i < grid.Count; ++i)
        {
            for (var j = 0; j < grid[i].Length; ++j)
            {
                var candidate = new Coordinate(i, j);
                if (IsLowPointCoordinate(candidate, grid)) lowestPointCoordinates.Add(candidate);
            }
        }

        return lowestPointCoordinates;
    }

    public override long PartOne(int[][] input)
        => GetLowestPointsCoordinates(input)
            .Sum(lowestPoint => input[lowestPoint.X][lowestPoint.Y] + 1);

    private static IEnumerable<Coordinate> GetBasin(Coordinate lowPoint, IReadOnlyList<int[]> grid)
    {
        var visited = new Stack<Coordinate>();

        var basin = new Stack<Coordinate>();
        basin.Push(lowPoint);

        var candidates = new Stack<Coordinate>();
        foreach (var candidate in GetNeighbors(lowPoint, grid))
        {
            candidates.Push(candidate);
        }

        while (candidates.Any())
        {
            var candidate = candidates.Pop();
            var value = grid[candidate.X][candidate.Y];

            visited.Push(candidate);

            if (basin.Contains(candidate) || value == 9) continue;

            basin.Push(candidate);

            var neighbors = GetNeighbors(candidate, grid)
                .Where(neighbor => !visited.Contains(neighbor));

            foreach (var neighbor in neighbors)
            {
                candidates.Push(neighbor);
            }
        }

        return basin;
    }

    public override long PartTwo(int[][] input)
        => GetLowestPointsCoordinates(input)
            .Select(coordinate => GetBasin(coordinate, input).Count())
            .OrderByDescending(size => size)
            .Take(3)
            .Aggregate(1, (sum, size) => sum * size);

    public override int[][] ReadInput(string inputPath)
        => File
            .ReadLines(inputPath)
            .Select(line => line.Select(value => value.ToString())
                .Select(int.Parse)
                .ToArray())
            .ToArray();
}
