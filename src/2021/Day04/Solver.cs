using System.Text.RegularExpressions;

namespace _2021.Day04;

public class Solver : Solver<(IEnumerable<int>, BingoGrid[]), int>
{
    public Solver() : base("Day04/input.txt") { }

    public override int PartOne((IEnumerable<int>, BingoGrid[]) input)
    {
        var (drawn, grids) = input;

        foreach (var number in drawn)
        {
            foreach (var grid in grids)
            {
                grid.MarkDrawn(number);
                if (grid.IsWinning())
                {
                    return grid.RemainingNumbers().Sum() * number;
                }
            }
        }

        return -1;
    }

    public override int PartTwo((IEnumerable<int>, BingoGrid[]) input)
    {
        var (drawn, grids) = input;

        var gridNotWonOffsets = Enumerable
            .Range(0, grids.Length)
            .ToList();

        foreach (var number in drawn)
        {
            for (var i = 0; i < grids.Length; ++i)
            {
                grids[i].MarkDrawn(number);

                if (grids[i].IsWinning())
                {
                    gridNotWonOffsets.Remove(i);
                }

                if (!gridNotWonOffsets.Any())
                {
                    return grids[i].RemainingNumbers().Sum() * number;
                }
            }
        }

        return -1;
    }

    public override (IEnumerable<int>, BingoGrid[]) ParseInput(IEnumerable<string> input)
    {
        var raw = input.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

        var drawn = raw
            .First()
            .Split(",")
            .Select(int.Parse);

        var numbers = raw
            .Skip(1)
            .Select(line => Regex
                .Split(line.Trim(), @"\s+")
                .Select(int.Parse)
                .ToArray())
            .ToArray();

        var grids = Enumerable
            .Range(0, numbers.Length)
            .Where((_, i) => i % 5 == 0)
            .Select(offset => numbers[offset..(offset + 5)])
            .Select(grid => new BingoGrid(grid))
            .ToArray();

        return (drawn, grids);
    }
}
