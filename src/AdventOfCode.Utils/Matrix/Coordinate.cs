namespace AdventOfCode.Utils.Matrix;

[Flags]
public enum Direction
{
    Right = 1,
    Left = 2,
    Up = 4,
    Down = 8,
    UpRight = Up | Right,
    UpLeft = Up | Left,
    DownRight = Down | Right,
    DownLeft = Down | Left,
}

public record Coordinate(int X, int Y)
{
    public Coordinate[] Neighbors = [
        new Coordinate(X - 1, Y),
        new Coordinate(X + 1, Y),
        new Coordinate(X, Y - 1),
        new Coordinate(X, Y + 1),
    ];

    public Coordinate MovedToThe(Direction direction)
        => direction switch
        {
            // Cardinal
            Direction.Right => this with { X = X + 1 },
            Direction.Left => this with { X = X - 1 },
            Direction.Up => this with { Y = Y + 1 },
            Direction.Down => this with { Y = Y - 1 },

            // Diagonals
            Direction.UpRight => this with { X = X + 1, Y = Y + 1 },
            Direction.UpLeft => this with { X = X - 1, Y = Y + 1 },
            Direction.DownRight => this with { X = X + 1, Y = Y - 1 },
            Direction.DownLeft => this with { X = X - 1, Y = Y - 1 },

            _ => throw new Exception($"Unknown direction {direction}")
        };

    public bool IsAdjascentTo(Coordinate coordinate)
    {
        int deltaX = Math.Abs(coordinate.X - X);
        int deltaY = Math.Abs(coordinate.Y - Y);

        return deltaX <= 1 && deltaY <= 1;
    }

    public override string ToString()
        => $"X: {X}, Y: {Y}";
}
