using System.Text.RegularExpressions;

using Shouldly;

using Xunit;

namespace _2025.Day10;

public class Solver202610()
    : Solver<Machine[], int>(year: 2025, day: 10)
{
    public override int PartOne(Machine[] machines)
        => machines.Aggregate(
            seed: 0,
            (sum, machine) =>
            {
                var graph = CircuitGraph.BuildFor(machine);

                var allLightTurnedOffState = machine
                    .IndicatorLightDiagram
                    .Lights
                    .Replace(IndicatorLightDiagram.LightSwitchedOnIndicator, IndicatorLightDiagram.LightSwitchedOffIndicator);

                var shortestPathLength = graph.ShortestPathLength(
                    from: new IndicatorLightDiagram(allLightTurnedOffState),
                    to: machine.IndicatorLightDiagram);

                return sum + shortestPathLength;
            });

    public override int PartTwo(Machine[] machines)
        => throw new NotImplementedException();

    public override Machine[] ParseInput(IEnumerable<string> input)
        => [.. input
            .Select(line =>
            {
                var lights = Regex
                    .Match(line, @"\[([^\]]*)\]").Groups[1].Value;

                var indicatorLightDiagram = new IndicatorLightDiagram(lights);

                var buttons = Regex
                    .Matches(line, @"\(([^)]*)\)")
                    .Select(m => m.Groups[1].Value.Split(',').Select(int.Parse).ToArray())
                    .Select(indexes => new Button(indexes))
                    .ToArray();

                return new Machine(indicatorLightDiagram, buttons, new JoltageRequirements());
            })];
}

public sealed record IndicatorLightDiagram(string Lights)
{
    public const char LightSwitchedOffIndicator = '.';
    public const char LightSwitchedOnIndicator = '#';

    public IndicatorLightDiagram AfterPressing(Button button)
        => new(string.Concat(Lights
            .Select((light, index) => button.WiredLightIndexes.Contains(index)
                ? (light == LightSwitchedOnIndicator
                    ? LightSwitchedOffIndicator
                    : LightSwitchedOnIndicator)
                : light)));

    public override string ToString()
        => Lights;
}

public sealed record Button(params int[] WiredLightIndexes)
{
    private readonly string _wiredLightIndexes = string.Join(',', WiredLightIndexes);

    public bool Equals(Button? other)
        => other is not null && _wiredLightIndexes == other._wiredLightIndexes;

    public override int GetHashCode()
        => StringComparer.Ordinal.GetHashCode(_wiredLightIndexes);

    public override string ToString()
        => $"({_wiredLightIndexes})";
}

public sealed record JoltageRequirements;

public sealed record Machine(
    IndicatorLightDiagram IndicatorLightDiagram,
    Button[] Buttons,
    JoltageRequirements JoltageRequirements);

public sealed class CircuitGraph
{
    private readonly Dictionary<IndicatorLightDiagram, Dictionary<Button, IndicatorLightDiagram>> _graph = [];

    public static CircuitGraph BuildFor(Machine machine)
    {
        var circuitGraph = new CircuitGraph();

        var pendingStates = new Queue<IndicatorLightDiagram>([machine.IndicatorLightDiagram]);
        var exploredStates = new HashSet<IndicatorLightDiagram>();

        while (pendingStates.Count != 0)
        {
            var current = pendingStates.Dequeue();

            if (exploredStates.Contains(current)) continue;

            circuitGraph._graph[current] = machine
                .Buttons
                .ToDictionary(
                    button => button,
                    current.AfterPressing);

            var transitions = circuitGraph._graph[current];
            foreach (var state in transitions.Values)
            {
                if (!circuitGraph._graph.ContainsKey(state))
                {
                    pendingStates.Enqueue(state);
                }
            }

            exploredStates.Add(current);
        }

        return circuitGraph;
    }

    public int ShortestPathLength(IndicatorLightDiagram from, IndicatorLightDiagram to)
    {
        if (from == to)
        {
            return 0;
        }

        var pendingStates = new Queue<IndicatorLightDiagram>([from]);
        var distances = new Dictionary<IndicatorLightDiagram, int> { [from] = 0 };

        while (pendingStates.Count != 0)
        {
            var current = pendingStates.Dequeue();
            var distance = distances[current];

            foreach (var (_, nextState) in _graph[current])
            {
                if (distances.ContainsKey(nextState)) continue;

                if (nextState == to) return distance + 1;

                distances[nextState] = distance + 1;
                pendingStates.Enqueue(nextState);
            }
        }

        throw new InvalidOperationException($"No sequence of button presses leads from {from} to {to}.");
    }
}

public class SolverTest
    : TestEngine<Solver202610, Machine[], int>
{
    public override Puzzle PartOne
        => PuzzleBuilder
            .FromInput([
                "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
                "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
                "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}",
            ])
            .ParsedAs([
                new Machine(
                    new IndicatorLightDiagram(".##."),
                    [new Button(3), new Button(1, 3), new Button(2), new Button(2, 3), new Button(0, 2), new Button(0, 1)],
                    new JoltageRequirements()
                ),

                new Machine(
                    new IndicatorLightDiagram("...#."),
                    [new Button(0, 2, 3, 4), new Button(2, 3), new Button(0, 4), new Button(0, 1, 2), new Button(1, 2, 3, 4)],
                    new JoltageRequirements()
                ),

                new Machine(
                    new IndicatorLightDiagram(".###.#"),
                    [new Button(0, 1, 2, 3, 4), new Button(0, 3, 4), new Button(0, 1, 2, 4, 5), new Button(1, 2)],
                    new JoltageRequirements()
                ),
            ])
            .ExpectsResult(
                2   // One way to do this is by pressing the last two buttons ((0,2) and (0,1)) once each
                + 3 // One way to achieve this is by pressing the last three buttons ((0,4), (0,1,2), and (1,2,3,4)) once each
                + 2 // One way to do this is by pressing buttons (0,3,4) and (0,1,2,4,5) once each
            )
            .WithTheActualSolutionBeing(545);

    public override Puzzle PartTwo
        => PuzzleBuilder
            .FromParsedInput([])
            .ExpectsResult(0)
            .WithTheActualSolutionBeing(0);

    [Fact]
    public void PressingAButtonShouldToggleAsExpected()
    {
        var lightsPanel = new IndicatorLightDiagram(".#.");

        lightsPanel
            .AfterPressing(new Button(1, 2))
            .Lights
            .ShouldBe("..#");
    }
}
