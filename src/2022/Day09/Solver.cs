using System.Diagnostics;

namespace _2022.Day09;

public enum Direction
{
    Right, 
    Left, 
    Up, 
    Down,
}

public record Motion(Direction Direction, int Times);

public record Coordinate(int X, int Y)
{
    public static readonly Coordinate Origin = new(0, 0);
}

public record Knot(Coordinate Coordinate);

public record Rope
{
    public Knot Head { get; } = new Knot(Coordinate.Origin);
    public Knot Tail { get; } = new Knot(Coordinate.Origin);
}

public class Solver : Solver<IEnumerable<Motion>, int>
{
    public Solver() : base("Day0/input.txt") { }

    public override int PartOne(IEnumerable<Motion> input)
    {
        throw new NotImplementedException();
    }

    public override int PartTwo(IEnumerable<Motion> input)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Motion> ParseInput(IEnumerable<string> input)
        => input.Select(motion =>
        {
            var row = motion.Split(' ');

            var direction = row[0] switch
            {
                "R" => Direction.Right,
                "L" => Direction.Left,
                "U" => Direction.Up,
                "D" => Direction.Down,
                _ => throw new UnreachableException(),
            };

            var times = int.Parse(row[1]);

            return new Motion(direction, times);
        });
}
