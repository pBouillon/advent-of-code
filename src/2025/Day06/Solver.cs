namespace _2025.Day06;

public class Solver()
    : Solver<(string[][] Columns, char[] Operators)>("Day06/input.txt")
{
    public override (string[][] Columns, char[] Operators) ParseInput(IEnumerable<string> input)
    {
        var homework = input.ToArray();

        var lineLenght = homework[0].Length;

        var numbersRows = homework[..^1];

        var symbols = homework[^1]
            .Select((@char, index) => (Symbol: @char, PositionInLine: index))
            .Where(x => x.Symbol != ' ')
            .ToArray();

        var columns = new List<string[]>();

        for (var symbolIndex = 0; symbolIndex < symbols.Length; ++symbolIndex)
        {
            var from = symbols[symbolIndex].PositionInLine;

            var to = symbolIndex + 1 < symbols.Length
                ? symbols[symbolIndex + 1].PositionInLine - 1
                : lineLenght;

            var column = new List<string>();

            foreach (var row in numbersRows)
            {
                column.Add(row[from..to]);
            }

            columns.Add([.. column]);
        }

        return (Columns: [.. columns], Operators: [.. symbols.Select(x => x.Symbol)]);
    }

    public override string PartOne((string[][] Columns, char[] Operators) input)
    {
        var symbolReducer = (char symbol)
            => (long a, long b) => symbol == '*' ? a * b : a + b;

        return input.Columns
            .Select((column, index) =>
            {
                var reducer = symbolReducer(input.Operators[index]);

                return column[1..].Aggregate(
                        seed: long.Parse(column[0]),
                        (acc, number) => reducer(acc, long.Parse(number)));
            })
            .Sum()
            .ToString();
    }

    public override string PartTwo((string[][] Columns, char[] Operators) input)
    {
        var symbolReducer = (char symbol)
            => (long a, long b) => symbol == '*' ? a * b : a + b;

        return input.Columns
             .Select((column, index) =>
             {
                 var reducer = symbolReducer(input.Operators[index]);

                 var decodedNumbers = Enumerable.Range(0, column[0].Length)
                     .Select(i => long.Parse(string.Concat(column.Select(number => number[i]))))
                     .ToList();

                 return decodedNumbers[1..].Aggregate(
                         seed: decodedNumbers[0],
                         reducer);
             })
             .Sum()
             .ToString();
    }
}

public class SolverTest
    : TestEngine<Solver, (string[][] Columns, char[] Operators)>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "123 328  51 64 ",
            " 45 64  387 23 ",
            "  6 98  215 314",
            "*   +   *   +  ",
        ])
        .ParsedAs((
            Columns: [
                ["123", " 45", "  6"],
                ["328", "64 ", "98 "],
                [" 51", "387", "215"],
                ["64 ", "23 ", "314"],
            ],
            Operators: ['*', '+', '*', '+']
        ))
        .ExpectsResult("4277556")
        .WithTheActualSolutionBeing("4405895212738");

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput((
            Columns: [
                ["123", " 45", "  6"],
                ["328", "64 ", "98 "],
                [" 51", "387", "215"],
                ["64 ", "23 ", "314"],
            ],
            Operators: ['*', '+', '*', '+']
        ))
        .ExpectsResult("3263827")
        .WithTheActualSolutionBeing("7450962489289");
}
