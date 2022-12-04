namespace _2022.Day04;

public record Range(int Start, int End)
{
    public int Length => End - Start + 1;

    public bool HasOverlapWith(Range range)
        => End >= range.Start && Start <= range.End;

    public bool IsEnglobing(Range range)
        => Start <= range.Start && End >= range.End;
}

public record Section
{
    private readonly Range Range;

    public Section(string range)
    {
        var indexes = range.Split('-')
            .Select(int.Parse)
            .ToArray();

        Range = new Range(indexes[0], indexes[1]);
    }

    public bool HasOverlapWith(Section section)
        => Range.HasOverlapWith(section.Range);

    public bool IsEnglobing(Section section)
        => Range.IsEnglobing(section.Range);
}

public record AssignementPair
{
    public readonly ValueTuple<Section, Section> Pair;

    public AssignementPair(string pair)
    {
        var assignementPair = pair.Split(',')
            .Select(assignement => new Section(assignement))
            .ToArray();

        Pair = (assignementPair[0], assignementPair[1]);
    }

    public bool HasOverlap()
    {
        var (first, second) = Pair;
        return first.HasOverlapWith(second);
    }

    public bool IsEnglobing()
    {
        var (first, second) = Pair;

        return first.IsEnglobing(second)
            || second.IsEnglobing(first);
    }
}

public class Solver : Solver<AssignementPair[], int>
{
    protected override string InputPath => "Day04/input.txt";

    public override int PartOne(AssignementPair[] input)
        => input.Count(pair => pair.IsEnglobing());

    public override int PartTwo(AssignementPair[] input)
        => input.Count(pair => pair.HasOverlap());

    public override AssignementPair[] ReadInput(string inputPath)
        => File.ReadAllLines(inputPath)
            .Select(pair => new AssignementPair(pair))
            .ToArray();
}
