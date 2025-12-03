using FluentAssertions;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace _2025.Day03;

public static class Extentions
{
    public static int ToInt(this char @char) => @char - '0';
}

public class Solver()
    : Solver<string[], long>("Day03/input.txt")
{
    public static (int Value, int Offset) MaxPossibleDigitIn(string bank)
    {
        var bestDigit = (Value: bank[0].ToInt(), Offset: 0);

        for (int i = 0; i < bank.Length; i++)
        {
            var current = (Value: bank[i].ToInt(), Offset: i);
            if (current.Value > bestDigit.Value) bestDigit = current;
        }

        return bestDigit;
    }

    public static long LongestPossibleNumberIn(string bank, int finalNumberLength)
    {
        var buffer = new StringBuilder();
        var singleDigitsSkipped = 0;

        for (var currentPowerOfTen = finalNumberLength; currentPowerOfTen > 0; --currentPowerOfTen)
        {
            var numbersOfPowerOfTenTooBig = finalNumberLength - currentPowerOfTen;
            var numbersOfPowerOfTenTooSmall = currentPowerOfTen - 1;

            var searchRange = bank[(numbersOfPowerOfTenTooBig + singleDigitsSkipped)..^numbersOfPowerOfTenTooSmall];

            var nextBestDigit = MaxPossibleDigitIn(searchRange);

            buffer.Append(nextBestDigit.Value);
            singleDigitsSkipped += nextBestDigit.Offset;
        }

        return long.Parse(buffer.ToString());
    }

    public override long PartOne(string[] strings)
        => strings
            .Select(bank => LongestPossibleNumberIn(bank, finalNumberLength: 2))
            .Sum();

    public override long PartTwo(string[] strings)
        => strings
            .Select(bank => LongestPossibleNumberIn(bank, finalNumberLength: 12))
            .Sum();

    public override string[] ParseInput(IEnumerable<string> input)
        => [.. input];
}

public class SolverTest : TestEngine<Solver, string[], long>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "987654321111111",
            "811111111111119",
            "234234234234278",
            "818181911112111",
        ])
        .ParsedAs([
            "987654321111111",
            "811111111111119",
            "234234234234278",
            "818181911112111",
        ])
        .ExpectsResult(357)
        .WithTheActualSolutionBeing(17_613);

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([
            "987654321111111",
            "811111111111119",
            "234234234234278",
            "818181911112111",
        ])
        .ExpectsResult(3_121_910_778_619)
        .WithTheActualSolutionBeing(175_304_218_462_560);
}
