using System.Diagnostics;
using System.Linq;
using Xunit.Abstractions;

namespace _2025.Day08;

public class Solver()
    : Solver<Coordinate[]>("Day08/input.txt")
{
    public override Coordinate[] ParseInput(IEnumerable<string> input)
        => [.. input
            .Select(line =>
            {
                var coordinates = line.Split(',')
                    .Select(int.Parse)
                    .ToArray();

                return new Coordinate(coordinates[0], coordinates[1], coordinates[2]);
            })];

    public override string PartOne(Coordinate[] junctionBoxes)
    {
        var possibleConnections = junctionBoxes
            .SelectMany(current => junctionBoxes
                .Select(other => current.X < other.X
                    ? (current, other)
                    : (other, current))
                .Where(connection => connection.Item1 != connection.Item2))
            .ToHashSet();

        var distanceTo = possibleConnections
            .ToDictionary(
                connection => connection,
                connection => connection.Item1.DistanceTo(connection.Item2)
            );

        var shortestDistances = distanceTo
            .OrderBy(kvp => kvp.Value)
            .Select(kvp => kvp.Key);

        var circuitIdOf = junctionBoxes
            .Select((junctionBox, i) => (JunctionBox: junctionBox, Index: i))
            .ToDictionary(
                x => x.JunctionBox,
                x => x.Index);

        var connectionsToMake = shortestDistances
            // This is just to alternate between the examples and the solution
            .Take(junctionBoxes.Length == 20 ? 10 : 1000);

        foreach (var (from, to) in connectionsToMake)
        {
            var areAlreadyInTheSameCircuit = circuitIdOf[from] == circuitIdOf[to];
            if (areAlreadyInTheSameCircuit) continue;

            var dependentConnections = circuitIdOf
                .Where(kvp => kvp.Value == circuitIdOf[to!])
                .Select(kvp => kvp.Key)
                .ToList();

            dependentConnections.ForEach(junctionBox => circuitIdOf[junctionBox] = circuitIdOf[from]);
        }

        return circuitIdOf
            .GroupBy(kvp => kvp.Value)
            .Select(group => group.Count())
            .OrderDescending()
            .Take(3)
            .Aggregate(seed: 1, (total, count) => total *= count)
            .ToString();
    }

    public override string PartTwo(Coordinate[] junctionBoxes)
    {
        var possibleConnections = junctionBoxes
            .SelectMany(current => junctionBoxes
                .Select(other => current.X < other.X
                    ? (current, other)
                    : (other, current))
                .Where(connection => connection.Item1 != connection.Item2))
            .ToHashSet();

        var distanceTo = possibleConnections
            .ToDictionary(
                connection => connection,
                connection => connection.Item1.DistanceTo(connection.Item2)
            );

        var shortestDistances = distanceTo
            .OrderBy(kvp => kvp.Value)
            .Select(kvp => kvp.Key);

        var circuitIdOf = junctionBoxes
            .Select((junctionBox, i) => (JunctionBox: junctionBox, Index: i))
            .ToDictionary(
                x => x.JunctionBox,
                x => x.Index);

        foreach (var (from, to) in shortestDistances)
        {
            var areAlreadyInTheSameCircuit = circuitIdOf[from] == circuitIdOf[to];
            if (areAlreadyInTheSameCircuit) continue;

            var dependentConnections = circuitIdOf
                .Where(kvp => kvp.Value == circuitIdOf[to!])
                .Select(kvp => kvp.Key)
                .ToList();

            dependentConnections.ForEach(junctionBox => circuitIdOf[junctionBox] = circuitIdOf[from]);

            var isOneBigCircuit = circuitIdOf.Values.ToHashSet().Count == 1;
            if (isOneBigCircuit) return (1L * from.X * to.X).ToString();
        }

        return string.Empty;
    }
}

[DebuggerDisplay("(X: {X}, Y: {Y}, Z: {Z})")]
public record Coordinate(int X, int Y, int Z)
{
    public double DistanceTo(Coordinate other)
        => Math.Sqrt(
            Math.Pow(X - other.X, 2)
            + Math.Pow(Y - other.Y, 2)
            + Math.Pow(Z - other.Z, 2));
}

public class SolverTest
    : TestEngine<Solver, Coordinate[]>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "162,817,812",
            "57,618,57",
            "906,360,560",
            "592,479,940",
            "352,342,300",
            "466,668,158",
            "542,29,236",
            "431,825,988",
            "739,650,466",
            "52,470,668",
            "216,146,977",
            "819,987,18",
            "117,168,530",
            "805,96,715",
            "346,949,466",
            "970,615,88",
            "941,993,340",
            "862,61,35",
            "984,92,344",
            "425,690,689",
        ])
        .ParsedAs([
            new(162, 817, 812),
            new(57, 618, 57),
            new(906, 360, 560),
            new(592, 479, 940),
            new(352, 342, 300),
            new(466, 668, 158),
            new(542, 29, 236),
            new(431, 825, 988),
            new(739, 650, 466),
            new(52, 470, 668),
            new(216, 146, 977),
            new(819, 987, 18),
            new(117, 168, 530),
            new(805, 96, 715),
            new(346, 949, 466),
            new(970, 615, 88),
            new(941, 993, 340),
            new(862, 61, 35),
            new(984, 92, 344),
            new(425, 690, 689),
        ])
        .ExpectsResult("40")
        .WithTheActualSolutionBeing("175440");

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([
            new(162, 817, 812),
            new(57, 618, 57),
            new(906, 360, 560),
            new(592, 479, 940),
            new(352, 342, 300),
            new(466, 668, 158),
            new(542, 29, 236),
            new(431, 825, 988),
            new(739, 650, 466),
            new(52, 470, 668),
            new(216, 146, 977),
            new(819, 987, 18),
            new(117, 168, 530),
            new(805, 96, 715),
            new(346, 949, 466),
            new(970, 615, 88),
            new(941, 993, 340),
            new(862, 61, 35),
            new(984, 92, 344),
            new(425, 690, 689),
        ])
        .ExpectsResult("25272")
        .WithTheActualSolutionBeing("3200955921");
}
