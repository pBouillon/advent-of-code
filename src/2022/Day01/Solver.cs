namespace _2022.Day01;

public class Solver : Solver<IEnumerable<long[]>, long>
{
    public Solver() : base("Day01/input.txt") { }

    public override long PartOne(IEnumerable<long[]> input)
        => input
            .Select(calories => calories.Sum())
            .Max();

    public override long PartTwo(IEnumerable<long[]> input)
        => input
            .Select(calories => calories.Sum())
            .OrderByDescending(sum => sum)
            .Take(3)
            .Sum();

    public override IEnumerable<long[]> ParseInput(IEnumerable<string> input)
    {
        var calories = new Stack<long[]>();

        var carried = new Stack<long>();
        foreach (var calory in input)
        {
            if (string.IsNullOrEmpty(calory))
            {
                calories.Push([.. carried]);
                carried.Clear();
            }
            else
            {
                carried.Push(long.Parse(calory));
            }
        }

        return calories;
    }
}
