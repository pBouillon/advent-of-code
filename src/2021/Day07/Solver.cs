namespace _2021.Day07;

public class Solver : Solver<int[], int>
{
    public Solver() : base("Day07/input.txt") { }

    public override int PartOne(int[] input)
        => Enumerable.Range(input.Min(), input.Max())
            .Select(distance => input.Sum(position => Math.Abs(distance - position)))
            .Min();

    public override int PartTwo(int[] input)
        => Enumerable.Range(input.Min(), input.Max())
            .Select(distance => input
                .Select(position => Math.Abs(distance - position))
                .Sum(consumption => consumption * (consumption + 1) / 2))
            .Min();

    public override int[] ParseInput(IEnumerable<string> input)
        => input.First()
            .Split(",")
            .Select(int.Parse)
            .ToArray();
}
