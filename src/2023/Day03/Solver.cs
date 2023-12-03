namespace _2023.Day03;

public class Solver : Solver<EngineSchematic, long>
{
    public Solver() : base("Day03/input.txt") { }

    public override long PartOne(EngineSchematic input)
        => input.PartNumbers.Sum();

    public override long PartTwo(EngineSchematic input)
    {
        throw new NotImplementedException();
    }

    public override EngineSchematic ParseInput(IEnumerable<string> input)
    {
        var symbols = new List<Coordinate>();

        var rows = input.ToArray();

        // Symbols Lookup
        for (var y = 0; y < rows.Length; ++y)
        {
            for (var x = 0; x < rows[y].Length; ++x)
            {
                var value = rows[y][x];

                if (!EngineSchematic.IsSymbol(value)) continue;

                var coordinate = new Coordinate(x, y);
                symbols.Add(coordinate);
            }
        }

        var engine = new EngineSchematic(symbols);

        // Engine Parts Lookup
        Coordinate? partStart;
        var buffer = string.Empty;

        void Reset()
        {
            buffer = string.Empty;
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
                        Length: buffer.Length,
                        Value: long.Parse(buffer));

                    engine.EvaluateAndAdd(part);

                    Reset();
                }
            }
        }

        return engine;
    }
}

public record Coordinate(int X, int Y)
{
    public bool IsAdjascentTo(Coordinate coordinate)
    {
        int deltaX = Math.Abs(coordinate.X - X);
        int deltaY = Math.Abs(coordinate.Y - Y);

        return deltaX <= 1 && deltaY <= 1;
    }
}


public record Part(Coordinate From, int Length, long Value)
{
    public bool IsNextToAny(List<Coordinate> coordinates)
        => Enumerable.Range(0, Length)
            .Select(offset => From with { X = From.X + offset })
            .Any(coordinate => coordinates.Any(coordinate.IsAdjascentTo));
}

public class EngineSchematic(List<Coordinate> SymbolCoordinates)
{
    public List<long> PartNumbers { get; init; } = new();

    public static bool MightBePart(char @char)
        => char.IsDigit(@char);

    public static bool IsSymbol(char @char)
        => !char.IsDigit(@char) && @char != '.';

    public void EvaluateAndAdd(Part part)
    {
        var isNextToSymbol = part.IsNextToAny(SymbolCoordinates);
        if (isNextToSymbol)
        {
            PartNumbers.Add(part.Value);
        }
    }
}
