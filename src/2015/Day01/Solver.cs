namespace _2015.Day01;

public class Solver : Solver<string, int>
{
    protected override string InputPath => "Day01/input.txt";

    public override int PartOne(string input)
    {
        static int Recursive(string floors, int current = 0)
        {
            if (floors.Length == 0) return current;

            var head = floors[0];
            var tail = floors[1..];

            return head switch
            {
                '(' => Recursive(tail, ++current),
                ')' => Recursive(tail, --current),
                _ => throw new Exception($"Unexpected symbol '{head}'")
            };
        }

        return Recursive(input);
    }

    public override int PartTwo(string input)
    {
        static int Recursive(string floors, int current = 0, int position = 0)
        {
            if (current == -1) return position;

            var head = floors[0];
            var tail = floors[1..];

            return head switch
            {
                '(' => Recursive(tail, ++current, ++position),
                ')' => Recursive(tail, --current, ++position),
                _ => throw new System.Exception($"Unexpected symbol '{head}'")
            };
        }

        return Recursive(input);
    }

    public override string ReadInput(string inputPath)
        => File
            .ReadLines(inputPath)
            .FirstOrDefault() ?? string.Empty;
}
