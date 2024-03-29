﻿namespace _2022.Day03;

public class PuzzleTest : TestEngine<Solver, Rucksack[], int>
{
    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input =
            [
                new Rucksack("vJrwpWtwJgWrhcsFMMfFFhFp"),
                new Rucksack("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"),
                new Rucksack("PmmdzqPrVvPwwTWBwg"),
                new Rucksack("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn"),
                new Rucksack("ttgJtRGJQctTZtZT"),
                new Rucksack("CrZsJsPPZsGzwwsLwLmpwMDw"),
            ],
            Result = 157,
        },
        Solution = 8240,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input =
            [
                new Rucksack("vJrwpWtwJgWrhcsFMMfFFhFp"),
                new Rucksack("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"),
                new Rucksack("PmmdzqPrVvPwwTWBwg"),
                new Rucksack("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn"),
                new Rucksack("ttgJtRGJQctTZtZT"),
                new Rucksack("CrZsJsPPZsGzwwsLwLmpwMDw"),
            ],
            Result = 70,
        },
        Solution = 2587,
    };
}
