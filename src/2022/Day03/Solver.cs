using AdventOfCode.Commons;

namespace _2022.Day03;

public record Rucksack
{
    public readonly string Content;
    public readonly char Intersection;

    public Rucksack(string content)
    {
        Content = content;

        var half = content.Length / 2;

        Intersection = content[..half]
            .Intersect(content[half..])
            .First();
    }
}

public record ElfGroup
{
    public const int Size = 3;

    public readonly char BadgeType;

    public ElfGroup(IEnumerable<Rucksack> contents)
    {
        var groups = contents.ToArray();

        BadgeType = groups[0].Content
            .Intersect(groups[1].Content)
            .Intersect(groups[2].Content)
            .First();
    }
}

public class Solver : Solver<Rucksack[], int>
{
    protected override string InputPath => "Day03/input.txt";

    private readonly Dictionary<char, int> _priority;

    public Solver()
    {
        var lowercases = Enumerable.Range(0, 26)
            .ToDictionary(
                priority => (char)(priority + 'a'),
                priority => priority + 1);

        var uppercases = Enumerable.Range(0, 26)
            .ToDictionary(
                priority => (char)(priority + 'A'),
                priority => priority + 27);

        _priority = new[] { lowercases, uppercases }
            .SelectMany(alphabet => alphabet)
            .ToDictionary(
                alphabet => alphabet.Key,
                alphabet => alphabet.Value);
    }

    public override int PartOne(Rucksack[] input)
        => input
            .Select(rucksack => rucksack.Intersection)
            .Sum(intersection => _priority[intersection]);

    public override int PartTwo(Rucksack[] input)
    {
        var groups = Enumerable.Range(0, input.Length / ElfGroup.Size)
            .Select(i => input
                .Skip(i * ElfGroup.Size)
                .Take(ElfGroup.Size))
                .Select(rucksacks => new ElfGroup(rucksacks));

        return groups
            .Select(group => group.BadgeType)
            .Sum(badgeType => _priority[badgeType]);
    }

    public override Rucksack[] ReadInput(string inputPath)
        => File.ReadAllLines(inputPath)
            .Select(content => new Rucksack(content))
            .ToArray();
}
