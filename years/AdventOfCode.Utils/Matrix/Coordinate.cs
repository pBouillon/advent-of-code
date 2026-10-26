namespace AdventOfCode.Utils.Matrix;

/// <summary>
/// A direction on a grid. Diagonals combine two cardinal directions, and Y
/// grows downward like the rows of the puzzle input.
/// </summary>
[Flags]
public enum Direction
{
    /// <summary>
    /// Towards increasing X.
    /// </summary>
    Right = 1,

    /// <summary>
    /// Towards decreasing X.
    /// </summary>
    Left = 2,

    /// <summary>
    /// Towards the previous row, decreasing Y.
    /// </summary>
    Up = 4,

    /// <summary>
    /// Towards the next row, increasing Y.
    /// </summary>
    Down = 8,

    /// <summary>
    /// Up and right at once.
    /// </summary>
    UpRight = Up | Right,

    /// <summary>
    /// Up and left at once.
    /// </summary>
    UpLeft = Up | Left,

    /// <summary>
    /// Down and right at once.
    /// </summary>
    DownRight = Down | Right,

    /// <summary>
    /// Down and left at once.
    /// </summary>
    DownLeft = Down | Left,
}

/// <summary>
/// A position on a grid. X grows to the right and Y grows downward, like the
/// columns and rows of the puzzle input.
/// </summary>
/// <param name="X">The column of the position</param>
/// <param name="Y">The row of the position</param>
public readonly record struct Coordinate(int X, int Y)
{
    /// <summary>
    /// The four diagonal neighbors of this coordinate.
    /// A new array is created on each access.
    /// </summary>
    public Coordinate[] DiagonalNeighbors => [
        new Coordinate(X - 1, Y - 1),
        new Coordinate(X - 1, Y + 1),
        new Coordinate(X + 1, Y - 1),
        new Coordinate(X + 1, Y + 1),
    ];

    /// <summary>
    /// The four orthogonal neighbors of this coordinate.
    /// A new array is created on each access.
    /// </summary>
    public Coordinate[] Neighbors => [
        new Coordinate(X - 1, Y),
        new Coordinate(X + 1, Y),
        new Coordinate(X, Y - 1),
        new Coordinate(X, Y + 1),
    ];

    /// <summary>
    /// Returns the coordinate reached by moving one step in a direction.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The direction is not one of the supported cardinals or diagonals
    /// </exception>
    public Coordinate MovedToThe(Direction direction)
        => direction switch
        {
            // Cardinal
            Direction.Right => this with { X = X + 1 },
            Direction.Left => this with { X = X - 1 },
            Direction.Up => this with { Y = Y - 1 },
            Direction.Down => this with { Y = Y + 1 },

            // Diagonals
            Direction.UpRight => this with { X = X + 1, Y = Y - 1 },
            Direction.UpLeft => this with { X = X - 1, Y = Y - 1 },
            Direction.DownRight => this with { X = X + 1, Y = Y + 1 },
            Direction.DownLeft => this with { X = X - 1, Y = Y + 1 },

            _ => throw new ArgumentOutOfRangeException(
                nameof(direction), direction, "Unknown direction"),
        };

    /// <summary>
    /// Tells whether a coordinate is one of the eight cells around this one.
    /// A coordinate is not adjacent to itself.
    /// </summary>
    public bool IsAdjacentTo(Coordinate coordinate)
        => this != coordinate
            && Math.Abs(coordinate.X - X) <= 1
            && Math.Abs(coordinate.Y - Y) <= 1;

    /// <summary>
    /// Returns the Manhattan distance between this coordinate and another.
    /// </summary>
    public long ManhattanDistanceTo(Coordinate coordinate)
        => Math.Abs((long)coordinate.X - X) + Math.Abs((long)coordinate.Y - Y);

    public override string ToString()
        => $"({X}, {Y})";
}
