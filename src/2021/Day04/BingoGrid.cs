namespace _2021.Day04;

public class BingoGrid
{
    private readonly bool[][] _drawn = Enumerable
        .Range(0, 5)
        .Select(_ => new bool[5])
        .ToArray();

    private readonly int[][] _grid;

    public BingoGrid(int[][] grid)
        => _grid = grid;

    public void MarkDrawn(int drawn)
    {
        for (var i = 0; i < _grid.Length; ++i)
        {
            for (var j = 0; j < _grid.Length; ++j)
            {
                var number = _grid[i][j];
                if (number == drawn) _drawn[i][j] = true;
            }
        }
    }

    public IEnumerable<int> RemainingNumbers()
    {
        var remainingNumbers = new List<int>();

        for (var i = 0; i < _drawn.Length; ++i)
        {
            for (var j = 0; j < _drawn.Length; ++j)
            {
                if (!_drawn[i][j]) remainingNumbers.Add(_grid[i][j]);
            }
        }

        return remainingNumbers;
    }

    public bool IsWinning()
    {
        if (_drawn.Any(row => row.All(drawn => drawn))) return true;

        for (var i = 0; i < _drawn.Length; ++i)
        {
            if (IsWinningColumn(i)) return true;
        }

        return false;
    }

    private bool IsWinningColumn(int column)
        => _drawn.All(t => t[column]);
}
