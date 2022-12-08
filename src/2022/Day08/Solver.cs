namespace _2022.Day08;

public static class MatrixExtensions
{
    public static int[] GetColumn(this int[,] matrix, int columnNumber)
        => Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();

    public static int[] GetRow(this int[,] matrix, int rowNumber)
        => Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();

    public static bool AreSmallerThan(this IEnumerable<int> trees, int current)
        => trees.All(tree => tree < current);
}

public class Solver : Solver<int[,], int>
{
    public Solver() : base("Day08/input.txt") { }

    public override int PartOne(int[,] input)
    {
        var visibleTreesCount = 0;

        var forestWidth = input.GetLength(0);
        for (var i = 0; i < forestWidth; ++i)
        {
            var isOnEdge = i == 0 || i == forestWidth - 1;

            if (isOnEdge)
            {
                visibleTreesCount += forestWidth;
                continue;
            }

            for (var j = 0; j < forestWidth; ++j)
            {
                isOnEdge = j == 0 || j == forestWidth - 1;

                if (isOnEdge)
                {
                    ++visibleTreesCount;
                    continue;
                }
                
                var column = input.GetColumn(j);
                var row = input.GetRow(i);

                var trees = new
                {
                    OnTop = column[..i],
                    OnBottom = column[(i + 1)..],
                    OnRight = row[(j + 1)..],
                    OnLeft = row[..j],
                };
                
                var tree = input[i, j];

                var intVisible = trees.OnTop.AreSmallerThan(tree)
                    || trees.OnRight.AreSmallerThan(tree)
                    || trees.OnBottom.AreSmallerThan(tree)
                    || trees.OnLeft.AreSmallerThan(tree);

                if (intVisible)
                {
                    ++visibleTreesCount;
                    continue;
                }
            }
        }

        return visibleTreesCount;
    }

    public override int PartTwo(int[,] input)
    {
        throw new NotImplementedException();
    }

    public override int[,] ParseInput(IEnumerable<string> input)
    {
        var forest = input
            .Select(row => row.ToCharArray()
                .Select(x => int.Parse(x.ToString()))
                .ToArray())
            .ToArray();

        var length = forest.Length;
        var treeHeights = new int[length, length];

        for (var i = 0; i < length; ++i)
        {
            for (var j = 0; j < length; ++j)
            {
                treeHeights[i, j] = forest[i][j];
            }
        }

        return treeHeights;
    }
}
