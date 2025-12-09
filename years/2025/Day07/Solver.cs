using AdventOfCode.Utils.Graph;
using AdventOfCode.Utils.Matrix;

namespace _2025.Day07;

public class Solver()
    : Solver<Node<Coordinate>>("Day07/input.txt")
{
    public override Node<Coordinate> ParseInput(IEnumerable<string> input)
    {
        var diagram = input.ToArray();

        var splittersIndexes = input
            .Select(line => line
                .Select((symbol, index) => (symbol, index))
                .Where(x => x.symbol == '^')
                .Select(x => x.index)
                .ToHashSet())
            .Where(indexes => indexes.Count > 0)
            .ToArray();

        var root = new Node<Coordinate>(new Coordinate(
            diagram[0].IndexOf('S'),
            0));

        var leafs = new HashSet<Node<Coordinate>> { root };

        for (var rowIndex = 0; rowIndex < splittersIndexes.Length; ++rowIndex)
        {
            var splitters = splittersIndexes[rowIndex];

            var splittersHit = splitters
                .Where(index => leafs.Any(node => node.Value.X == index))
                .ToArray();

            foreach (var index in splittersHit)
            {
                var splittedBeam = leafs.First(node => node.Value.X == index);

                leafs.Remove(splittedBeam);

                var left = leafs.FirstOrDefault(node => node.Value.X == index - 1)
                    ?? new Node<Coordinate>(
                        splittedBeam.Value.MovedToThe(Direction.Left) with { Y = rowIndex + 1 }
                    );

                splittedBeam.AddUnidirectionalLinkTo(left);
                leafs.Add(left);

                var right = leafs.FirstOrDefault(node => node.Value.X == index + 1)
                    ?? new Node<Coordinate>(
                        splittedBeam.Value.MovedToThe(Direction.Right) with { Y = rowIndex + 1 }
                    );

                splittedBeam.AddUnidirectionalLinkTo(right);
                leafs.Add(right);
            }
        }

        return root;
    }

    public override string PartOne(Node<Coordinate> root)
    {
        var splittedBeamsCount = 0;

        var toExplore = new Stack<Node<Coordinate>>();
        toExplore.Push(root);

        var explored = new HashSet<Coordinate>();

        while (toExplore.Count > 0)
        {
            var current = toExplore.Pop();

            var isAlreadyExplored = explored.Contains(current.Value);
            if (isAlreadyExplored) continue;

            explored.Add(current.Value);

            var isSplittedBeam = !current.IsLeaf();
            if (isSplittedBeam)
            {
                ++splittedBeamsCount;
                current.ConnectsTo.ForEach(toExplore.Push);
            }
        }

        return splittedBeamsCount.ToString();
    }

    public override string PartTwo(Node<Coordinate> root)
    {
        var cache = new Dictionary<Coordinate, long>();

        long PossiblePathsToLeafsCount(Node<Coordinate> node)
        {
            var isCached = cache.ContainsKey(node.Value);
            if (isCached)
            {
                return cache[node.Value];
            }

            var result = node.IsLeaf()
                ? 1
                : node.ConnectsTo.Sum(PossiblePathsToLeafsCount);

            cache[node.Value] = result;

            return result;
        }

        return PossiblePathsToLeafsCount(root).ToString();
    }
}

public class SolverTest
    : TestEngine<Solver, Node<Coordinate>>
{
    private readonly string[] _diagram = [
        ".......S.......",
        "...............",
        ".......^.......",
        "...............",
        "......^.^......",
        "...............",
        ".....^.^.^.....",
        "...............",
        "....^.^...^....",
        "...............",
        "...^.^...^.^...",
        "...............",
        "..^...^.....^..",
        "...............",
        ".^.^.^.^.^...^.",
        "...............",
    ];

    private Node<Coordinate> Root => new Solver().ParseInput(_diagram);

    public override Puzzle PartOne => PuzzleBuilder
        .FromParsedInput(Root)
        .ExpectsResult("21")
        .WithTheActualSolutionBeing("1690");

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput(Root)
        .ExpectsResult("40")
        .WithTheActualSolutionBeing("221371496188107");
}
