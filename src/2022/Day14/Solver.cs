using System.Runtime.CompilerServices;
using System.Text;

namespace _2022.Day14;

public record Coordinate(int Depth, int Column)
{
    public static Coordinate From(string raw)
    {
        var axes = raw.Split(',')
            .Select(int.Parse)
            .ToArray();

        return new Coordinate(axes[1], axes[0]);
    }
}

public enum Material { Air, Rock, Sand }

public class Cave
{
    private int Depth { get; init; }
    private int MinWidth { get; init; }
    private int MaxWidth { get; init; }

    private readonly Material[,] _cave;

    private readonly int _floorOffset = 1_000;
    private readonly Material[,] _flooredCave;

    private int Width => MaxWidth - MinWidth;

    public Cave(int depth, int minWidth, int maxWidth)
    {
        (Depth, MinWidth, MaxWidth) = (depth + 1, minWidth, maxWidth + 1);

        _cave = new Material[Depth, Width];
        for (var i = 0; i < Depth * Width; ++i)
        {
            _cave[i % Depth, i / Depth] = Material.Air;
        }

        var floorWidth = _floorOffset + Width + _floorOffset;
        var floorDepth = Depth + 2;
        _flooredCave = new Material[floorDepth, floorWidth];
        for (var i = 0; i < Depth * floorWidth; ++i)
        {
            _flooredCave[i % floorDepth, i / floorDepth] = i % floorDepth == floorDepth - 1
                ? Material.Rock
                : Material.Air;
        }
    }

    public void AddRock(Coordinate from, Coordinate to)
    {
        // Floored cave
        var delta = new
        {
            Column = from.Column == to.Column ? 0 : from.Column < to.Column ? 1 : -1,
            Depth = from.Depth == to.Depth ? 0 : from.Depth < to.Depth ? 1 : -1,
        };

        for (
            var (depth, column) = from;
            depth - delta.Depth != to.Depth || column - delta.Column != to.Column;
            (depth, column) = (depth + delta.Depth, column + delta.Column))
        {
            _flooredCave[depth, _floorOffset + column] = Material.Rock;
        }

        // Abyssimal cave
        from = @from with { Column = from.Column - MinWidth };
        to = to with { Column = to.Column - MinWidth };

        for (
            var (depth, column) = from;
            depth - delta.Depth != to.Depth || column - delta.Column != to.Column;
            (depth, column) = (depth + delta.Depth, column + delta.Column))
        {
            _cave[depth, column] = Material.Rock;
        }
    }

    public bool DropSandToTheVoid()
    {
        var grainCoordinate = new Coordinate(0, 500 - MinWidth);

        bool hasSettled;
        bool isIntoTheAbyss;

        do
        {
            var next = GetNextCoordinateOfGrainOn(grainCoordinate!);

            hasSettled = grainCoordinate == next;
            isIntoTheAbyss = next is null;

            grainCoordinate = next;
        } while (!hasSettled && !isIntoTheAbyss);

        if (grainCoordinate is not null)
        {
            var (depth, column) = grainCoordinate;
            _cave[depth, column] = Material.Sand;
        }

        return isIntoTheAbyss;
    }

    public bool DropSandUntilReachingTheSource()
    {
        var source = new Coordinate(0, _floorOffset + 500);
        var grainCoordinate = source;

        bool hasSettled;
        bool hasReachedTheSource;

        do
        {
            var next = GetNextCoordinateOfGrainOn(grainCoordinate!, floored: true);

            hasSettled = grainCoordinate == next;
            hasReachedTheSource = next == source;

            grainCoordinate = next;
        } while (!hasSettled && !hasReachedTheSource);

        var (depth, column) = grainCoordinate;
        _flooredCave[depth, column] = Material.Sand;

        return hasReachedTheSource;
    }

    private Coordinate? GetNextCoordinateOfGrainOn(Coordinate initialCoordinate, bool floored = false)
    {
        // A unit of sand always falls down one step if possible
        var bellow = initialCoordinate with { Depth = initialCoordinate.Depth + 1 };
        if (!IsBlocked(bellow)) return bellow;

        // The unit of sand attempts to instead move diagonally one step down and to the left
        var downLeft = bellow with { Column = bellow.Column - 1 };

        if (IsIntoTheAbyss(downLeft)) return null;
        if (!IsBlocked(downLeft)) return downLeft;

        // If that tile is blocked, the unit of sand attempts to instead move diagonally one step
        // down and to the right
        var downRight = bellow with { Column = bellow.Column + 1 };
        
        if (IsIntoTheAbyss(downRight)) return null;

        return IsBlocked(downRight)
            ? initialCoordinate
            : downRight;

        bool IsBlocked(Coordinate coordinate)
            => floored
                ? _flooredCave[coordinate.Depth, coordinate.Column] != Material.Air
                : _cave[coordinate.Depth, coordinate.Column] != Material.Air;

        bool IsIntoTheAbyss(Coordinate coordinate) 
            => coordinate.Depth > (floored ? Depth + 2 : Depth)
            || coordinate.Column < 0 
            || coordinate.Column >= (floored
                ? _floorOffset + Width + _floorOffset
                : Width);
    }

    public override string ToString()
    {
        var visualization = new StringBuilder();

        for (int i = 0; i < _cave.GetLength(0); i++)
        {
            for (int j = 0; j < _cave.GetLength(1); j++)
            {
                var symbol = _cave[i, j] switch
                {
                    Material.Air => '.',
                    Material.Rock => '#',
                    Material.Sand => 'o',
                    _ => '?',
                };

                visualization.Append(symbol);
            }

            visualization.AppendLine();
        }

        visualization.AppendLine();
        visualization.AppendLine();
        visualization.AppendLine();

        for (int i = 0; i < _flooredCave.GetLength(0); i++)
        {
            for (int j = 0; j < _flooredCave.GetLength(1); j++)
            {
                var symbol = _flooredCave[i, j] switch
                {
                    Material.Air => '.',
                    Material.Rock => '#',
                    Material.Sand => 'o',
                    _ => '?',
                };

                visualization.Append(symbol);
            }

            visualization.AppendLine();
        }

        return visualization.ToString();
    }
}

public class Solver : Solver<Cave, int>
{
    public Solver() : base("Day14/input.txt") { }

    public override int PartOne(Cave input)
    {
        var counter = 0;

        while (!input.DropSandToTheVoid()) ++counter;

        return counter;
    }

    public override int PartTwo(Cave input)
    {
        var counter = 1;

        while (!input.DropSandUntilReachingTheSource()) ++counter;

        return counter;
    }

    public override Cave ParseInput(IEnumerable<string> input)
    {
        var caveDepth = int.MinValue;
        var caveMinWidth = int.MaxValue;
        var caveMaxWidth = int.MinValue;

        var rockLines = new List<(Coordinate, Coordinate)>();

        // Extract the rock lines and compute the cave dimensions
        foreach (var line in input)
        {
            var coordinates = line.Split(" -> ")
                .Select(Coordinate.From)
                .ToArray();

            for (var i = 1; i < coordinates.Length; ++i)
            {
                var from = coordinates[i - 1];
                var to = coordinates[i];

                // Update the dimensions
                caveDepth = int.Max(
                    caveDepth,
                    int.Max(from.Depth, to.Depth));

                caveMinWidth = int.Min(
                    caveMinWidth,
                    int.Min(from.Column, to.Column));

                caveMaxWidth = int.Max(
                    caveMaxWidth,
                    int.Max(from.Column, to.Column));

                // Add the line
                rockLines.Add((from, to));
            }
        }

        // Create the cave
        var cave = new Cave(caveDepth, caveMinWidth, caveMaxWidth);

        // Draw the rock lines
        foreach (var (from, to) in rockLines)
        {
            cave.AddRock(from, to);
        }

        return cave;
    }
}
