namespace _2015.Day03;

public record Coordinate(int X, int Y);

public class Solver : Solver<string, int>
{
    protected override string InputPath => "Day03/input.txt";

    private static HashSet<Coordinate> GetVisitedHouses(string input)
    {
        var currentCoordinate = new Coordinate(0, 0);
        var visited = new HashSet<Coordinate> { currentCoordinate };

        foreach (var move in input)
        {
            currentCoordinate = move switch
            {
                '^' => currentCoordinate with { X = currentCoordinate.X + 1 },
                'v' => currentCoordinate with { X = currentCoordinate.X - 1 },
                '>' => currentCoordinate with { Y = currentCoordinate.Y + 1 },
                '<' => currentCoordinate with { Y = currentCoordinate.Y - 1 },
                _ => throw new Exception($"Unexpected move '{move}'"),
            };

            visited.Add(currentCoordinate);
        }

        return visited;
    }

    public override int PartOne(string input)
        => GetVisitedHouses(input).Count;

    public override int PartTwo(string input)
    {
        var santaPath = string.Join(string.Empty, input
            .ToCharArray()
            .Where((direction, index) => index % 2 != 0));

        var robotPath = string.Join(string.Empty, input
            .ToCharArray()
            .Where((direction, index) => index % 2 == 0));

        var visitedHouses = GetVisitedHouses(santaPath);
        visitedHouses.UnionWith(GetVisitedHouses(robotPath));

        return visitedHouses.Count;
    }

    public override string ReadInput(string inputPath)
        => File
            .ReadLines(inputPath)
            .FirstOrDefault() ?? string.Empty;
}
