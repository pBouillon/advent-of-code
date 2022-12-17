using _2022.Day11;

namespace _2022.Day12;

public record Coordinate(int X, int Y);

public record Edge
{
    public Coordinate Coordinate { get; }

    public char ElevationLevel { get; }

    public bool IsStartingEdge { get; }

    public bool IsEndingEdge { get; }

    public readonly List<Edge> Neighbors = new();

    public readonly List<Edge> ReacheableFrom = new();

    public Edge(Coordinate coordinate, char elevationLevel)
    {
        Coordinate = coordinate;

        ElevationLevel = elevationLevel switch
        {
            'S' => 'a',
            'E' => 'z',
            _ => elevationLevel,
        };

        IsStartingEdge = elevationLevel == 'S';

        IsEndingEdge = elevationLevel == 'E';
    }

    public void AddNeighbor(Edge edge)
    {
        var canReach = ElevationLevel - edge.ElevationLevel >= -1;

        if (canReach)
        {
            Neighbors.Add(edge);
            edge.ReacheableFrom.Add(this);
        }
    }
}

public class ElevationMap
{
    private readonly List<Edge> _edges = new();

    public Edge Source
        => _edges.First(edge => edge.IsStartingEdge);

    public Edge Target 
        => _edges.First(edge => edge.IsEndingEdge);

    public ElevationMap(IEnumerable<Edge> edges)
        => _edges = edges.ToList();

    public int FindShortestPathLength(Edge from, char toAnyElevationOf)
    {
        var distanceTo = _edges.ToDictionary(
            edge => edge.Coordinate,
            edge => edge.Coordinate == from.Coordinate
                ? 0
                : int.MaxValue);

        var visited = new HashSet<Coordinate>();
        var toVisit = _edges.ToList();

        while (toVisit.Any())
        {
            var next = toVisit
                .OrderBy(edge => distanceTo[edge.Coordinate])
                .First();

            if (next.ElevationLevel == toAnyElevationOf)
            {
                return distanceTo[next.Coordinate];
            }

            visited.Add(next.Coordinate);

            toVisit = toVisit
                .Where(edge => edge.Coordinate != next.Coordinate)
                .ToList();

            var isParentReacheable = distanceTo[next.Coordinate] != int.MaxValue;
            if (!isParentReacheable) continue;

            var neighbors = next.ReacheableFrom.Where(node => !visited.Contains(node.Coordinate));

            foreach (var neighbor in neighbors)
            {
                var neighborCoordinate = neighbor.Coordinate;

                var distanceToNeighbor = distanceTo[next.Coordinate] + 1;

                var isNeighborCloser = distanceToNeighbor < distanceTo[neighborCoordinate];

                if (isNeighborCloser)
                {
                    distanceTo[neighborCoordinate] = distanceToNeighbor;
                }
            }
        }

        throw new Exception($"No path found to any edge of elevation '{toAnyElevationOf}'");
    }

    public int FindShortestPathLength(Edge from, Edge to)
    {
        // Initialization
        var distanceTo = _edges.ToDictionary(
            edge => edge.Coordinate,
            edge => edge.Coordinate == from.Coordinate
                ? 0
                : int.MaxValue);

        // Traversal
        var visited = new HashSet<Coordinate>();
        var toVisit = _edges.ToList();

        while (toVisit.Any())
        {
            // Get the closest node and remove it from the ones to visit
           var next = toVisit
               .OrderBy(edge => distanceTo[edge.Coordinate])
               .First();

            visited.Add(next.Coordinate);

            toVisit = toVisit
                .Where(edge => edge.Coordinate != next.Coordinate)
                .ToList();

            // Update the distance of each neighbors
            var isParentReacheable = distanceTo[next.Coordinate] != int.MaxValue;
            if (!isParentReacheable) continue;

            var neighbors = next.Neighbors.Where(node => !visited.Contains(node.Coordinate));

            foreach (var neighbor in neighbors)
            {
                var neighborCoordinate = neighbor.Coordinate;

                var distanceToNeighbor = distanceTo[next.Coordinate] + 1;

                var isNeighborCloser = distanceToNeighbor < distanceTo[neighborCoordinate];

                if (isNeighborCloser)
                {
                    distanceTo[neighborCoordinate] = distanceToNeighbor;
                }
            }
        }

        return distanceTo[to.Coordinate];
    }
}

public class Solver : Solver<ElevationMap, int>
{
    public Solver() : base ("Day12/input.txt") { }

    public override int PartOne(ElevationMap input)
        => input.FindShortestPathLength(from: input.Source, to: input.Target);

    public override int PartTwo(ElevationMap input)
        => input.FindShortestPathLength(from: input.Target, toAnyElevationOf: 'a');

    public override ElevationMap ParseInput(IEnumerable<string> input)
    {
        var edges = new Dictionary<Coordinate, Edge>();

        var rows = input.ToArray();

        // Create the edges
        for (var i = 0; i < rows.Length; ++i)
        {
            for (var j = 0; j < rows[i].Length; ++j)
            {
                var coordinate = new Coordinate(i, j);
                var elevationLevel = rows[i][j];

                var edge = new Edge(coordinate, elevationLevel);

                edges.Add(coordinate, edge);
            }
        }

        // Add their neighbors
        for (var i = 0; i < rows.Length; ++i)
        {
            for (var j = 0; j < rows[i].Length; ++j)
            {
                var coordinate = new Coordinate(i, j);

                var edge = edges[coordinate];
                List<Coordinate> neighbors = GetNeighborCoordinates(edge, rows);

                foreach (var neighborCoordinate in neighbors)
                {
                    var neighbor = edges[neighborCoordinate];
                    edge.AddNeighbor(neighbor);
                }
            }
        }

        // Create the elevation map
        return new ElevationMap(edges.Values);
    }

    private static List<Coordinate> GetNeighborCoordinates(Edge edge, string[] rows)
    {
        var coordinate = edge.Coordinate;

        var neighbors = new List<Coordinate>();

        // Top neighbor
        if (coordinate.X - 1 > -1)
            neighbors.Add(coordinate with { X = coordinate.X - 1 });

        // Down neighbor
        if (coordinate.X + 1 < rows.Length)
            neighbors.Add(coordinate with { X = coordinate.X + 1 });

        // Left neighbor
        if (coordinate.Y - 1 > -1)
            neighbors.Add(coordinate with { Y = coordinate.Y - 1 });

        // Right neighbor
        if (coordinate.Y + 1 < rows[coordinate.X].Length)
            neighbors.Add(coordinate with { Y = coordinate.Y + 1 });

        return neighbors;
    }
}
