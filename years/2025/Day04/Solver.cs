using AdventOfCode.Utils.Matrix;

namespace _2025.Day04;

public class Solver()
    : Solver<IDictionary<Coordinate, char>, long>("Day04/input.txt")
{
    public override long PartOne(IDictionary<Coordinate, char> matrix)
    {
        var accessiblePaperRolls = 0;

        matrix.TraverseMatrix((coordinate, value) =>
        {
            if (value == '.') return;

            var nearestPaperRollsAmount = coordinate.Neighbors
                .Concat(coordinate.DiagonalNeighbors)
                .Count(neighbor => matrix.ContainsKey(neighbor) && matrix[neighbor] == '@');

            if (nearestPaperRollsAmount < 4) ++accessiblePaperRolls;
        });

        return accessiblePaperRolls;
    }

    public override long PartTwo(IDictionary<Coordinate, char> matrix)
    {
        var accessiblePaperRolls = 0;

        var hasAnyPaperRollBeenRemoved = true;
        while (hasAnyPaperRollBeenRemoved)
        {
            var accessiblePaperRollsThisPass = 0;

            matrix.TraverseMatrix((coordinate, value) =>
            {
                if (value == '.') return;

                var nearestPaperRollsAmount = coordinate.Neighbors
                    .Concat(coordinate.DiagonalNeighbors)
                    .Count(neighbor => matrix.ContainsKey(neighbor) && matrix[neighbor] == '@');

                if (nearestPaperRollsAmount < 4)
                {
                    ++accessiblePaperRollsThisPass;
                    matrix[coordinate] = '.';
                }
            });

            accessiblePaperRolls += accessiblePaperRollsThisPass;
            hasAnyPaperRollBeenRemoved = accessiblePaperRollsThisPass > 0;
        }

        return accessiblePaperRolls;
    }

    public override IDictionary<Coordinate, char> ParseInput(IEnumerable<string> input)
        => input.ParseMatrix();
}

public class SolverTest : TestEngine<Solver, IDictionary<Coordinate, char>, long>
{
    private readonly string[] _elvesDiagram = [
        "..@@.@@@@.",
        "@@@.@.@.@@",
        "@@@@@.@.@@",
        "@.@@@@..@.",
        "@@.@@@@.@@",
        ".@@@@@@@.@",
        ".@.@.@.@@@",
        "@.@@@.@@@@",
        ".@@@@@@@@.",
        "@.@.@@@.@.",
    ];

    public override Puzzle PartOne => PuzzleBuilder
        .FromInput(_elvesDiagram)
        .ParsedAs(_elvesDiagram.ParseMatrix())
        .ExpectsResult(13)
        .WithTheActualSolutionBeing(1_564);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput(_elvesDiagram.ParseMatrix())
        .ExpectsResult(43)
        .WithTheActualSolutionBeing(9_401);
}
