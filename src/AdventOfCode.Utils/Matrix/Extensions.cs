using System.Collections.ObjectModel;

namespace AdventOfCode.Utils.Matrix;

public static class Extensions
{
    public static void TraverseMatrix(
        this ReadOnlyDictionary<Coordinate, char> matrix,
        Action<Coordinate, char>? onCell = null,
        Action? onNewRow = null)
    {
        var orderedRows = matrix.Keys
            .OrderBy(c => c.Y)
            .ThenBy(c => c.X)
            .GroupBy(c => c.Y);

        foreach (var row in orderedRows)
        {
            onNewRow?.Invoke();

            foreach (var coordinate in row)
            {
                var value = matrix[coordinate];
                onCell?.Invoke(coordinate, value);
            }
        }
    }

    /// <summary>
    /// Parses a matrix from a collection of strings
    /// </summary>
    /// <example>
    /// The following example demonstrates the usage of the ParseMatrix method
    /// <code>
    /// var inputMatrix = new List<string>
    /// {
    ///     "abc",
    ///     "def"
    /// };
    /// 
    /// var parsedMatrix = inputMatrix.ParseMatrix(
    ///     onCell: (coordinate, value) =>
    ///     {
    ///         Console.WriteLine($"[{coordinate.X}, {coordinate.Y}]: {value}");
    ///     },
    ///     onNewRow: () =>
    ///     {
    ///         Console.WriteLine("New row encountered");
    ///     });
    /// 
    /// // The output will be:
    /// // New row encountered
    /// // [0, 0]: a
    /// // [1, 0]: b
    /// // [2, 0]: c
    /// // New row encountered
    /// // [0, 1]: d
    /// // [1, 1]: e
    /// // [2, 1]: f
    /// </code>
    /// </example>
    /// <param name="source">
    /// The collection of strings representing the matrix
    /// </param>
    /// <param name="onCell">
    /// The action to be performed on each cell of the matrix
    /// </param>
    /// <param name="onNewRow">
    /// The optional action to be performed when a new row is encountered
    /// </param>
    /// <returns>
    /// The resulting matrix
    /// </returns>
    public static ReadOnlyDictionary<Coordinate, char> ParseMatrix(
        this IEnumerable<string> source,
        Action<Coordinate, char>? onCell = null,
        Action? onNewRow = null)
    {
        var matrix = new Dictionary<Coordinate, char>();

        var rows = source.ToArray();

        for (var y = 0; y < rows.Length; ++y)
        {
            onNewRow?.Invoke();

            for (var x = 0; x < rows[y].Length; ++x)
            {
                var coordinate = new Coordinate(x, y);
                var value = rows[y][x];

                matrix[coordinate] = value;

                onCell?.Invoke(coordinate, value);
            }
        }

        return matrix.AsReadOnly();
    }
}
