namespace _2023.Day01;

public class Solver : Solver<string[], long>
{
    public Solver() : base("Day01/input.txt") { }

    public override long PartOne(string[] input)
    {
        static long ExtractNumber(string slice)
        {
            var digits = slice
                .Where(char.IsDigit)
                .ToList();

            return long.Parse($"{digits.First()}{digits.Last()}");
        }

        return input
            .Select(ExtractNumber)
            .Sum();
    }

    public override long PartTwo(string[] input)
    {
        var spelledDigit = new Dictionary<string, char>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "one", '1' },
            { "two", '2' },
            { "three", '3' },
            { "four", '4' },
            { "five", '5' },
            { "six", '6' },
            { "seven", '7' },
            { "eight", '8' },
            { "nine", '9' },
        };

        (char?, int) FindSpelledDigit(string text)
        {
            var upTo = Math.Min("seven".Length + 1, text.Length + 1);

            for (var i = "one".Length; i < upTo; ++i)
            {
                var slice = text[0..i];
                if (spelledDigit!.TryGetValue(slice, out char spelled))
                {
                    return (spelled, i);
                }
            }

            return (null, -1);
        }

        long ExtractNumber(string slice, Queue<char> digits, int index = 0)
        {
            if (slice.Length == index)
            {
                return long.Parse($"{digits.First()}{digits.Last()}");
            }

            var next = slice[index];
            if (char.IsDigit(next))
            {
                digits.Enqueue(next);
                return ExtractNumber(slice, digits, ++index);
            }

            var (spelled, findAt) = FindSpelledDigit(slice[index..]);
            if (spelled is not null)
            {
                digits.Enqueue(spelled.Value);
                return ExtractNumber(slice, digits, index + findAt - 1);
            }

            return ExtractNumber(slice, digits, ++index);
        }

        return input
            .Select(line => ExtractNumber(line, new Queue<char>()))
            .Sum();
    }

    public override string[] ParseInput(IEnumerable<string> input)
        => input.ToArray();
}
