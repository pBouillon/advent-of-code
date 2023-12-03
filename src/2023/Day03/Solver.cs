using AdventOfCode.Utils.Matrix;

namespace _2023.Day03;

public class Solver : Solver<EngineSchematic, long>
{
    public Solver() : base("Day03/input.txt") { }

    public override long PartOne(EngineSchematic engine)
        => engine.Parts.Sum(part => part.Value);

    public override long PartTwo(EngineSchematic engine)
        => engine.ComputeGears().Sum(gear => gear.Ratio);

    public override EngineSchematic ParseInput(IEnumerable<string> input)
    {
        var symbols = new List<Symbol>();

        var rows = input.ToArray();

        // Symbols Lookup
        for (var y = 0; y < rows.Length; ++y)
        {
            for (var x = 0; x < rows[y].Length; ++x)
            {
                var value = rows[y][x];

                if (!EngineSchematic.IsSymbol(value)) continue;

                var coordinate = new Coordinate(x, y);
                symbols.Add(new Symbol(coordinate, value));
            }
        }

        var engine = new EngineSchematic(symbols);

        // Engine Parts Lookup
        Coordinate? partStart;
        string? buffer;

        void Reset()
        {
            buffer = null;
            partStart = null;
        }

        for (var y = 0; y < rows.Length; ++y)
        {
            Reset();

            for (var x = 0; x < rows[y].Length; ++x)
            {
                var value = rows[y][x];

                // Reading an engine part
                if (EngineSchematic.MightBePart(value))
                {
                    partStart ??= new Coordinate(x, y);
                    buffer += value;
                    continue;
                }

                // Finished reading an engine part
                if (partStart is not null)
                {
                    // Create the part
                    var part = new Part(
                        From: partStart,
                        Value: long.Parse(buffer!));

                    engine.EvaluateAndAdd(part);

                    Reset();
                }
            }
        }

        return engine;
    }
}

public record Symbol(Coordinate Coordinate, char Value);

public record Part(Coordinate From, long Value)
{
    public Coordinate[] Trail => Enumerable.Range(0, Value.ToString().Length)
            .Select(offset => From with { X = From.X + offset })
            .ToArray();

    public bool IsNextToAny(List<Coordinate> coordinates)
        => Trail.Any(coordinate => coordinates.Any(coordinate.IsAdjascentTo));
}

public record Gear(Coordinate Coordinate, Part PartOne, Part PartTwo)
{
    public long Ratio = PartOne.Value * PartTwo.Value;
}

public class EngineSchematic(List<Symbol> Symbols)
{
    public List<Part> Parts { get; init; } = new();

    public static bool MightBePart(char @char)
        => char.IsDigit(@char);

    public static bool IsSymbol(char @char)
        => !char.IsDigit(@char) && @char != '.';

    public void EvaluateAndAdd(Part part)
    {
        var symbolCoordinates = Symbols
            .Select(symbol => symbol.Coordinate)
            .ToList();

        var isNextToSymbol = part.IsNextToAny(symbolCoordinates);
        if (isNextToSymbol)
        {
            Parts.Add(part);
        }
    }

    public Gear[] ComputeGears()
        => Symbols
            .Where(symbol => symbol.Value == '*')
            .Select(gear => new
            {
                Symbol = gear,
                Parts = Parts
                    .Where(part => part.IsNextToAny([gear.Coordinate]))
                    .ToArray(),
            })
            .Where(candidate => candidate.Parts.Length == 2)
            .Select(candidate => new Gear(
                candidate.Symbol.Coordinate,
                candidate.Parts[0],
                candidate.Parts[1]))
            .ToArray();
}
