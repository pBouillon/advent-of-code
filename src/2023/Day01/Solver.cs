
namespace _2023.Day01;

public class Solver : Solver<string[], long>
{
    public Solver() : base("Day01/input.txt") { }

    public override long PartOne(string[] input)
    {
        long GetNumberOf(string slice)
        {
            var digits = slice
            .Where(c => char.IsDigit(c)).ToList();

            return long.Parse($"{digits.First()}{digits.Last()}");
        }

        return input
            .Select(GetNumberOf).Sum();
    }

    public override long PartTwo(string[] input)
    {
        return 0;
    }

    public override string[] ParseInput(IEnumerable<string> input)
        => input.ToArray();
}
