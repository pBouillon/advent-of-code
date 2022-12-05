﻿namespace _2021.Day01;

public class Solver : Solver<int[], int>
{
    public Solver() : base("Day01/input.txt") { }

    public override int PartOne(int[] input)
    {
        var count = 0;

        for (var i = 1; i < input.Length; ++i)
        {
            if (input[i - 1] < input[i]) ++count;
        }

        return count;
    }

    public override int PartTwo(int[] input)
    {
        var windowSums = Enumerable.Range(0, input.Length - 2)
            .Select(offset => input[offset..(offset + 3)].Sum())
            .ToArray();

        return PartOne(windowSums);
    }

    public override int[] ParseInput(IEnumerable<string> input)
        => input.Select(int.Parse).ToArray();
}
