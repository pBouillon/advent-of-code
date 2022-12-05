using System.Diagnostics;

namespace _2022.Day02;

public record Round(string Opponent, string You)
{
    private readonly Dictionary<string, int> _moveScore = new()
    {
        { "X", 1 },
        { "Y", 2 },
        { "Z", 3 }
    };

    private readonly Dictionary<string, string> _mirror = new()
    {
        { "A", "X" },  // Rock
        { "B", "Y" },  // Paper
        { "C", "Z" },  // Scissors
    };

    private readonly Dictionary<string, string> _winCombinations = new()
    {
        { "A", "Y" },  // Rock vs Paper
        { "B", "Z" },  // Paper vs Scissors
        { "C", "X" },  // Scissors vs Rock
    };

    private readonly Dictionary<string, string> _loseCombinations = new()
    {
        { "A", "Z" },  // Rock vs Scissors
        { "B", "X" },  // Paper vs Rock
        { "C", "Y" },  // Scissors vs Paper
    };

    public int Score => _moveScore[You] + RoundScore();

    private int RoundScore()
    {
        var result = $"{Opponent}{You}";

        if (_winCombinations.Select(kvp => $"{kvp.Key}{kvp.Value}").Contains(result))
        {
            return 6;
        }

        if (_loseCombinations.Select(kvp => $"{kvp.Key}{kvp.Value}").Contains(result))
        {
            return 0;
        }

        return 3;
    }

    public int RoundScoreWithOutcome()
    {
        var roundWithOutcome = this with
        {
            You = You switch
            {
                "X" => _loseCombinations[Opponent],  // Should lose
                "Y" => _mirror[Opponent],            // Should draw
                "Z" => _winCombinations[Opponent],   // Should win
                _ => throw new UnreachableException(),
            }
        };

        return roundWithOutcome.Score;
    }
}

public class Solver : Solver<IEnumerable<Round>, int>
{
    public Solver() : base("Day02/input.txt") { }

    public override int PartOne(IEnumerable<Round> input)
        => input.Select(round => round.Score).Sum();

    public override int PartTwo(IEnumerable<Round> input)
        => input.Select(round => round.RoundScoreWithOutcome()).Sum();

    public override IEnumerable<Round> ParseInput(IEnumerable<string> input)
        => input.Select(line =>
            {
                var parts = line.Split(' ');
                return new Round(parts[0], parts[1]);
            });
}
