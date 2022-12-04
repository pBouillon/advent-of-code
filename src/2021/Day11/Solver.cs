namespace _2021.Day11;

public record Coordinate(int X, int Y)
{
    public IEnumerable<Coordinate> GetNeighbors()
        => from i in new[] { -1, 0, 1 }
           from j in new[] { -1, 0, 1 }
           where i != 0 || j != 0
           select new Coordinate(X + i, Y + j);
}

public class Solver : Solver<Dictionary<Coordinate, int>, int>
{
    protected override string InputPath => "Day11/input.txt";

    public override int PartOne(Dictionary<Coordinate, int> input)
    {
        var flashes = 0;
        var toFlash = new Queue<Coordinate>();
        var hasFlashed = new Queue<Coordinate>();

        for (var step = 0; step < 100; ++step)
        {
            // Increase energy
            foreach (var coordinate in input.Keys)
            {
                ++input[coordinate];
                if (input[coordinate] > 9) toFlash.Enqueue(coordinate);
            }

            // Flash
            while (toFlash.Any())
            {
                var flashed = toFlash.Dequeue();
                if (hasFlashed.Contains(flashed)) continue;

                hasFlashed.Enqueue(flashed);
                ++flashes;

                flashed
                    .GetNeighbors()
                    .Where(input.ContainsKey)
                    .ToList()
                    .ForEach(neighbor =>
                    {
                        ++input[neighbor];
                        if (input[neighbor] > 9) toFlash.Enqueue(neighbor);
                    });
            }

            // Reset the flashing octopuses
            while (hasFlashed.Any()) input[hasFlashed.Dequeue()] = 0;
            hasFlashed.Clear();
        }

        return flashes;
    }

    public override int PartTwo(Dictionary<Coordinate, int> input)
    {
        var toFlash = new Queue<Coordinate>();
        var hasFlashed = new Queue<Coordinate>();

        for (var step = 0; ; ++step)
        {
            // Increase energy
            foreach (var coordinate in input.Keys)
            {
                ++input[coordinate];
                if (input[coordinate] > 9) toFlash.Enqueue(coordinate);
            }

            // Flash
            while (toFlash.Any())
            {
                var flashed = toFlash.Dequeue();
                if (hasFlashed.Contains(flashed)) continue;

                hasFlashed.Enqueue(flashed);

                flashed
                    .GetNeighbors()
                    .Where(input.ContainsKey)
                    .ToList()
                    .ForEach(neighbor =>
                    {
                        ++input[neighbor];
                        if (input[neighbor] > 9) toFlash.Enqueue(neighbor);
                    });
            }

            // Reset the flashing octopuses
            if (hasFlashed.Count == input.Keys.Count) return step + 1;
            while (hasFlashed.Any()) input[hasFlashed.Dequeue()] = 0;
        }
    }

    public override Dictionary<Coordinate, int> ReadInput(string inputPath)
    {
        var octopuses = File
            .ReadLines(inputPath)
            .ToArray();

        return new Dictionary<Coordinate, int>(Enumerable.Range(0, 10)
            .SelectMany(
                _ => Enumerable.Range(0, 10),
                (y, x) => new KeyValuePair<Coordinate, int>(
                    new Coordinate(x, y),
                    octopuses[y][x] - '0')));
    }
}
