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

    public Coordinate MovedToThe(Direction direction)
        => direction switch
        {
            Direction.Right => this with { X = X + 1 },
            Direction.Left => this with { X = X - 1 },
            Direction.Up => this with { Y = Y + 1 },
            Direction.Down => this with { Y = Y - 1 },
            _ => throw new UnreachableException(),
        };

    public IEnumerable<Direction> MovementsToReach(Coordinate target)
    {
        var directions = new List<Direction>();

        var distanceTo = new
        {
            X = target.X - X,
            Y = target.Y - Y,
        };

        if (distanceTo.X < -1)
        {
            directions.Add(Direction.Left);

            if (distanceTo.Y < 0) directions.Add(Direction.Down);
            if (distanceTo.Y > 0) directions.Add(Direction.Up);

            return directions;
        }

        if (distanceTo.X > 1)
        {
            directions.Add(Direction.Right);

            if (distanceTo.Y < 0) directions.Add(Direction.Down);
            if (distanceTo.Y > 0) directions.Add(Direction.Up);

            return directions;
        }

        if (distanceTo.Y < -1)
        {
            directions.Add(Direction.Down);

            if (distanceTo.X < 0) directions.Add(Direction.Left);
            if (distanceTo.X > 0) directions.Add(Direction.Right);

            return directions;
        }

        if (distanceTo.Y > 1)
        {
            directions.Add(Direction.Up);

            if (distanceTo.X < 0) directions.Add(Direction.Left);
            if (distanceTo.X > 0) directions.Add(Direction.Right);

            return directions;
        }

        return directions;
    }
}

public class Knot
{
    private readonly HashSet<Coordinate> _visited = new() { Coordinate.Origin };

    public Coordinate Coordinate { get; private set; } = Coordinate.Origin;

    public int VisitedCoordinatesCount => _visited.Count;

    public IEnumerable<Direction> MovementsToReach(Knot target)
        => Coordinate.MovementsToReach(target.Coordinate);

    public void MoveTo(IEnumerable<Direction> directions)
    {
        foreach (var direction in directions)
        {
            Coordinate = Coordinate.MovedToThe(direction);
        }

        _visited.Add(Coordinate);
    }
}

public class Rope
{
    private readonly Knot[] _knots;

    public Knot Head => _knots[0];

    public Knot Tail => _knots[^1];

    public Rope(int knotsCount)
        => _knots = Enumerable.Range(0, knotsCount)
            .Select(_ => new Knot())
            .ToArray();

    public void Apply(IEnumerable<Motion> motions)
        => motions.ToList().ForEach(Apply);

    public void Apply(Motion motion)
    {
        for (var i = 0; i < motion.Times; ++i)
        {
            Head.MoveTo(new[] { motion.Direction });
            ApplyInertia();
        }
    }

    private void ApplyInertia()
    {
        for (var j = 1; j < _knots.Length; ++j)
        {
            var target = _knots[j - 1];
            var source = _knots[j];

            var directions = source.MovementsToReach(target);

            source.MoveTo(directions);
        }
    }
}

public class Solver : Solver<IEnumerable<Motion>, int>
{
    public Solver() : base("Day09/input.txt") { }

    public override int PartOne(IEnumerable<Motion> input)
    {
        var rope = new Rope(knotsCount: 2);
        rope.Apply(input);

        return rope.Tail.VisitedCoordinatesCount;
    }

    public override int PartTwo(IEnumerable<Motion> input)
    {
        var rope = new Rope(knotsCount: 10);
        rope.Apply(input);

        return rope.Tail.VisitedCoordinatesCount;
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
