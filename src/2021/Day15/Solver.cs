namespace _2021.Day15;

public record Coordinate(int X, int Y)
{
    public IEnumerable<Coordinate> GetNeighbors()
        => new[]
        {
            new Coordinate(X - 1, Y),
            new Coordinate(X + 1, Y),
            new Coordinate(X, Y - 1),
            new Coordinate(X, Y + 1),
        };
}

public class Solver : Solver<int[][], long>
{
    protected override string InputPath => "Day15/input.txt";

    public static int[][] GetExtendedMap(int[][] map, int magnitude = 5)
    {
        // Scale up the map
        var extendedMap = new int[map.Length * magnitude][];

        for (var x = 0; x < extendedMap.Length; ++x)
        {
            extendedMap[x] = new int[map.Length * magnitude];
        }

        // Populate its values
        for (var x = 0; x < extendedMap.Length; ++x)
        {
            for (var y = 0; y < extendedMap[x].Length; ++y)
            {
                var seed = map[x % map.Length][y % map.Length];
                var distanceFromSeed = x / map.Length + y / map.Length;

                extendedMap[x][y] = (seed + distanceFromSeed - 1) % 9 + 1;
            }
        }

        return extendedMap;
    }

    private static long GetShortestPathCost(int[][] map, Coordinate source, Coordinate target)
    {
        // Initialize the costs map
        var cost = new Dictionary<Coordinate, long>(
            from y in Enumerable.Range(0, map.Length)
            from x in Enumerable.Range(0, map[0].Length)
            select new KeyValuePair<Coordinate, long>(new Coordinate(x, y), long.MaxValue))
        { [source] = 0 };

        // Keep track of the opened nodes, with the less costly ones at the top
        var opened = new PriorityQueue<Coordinate, long>();
        opened.Enqueue(source, 0);

        while (opened.Count > 0)
        {
            // Take the less costly node of the opened ones
            var current = opened.Dequeue();

            // If the goal is reached, return the total cost
            if (current == target) return cost[target];

            // Add all the viable neighbors to the opened nodes
            current.GetNeighbors()
                .Where(neighbor => cost.ContainsKey(neighbor)
                    && cost[neighbor] > cost[current] + map[neighbor.X][neighbor.Y])
                .ToList()
                .ForEach(neighbor =>
                {
                    var costToNeighbor = cost[current] + map[neighbor.X][neighbor.Y];

                    cost[neighbor] = costToNeighbor;
                    opened.Enqueue(neighbor, costToNeighbor);
                });
        }

        return -1;
    }

    public override long PartOne(int[][] input)
    {
        var start = new Coordinate(0, 0);
        var goal = new Coordinate(input.Length - 1, input.Length - 1);

        return GetShortestPathCost(input, start, goal);
    }

    public override long PartTwo(int[][] input)
    {
        // Multiply the map in X and Y
        var extended = GetExtendedMap(input);

        var start = new Coordinate(0, 0);
        var goal = new Coordinate(extended.Length - 1, extended.Length - 1);

        // Compute the shortest path
        return GetShortestPathCost(extended, start, goal);
    }

    public override int[][] ReadInput(string inputPath)
        => File.ReadLines(inputPath)
            .Select(line => line.Select(x => x - '0').ToArray())
            .ToArray();
}
