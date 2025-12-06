namespace _2025.Day06;

public class Solver()
    : Solver<(long[][] Numbers, string[] Symbols)>("Day06/input.txt")
{
    public override (long[][] Numbers, string[] Symbols) ParseInput(IEnumerable<string> input)
    {
        var homework = input.ToArray();

        var numbers = homework[..^1]
            .Select(line
                => line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray())
            .ToArray();

        var symbols = homework[^1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return (Numbers: numbers, Symbols: symbols);
    }

    public override string PartOne((long[][] Numbers, string[] Symbols) input)
    {
        var symbolReducer = (string symbol)
            => (long a, long b) => symbol == "*" ? a * b : a + b;
        
        var (numbers, symbols) = input;

        var grandTotal = 0L;
        for (var i = 0; i < numbers[0].Length; ++i)
        {
            var reducer = symbolReducer(symbols[i]);

            var columnTotal = numbers[0][i];
            for (var j = 1; j < numbers.Length; ++j)
            {
                columnTotal = reducer(columnTotal, numbers[j][i]);
            }

            grandTotal += columnTotal;
        }

        return grandTotal.ToString();
    }

    public override string PartTwo((long[][] Numbers, string[] Symbols) input)
    {
        throw new NotImplementedException();
    }
}

public class SolverTest
    : TestEngine<Solver, (long[][] Numbers, string[] Symbols)>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "123 328  51 64 ",
            " 45 64  387 23 ",
            "  6 98  215 314",
            "*   +   *   +  ",
        ])
        .ParsedAs((
            Numbers: [
                [123, 328,  51,  64],
                [ 45,  64, 387,  23],
                [  6,  98, 215, 314],
            ],
            Symbols: ["*", "+", "*", "+"]
        ))
        .ExpectsResult("4277556")
        .WithTheActualSolutionBeing("4405895212738");

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput((
            Numbers: [
                [123, 328,  51,  64],
                [45,   64, 387,  23],
                [6,    98, 215, 314],
            ],
            Symbols: ["*", "+", "*", "+"]
        ))
        .ExpectsResult("0")
        .WithTheActualSolutionBeing("0");
}
