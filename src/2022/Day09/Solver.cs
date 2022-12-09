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
    public static Coordinate Origin = new Coordinate(0, 0);

    public Coordinate MovedToThe(Direction direction)
        => direction switch
        {
            Direction.Right => this with { X = X + 1 },
            Direction.Left => this with { X = X - 1 },
            Direction.Up => this with { Y = Y + 1 },
            Direction.Down => this with { Y = Y - 1 },
            _ => throw new UnreachableException(),
        };
}

public record Rope
{
    private readonly HashSet<Coordinate> _visitedFromTail = new() { Coordinate.Origin };

    public Coordinate Head { get; private set; } = Coordinate.Origin;

    public Coordinate Tail { get; private set; } = Coordinate.Origin;

    public int VisitedCoordintatesFromTailCount => _visitedFromTail.Count;

    private IEnumerable<Direction> TailShouldFollow()
    {
        var directions = new List<Direction>();

        var distanceToX = Head.X - Tail.X;
        var distanceToY = Head.Y - Tail.Y;

        if (distanceToX < -1)
        {
            directions.Add(Direction.Left);
            
            if (distanceToY < 0) directions.Add(Direction.Down);
            if (distanceToY > 0) directions.Add(Direction.Up);
        }

        if (distanceToX > 1)
        {
            directions.Add(Direction.Right);

            if (distanceToY < 0) directions.Add(Direction.Down);
            if (distanceToY > 0) directions.Add(Direction.Up);
        }

        if (distanceToY < -1)
        {
            directions.Add(Direction.Down);

            if (distanceToX < 0) directions.Add(Direction.Left);
            if (distanceToX > 0) directions.Add(Direction.Right);
        }

        if (distanceToY > 1)
        {
            directions.Add(Direction.Up);

            if (distanceToX < 0) directions.Add(Direction.Left);
            if (distanceToX > 0) directions.Add(Direction.Right);
        }

        return directions;
    }

    public void Apply(Motion motion)
    {
        for (var i = 0; i < motion.Times; ++i)
        {
            Head = Head.MovedToThe(motion.Direction);

            var directions = TailShouldFollow();

            foreach (var direction in directions)
            {
                Tail = Tail.MovedToThe(direction);
            }

            _visitedFromTail.Add(Tail);
        }
    }
}

public class Solver : Solver<IEnumerable<Motion>, int>
{
    public Solver() : base("Day09/input.txt") { }

    public override int PartOne(IEnumerable<Motion> input)
    {
        var rope = new Rope();

        foreach (var motion in input)
        {
            rope.Apply(motion);
        }

        return rope.VisitedCoordintatesFromTailCount;
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
